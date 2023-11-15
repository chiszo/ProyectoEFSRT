using EFSRT_RopaStore.Repositorio.Interface;
using Microsoft.Data.SqlClient;
using RopaStore.Domain.Entidad;

namespace EFSRT_RopaStore.Repositorio.RepositorioSQL
{
    public class cargoSQL:ICargo
    {
        private readonly string cadena;

        public cargoSQL()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").
                Build().GetConnectionString("sql");
        }

        public IEnumerable<Cargo> listado()
        {
            List<Cargo> temporal = new List<Cargo>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_cargo", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Cargo()
                    {
                        idcargo = dr.GetString(0),
                        descripcion = dr.GetString(1),

                    });
                }
                dr.Close();
            }
            return temporal;
        }
    }
}
