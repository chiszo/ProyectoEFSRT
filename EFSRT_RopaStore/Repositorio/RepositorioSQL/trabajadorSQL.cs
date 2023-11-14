using EFSRT_RopaStore.Repositorio.Interface;
using Microsoft.Data.SqlClient;
using RopaStore.Domain.Entidad;
using System.Data;

namespace EFSRT_RopaStore.Repositorio.RepositorioSQL
{
    public class trabajadorSQL : ITrabajador
    {
        private readonly string cadena;

        public trabajadorSQL()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").
                Build().GetConnectionString("sql");
        }

        public string DeleteTrabajador(Trabajador reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_trabajadores_delete", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", reg.idtrabajador);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje= $"Se ha eliminado {i} trabajador";
                }
                catch (Exception ex) 
                { 
                    mensaje=ex.Message;
                }
                finally { cn.Close(); }
            }

                return mensaje;
        }

        public Trabajador GetTrabajador(string idtrabajador)
        {
            if (string.IsNullOrEmpty(idtrabajador))
                return new Trabajador();
            else
                return GetTrabajadores().FirstOrDefault(x => x.idtrabajador==idtrabajador);
        }

        public IEnumerable<Trabajador> GetTrabajadores()
        {
            List<Trabajador> temporal = new List<Trabajador>();
            using (SqlConnection cn = new SqlConnection(cadena))
            { 
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_trabajadores", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) 
                {
                    temporal.Add(new Trabajador()
                    {
                        idtrabajador = dr.GetString(0),
                        nombre = dr.GetString(1),
                        apellido = dr.GetString(2),
                        dni = dr.GetString(3),
                        telefono = dr.GetString(4),
                        correo = dr.GetString(5),
                        direccion = dr.GetString(6),
                        idcargo = dr.GetString(7),
                        idarea = dr.GetString(8),
                        clave = dr.GetString(9),

                    });
                }
                dr.Close();
            }
            return temporal;
        }

        public IEnumerable<Trabajador> GetTrabajadores(string nombre)
        {
            return GetTrabajadores().Where(x=>x.nombre.StartsWith(nombre, StringComparison.CurrentCultureIgnoreCase));
        }

        public string InsertTrabajador(Trabajador reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_trabajadores_add",cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", reg.idtrabajador);
                    cmd.Parameters.AddWithValue("@nombres", reg.nombre);
                    cmd.Parameters.AddWithValue("@apellidos", reg.apellido);
                    cmd.Parameters.AddWithValue("@dni", reg.dni);
                    cmd.Parameters.AddWithValue("@tel", reg.telefono);
                    cmd.Parameters.AddWithValue("@correo", reg.correo);
                    cmd.Parameters.AddWithValue("@direccion", reg.direccion);
                    cmd.Parameters.AddWithValue("@idcargo", reg.idcargo);
                    cmd.Parameters.AddWithValue("@idarea", reg.idarea);
                    cmd.Parameters.AddWithValue("@contraseña", reg.clave);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha agregado {i} trabajador";
                }
                catch (Exception ex)
                {
                    mensaje=ex.Message;
                }
                finally
                { 
                    cn.Close();
                }
            }
            return mensaje;
        }

        public string UpdateTrabajador(Trabajador reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_trabajadores_update", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", reg.idtrabajador);
                    cmd.Parameters.AddWithValue("@nombres", reg.nombre);
                    cmd.Parameters.AddWithValue("@apellidos", reg.apellido);
                    cmd.Parameters.AddWithValue("@dni", reg.dni);
                    cmd.Parameters.AddWithValue("@tel", reg.telefono);
                    cmd.Parameters.AddWithValue("@correo", reg.correo);
                    cmd.Parameters.AddWithValue("@direccion", reg.direccion);
                    cmd.Parameters.AddWithValue("@idcargo", reg.idcargo);
                    cmd.Parameters.AddWithValue("@idarea", reg.idarea);
                    cmd.Parameters.AddWithValue("@contraseña", reg.clave);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha modificado {i} trabajador";
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
