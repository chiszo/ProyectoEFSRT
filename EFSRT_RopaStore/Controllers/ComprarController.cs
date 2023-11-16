using EFSRT_RopaStore.Repositorio.Interface;
using EFSRT_RopaStore.Repositorio.RepositorioSQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using RopaStore.Domain.Entidad;

namespace EFSRT_RopaStore.Controllers
{
    public class ComprarController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;
        ILote _lote;
        ICargo _cargo;
        ITipoprod _tipopro;
        IArea _area;
        IConfiguration _config;
  

        public ComprarController(IConfiguration config)
        {
            _config = config;
            _producto = new productoSQL();
            _proveedor = new proveedorSQL();
            _trabajador = new trabajadorSQL();
            _area = new areaSQL();
            _cargo = new cargoSQL();
            _lote = new loteSQL();
            _tipopro = new tipoproSQL();
        }
        public async Task<IActionResult> list(string idproveedor = "", int p = 0)
        {
            ViewBag.idproveedor= new SelectList(_proveedor.GetProveedores(), "idproveedor", "empresa",idproveedor);

            IEnumerable<Producto> temporal = _producto.GetProductos();
            IEnumerable<Producto> temporal1 = _producto.GetProveedor(idproveedor);
            int f = 7;
            int c = temporal.Count();

            int c1= temporal1.Count();
            int pags1 = c1 % f == 0 ? c1 / f : c1 / f + 1;

            int pags = c % f == 0 ? c / f : c / f + 1;
            ViewBag.p = p;
            ViewBag.pags = pags1;

            if (HttpContext.Session.GetString("Canasta") == null)
                HttpContext.Session.SetString("Canasta",
                        JsonConvert.SerializeObject(new List<DetalleCompra>()));

            if (idproveedor == null)
            {
                ViewBag.pags = pags;
                return View(await Task.Run(() => temporal.Skip(f * p).Take(f)));
            }

            return View(await Task.Run(() => temporal1.Skip(f * p).Take(f)));
        }

        public async Task<IActionResult> Detalle(string id = "")
        {
            Producto reg = _producto.GetProducto(id);
            if (reg == null)
                return RedirectToAction("list");
            else
                return View(await Task.Run(() => reg));
        }

        [HttpPost]
        public async Task<IActionResult> Detalle(string codigo, int cantidad)
        {
            Producto item = _producto.GetProducto(codigo);

            DetalleCompra reg = new DetalleCompra()
            {
                idproducto = item.idproducto,
                preciocompra = item.precio,
                cantidad = cantidad
            };

            List<DetalleCompra> temporal = JsonConvert.DeserializeObject<List<DetalleCompra>>(HttpContext.Session.GetString("Canasta"));
            temporal.Add(reg);

            HttpContext.Session.SetString("Canasta", JsonConvert.SerializeObject(temporal));

            ViewBag.mensaje = "Producto Agregado";

            return View(await Task.Run(() => item));
        }

        public async Task<IActionResult> Canasta()
        {
            List<DetalleCompra> temporal = JsonConvert.DeserializeObject<List<DetalleCompra>>(
                        HttpContext.Session.GetString("Canasta"));

            return View(await Task.Run(() => temporal));
        }

        [HttpPost]
        public async Task<IActionResult> Canasta(string codigo, int cantidad)
        {
            List<DetalleCompra> temporal = JsonConvert.DeserializeObject<List<DetalleCompra>>(
                        HttpContext.Session.GetString("Canasta"));

            Producto item = _producto.GetProducto(codigo);
            DetalleCompra reg = temporal.Where(it => it.idproducto == codigo).First();
            temporal.Remove(reg);
            DetalleCompra regi = new DetalleCompra()
            {
                idproducto = item.idproducto,
                preciocompra = item.precio,
                cantidad = cantidad
            };

            temporal.Add(regi);
            HttpContext.Session.SetString("Canasta", JsonConvert.SerializeObject(temporal));
            return View(await Task.Run(() => temporal));
        }

        public IActionResult Delete(string id)
        {
            List<DetalleCompra> temporal = JsonConvert.DeserializeObject<List<DetalleCompra>>(
                        HttpContext.Session.GetString("Canasta"));

            DetalleCompra reg = temporal.Where(it => it.idproducto == id).First();
            temporal.Remove(reg);

            HttpContext.Session.SetString("Canasta", JsonConvert.SerializeObject(temporal));


            return RedirectToAction("Canasta");
        }

        public async Task<IActionResult> Pedido()
        {
            return View(await Task.Run(() => new CompraProducto()));
        }

        [HttpPost]
        public async Task<IActionResult> Pedido(string idpedido, CompraProducto reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_config["ConnectionStrings:sql"]))
            {
                cn.Open();
                SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_agregar_compra", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", idpedido);
                    cmd.Parameters.AddWithValue("@fecha", reg.fechapedido);
                    cmd.Parameters.AddWithValue("@idproveedor", reg.idproveedor);
                    cmd.Parameters.AddWithValue("@montot", reg.montoT);
                    cmd.ExecuteNonQuery();

                    List<DetalleCompra> temporal = JsonConvert.DeserializeObject<List<DetalleCompra>>(
                        HttpContext.Session.GetString("Canasta"));
                    temporal.ForEach(x =>
                    {
                        SqlCommand cmd = new SqlCommand("sp_detalle_compra", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", idpedido);
                        cmd.Parameters.AddWithValue("@idproducto", x.idproducto);
                        cmd.Parameters.AddWithValue("@precio", x.preciocompra);
                        cmd.Parameters.AddWithValue("@cant", x.cantidad);
                        cmd.Parameters.AddWithValue("@montot", x.monto);
                        cmd.ExecuteNonQuery();
                    });

                    temporal.ForEach(x =>
                    {
                        cmd = new SqlCommand(
                        "update producto set cantidad+=@cant Where idproducto=@idproducto", cn, tr);
                        cmd.Parameters.AddWithValue("@idproducto", x.idproducto);
                        cmd.Parameters.AddWithValue("@cantidad", x.cantidad);
                        cmd.ExecuteNonQuery();
                    });

                    tr.Commit();
                    mensaje = "Pedido Registrado";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message; tr.Rollback();
                }
                finally { cn.Close(); }
            }
            ViewBag.mensaje = mensaje;
            return View(await Task.Run(() => reg));
        }
    }
}
