using EFSRT_RopaStore.Repositorio.Interface;
using Microsoft.Data.SqlClient;
using RopaStore.Domain.Entidad;
using System.Data;

namespace EFSRT_RopaStore.Repositorio.RepositorioSQL
{
    public class compraSQL : IComprobante
    {
        private readonly string cadena;

        public compraSQL()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").
                Build().GetConnectionString("sql");
        }

        public string agregarComprobante(CompraProducto a, DetalleCompra b)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_agregar_compra", cn, tr);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", a.codcomprapro);
                    cmd.Parameters.AddWithValue("@fecha", a.fechapedido);
                    cmd.Parameters.AddWithValue("@idproveedor", a.idproveedor);
                    cmd.Parameters.AddWithValue("@montot", a.montoT);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("sp_detalle_compra", cn, tr);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", b.idproducto);
                    cmd.Parameters.AddWithValue("@idproducto",b.idproducto);
                    cmd.Parameters.AddWithValue("@precio", b.preciocompra);
                    cmd.Parameters.AddWithValue("@cant", b.cantidad);
                    cmd.Parameters.AddWithValue("@montot", b.monto);
                    cmd.ExecuteNonQuery();

                    tr.Commit();
                    mensaje = "Proceso Completado";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                    tr.Rollback();
                }
                finally { cn.Close(); }
            }
            return mensaje;
        }

        public IEnumerable<CompraProducto> listado()
        {
            throw new NotImplementedException();
        }
    }
}
