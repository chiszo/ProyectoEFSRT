using EFSRT_RopaStore.Repositorio.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using Newtonsoft.Json;
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


        public IEnumerable<CompraProducto> listado()
        {
            throw new NotImplementedException();
        }
    }
}
