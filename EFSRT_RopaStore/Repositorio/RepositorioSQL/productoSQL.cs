using EFSRT_RopaStore.Repositorio.Interface;
using Microsoft.Data.SqlClient;
using RopaStore.Domain.Entidad;
using System.Data;

namespace EFSRT_RopaStore.Repositorio.RepositorioSQL
{
    public class productoSQL : IProducto
    {
        private readonly string cadena;

        public productoSQL()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").
                Build().GetConnectionString("sql");
        }

        public string DeleteProductos(Producto reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_productos_delete", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", reg.idproducto);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha eliminado {i} producto";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally { cn.Close(); }
            }

            return mensaje;
        }

        public Producto GetProducto(string idproducto)
        {
            if (string.IsNullOrEmpty(idproducto))
                return new Producto();
            else
                return GetProductos().FirstOrDefault(x => x.idproducto == idproducto);
        }

        public IEnumerable<Producto> GetProductos()
        {
            List<Producto> temporal = new List<Producto>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_productos", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Producto()
                    {
                        idproducto = dr.GetString(0),
                        idtipopro = dr.GetString(1),
                        idproveedor = dr.GetString(2),
                        nombre = dr.GetString(3),
                        cantidad = dr.GetInt16(4),
                        precio = dr.GetDecimal(5),
                        stockmin = dr.GetInt16(6),
                        stockmax = dr.GetInt16(7),
                        idlote = dr.GetString(8),
                        estado = dr.GetBoolean(9),
                    });
                }
                dr.Close();
            }
            return temporal;
        }

        public IEnumerable<Producto> GetProductos(string nombre)
        {
            return GetProductos().Where(x => x.nombre.StartsWith(nombre, StringComparison.CurrentCultureIgnoreCase));
        }

        public string InsertProductos(Producto reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_productos_add", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", reg.idproducto);
                    cmd.Parameters.AddWithValue("@tipo", reg.idtipopro);
                    cmd.Parameters.AddWithValue("@idproveedor", reg.idproveedor);
                    cmd.Parameters.AddWithValue("@nombre", reg.nombre);
                    cmd.Parameters.AddWithValue("@cantidad", reg.cantidad);
                    cmd.Parameters.AddWithValue("@precio", reg.precio);
                    cmd.Parameters.AddWithValue("@min", reg.stockmin);
                    cmd.Parameters.AddWithValue("@max", reg.stockmax);
                    cmd.Parameters.AddWithValue("@idlote", reg.idlote);
                    cmd.Parameters.AddWithValue("@estado", reg.estado);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha agregado {i} producto";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }
            return mensaje;
        }

        public string UpdateProductos(Producto reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_productos_update", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", reg.idproducto);
                    cmd.Parameters.AddWithValue("@tipo", reg.idtipopro);
                    cmd.Parameters.AddWithValue("@idproveedor", reg.idproveedor);
                    cmd.Parameters.AddWithValue("@nombre", reg.nombre);
                    cmd.Parameters.AddWithValue("@cantidad", reg.cantidad);
                    cmd.Parameters.AddWithValue("@precio", reg.precio);
                    cmd.Parameters.AddWithValue("@min", reg.stockmin);
                    cmd.Parameters.AddWithValue("@max", reg.stockmax);
                    cmd.Parameters.AddWithValue("@idlote", reg.idlote);
                    cmd.Parameters.AddWithValue("@estado", reg.estado);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha modificado {i} producto";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }
            return mensaje;
        }

        public IEnumerable<Producto> GetProveedor(string idproveedor)
        {
            return GetProductos().Where(x => x.idproveedor.StartsWith(idproveedor, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
