using EFSRT_RopaStore.Repositorio.Interface;
using RopaStore.Domain.Entidad;

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
            throw new NotImplementedException();
        }

        public IEnumerable<CompraProducto> listado()
        {
            throw new NotImplementedException();
        }
    }
}
