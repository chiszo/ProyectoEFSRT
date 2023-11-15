using EFSRT_RopaStore.Repositorio.Interface;
using Microsoft.Data.SqlClient;
using RopaStore.Domain.Entidad;

namespace EFSRT_RopaStore.Repositorio.RepositorioSQL
{
    public class tipoproSQL:ITipoprod
    {
        private readonly string cadena;

        public tipoproSQL()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").
                Build().GetConnectionString("sql");
        }

        public IEnumerable<TipoProducto> listado()
        {
            List<TipoProducto> temporal = new List<TipoProducto>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_tipotpo", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new TipoProducto()
                    {
                        idtipopro = dr.GetString(0),
                        descripcion = dr.GetString(1),

                    });
                }
                dr.Close();
            }
            return temporal;
        }
    }
}
