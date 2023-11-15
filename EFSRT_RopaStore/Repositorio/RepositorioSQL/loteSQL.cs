using EFSRT_RopaStore.Repositorio.Interface;
using Microsoft.Data.SqlClient;
using RopaStore.Domain.Entidad;

namespace EFSRT_RopaStore.Repositorio.RepositorioSQL
{
    public class loteSQL:ILote
    {
        private readonly string cadena;

        public loteSQL()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").
                Build().GetConnectionString("sql");
        }

        public IEnumerable<Lote> listado()
        {
            List<Lote> temporal = new List<Lote>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_lote", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Lote()
                    {
                        idlote = dr.GetString(0),
                        descripcion = dr.GetString(1),

                    });
                }
                dr.Close();
            }
            return temporal;
        }
    }
}
