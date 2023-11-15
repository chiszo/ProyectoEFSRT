using EFSRT_RopaStore.Repositorio.Interface;
using Microsoft.Data.SqlClient;
using RopaStore.Domain.Entidad;

namespace EFSRT_RopaStore.Repositorio.RepositorioSQL
{
    public class areaSQL:IArea
    {
        private readonly string cadena;

        public areaSQL()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").
                Build().GetConnectionString("sql");
        }

        public IEnumerable<Area> listado()
        {
            List<Area> temporal = new List<Area>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_area", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Area()
                    {
                        idarea = dr.GetString(0),
                        descripcion= dr.GetString(1),

                    });
                }
                dr.Close();
            }
            return temporal;
        }
    }
}
