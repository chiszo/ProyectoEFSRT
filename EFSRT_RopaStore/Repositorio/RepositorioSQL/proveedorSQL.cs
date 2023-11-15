using EFSRT_RopaStore.Repositorio.Interface;
using Microsoft.Data.SqlClient;
using RopaStore.Domain.Entidad;
using System.Data;

namespace EFSRT_RopaStore.Repositorio.RepositorioSQL
{
    public class proveedorSQL : IProveedor
    {
        private readonly string cadena;

        public proveedorSQL()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").
                Build().GetConnectionString("sql");
        }

        public string DeleteProveedor(Proveedor reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_proveedor_delete", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", reg.idproveedor);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha eliminado {i} proveedor";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally { cn.Close(); }
            }

            return mensaje;
        }

        public Proveedor GetProveedor(string idproveedor)
        {
            if (string.IsNullOrEmpty(idproveedor))
                return new Proveedor();
            else
                return GetProveedores().FirstOrDefault(x => x.idproveedor == idproveedor);
        }

        public IEnumerable<Proveedor> GetProveedores()
        {
            List<Proveedor> temporal = new List<Proveedor>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_proveedor", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Proveedor()
                    {
                       idproveedor = dr.GetString(0),
                       telefono = dr.GetString(1),
                       direccion = dr.GetString(2),
                       empresa = dr.GetString(3),
                       ruc = dr.GetString(4),
                       correo = dr.GetString(5),
                       representante = dr.GetString(6),

                    });
                }
                dr.Close();
            }
            return temporal;
        }

        public IEnumerable<Proveedor> GetProveedores(string nombre)
        {
            return GetProveedores().Where(x => x.empresa.StartsWith(nombre, StringComparison.CurrentCultureIgnoreCase));
        }

        public string InsertProveedor(Proveedor reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_proveedor_add", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", reg.idproveedor);
                    cmd.Parameters.AddWithValue("@telefono", reg.telefono);
                    cmd.Parameters.AddWithValue("@direccion", reg.direccion);
                    cmd.Parameters.AddWithValue("@empresa", reg.empresa);
                    cmd.Parameters.AddWithValue("@ruc", reg.ruc);
                    cmd.Parameters.AddWithValue("@correo", reg.correo);
                    cmd.Parameters.AddWithValue("@representante", reg.representante);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha agregado {i} proveedor";
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

        public string UpdateProveedor(Proveedor reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_proveedor_update", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", reg.idproveedor);
                    cmd.Parameters.AddWithValue("@telefono", reg.telefono);
                    cmd.Parameters.AddWithValue("@direccion", reg.direccion);
                    cmd.Parameters.AddWithValue("@empresa", reg.empresa);
                    cmd.Parameters.AddWithValue("@ruc", reg.ruc);
                    cmd.Parameters.AddWithValue("@correo", reg.correo);
                    cmd.Parameters.AddWithValue("@representante", reg.representante);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha modificado {i} proveedor";
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
    }
}
