using RopaStore.Domain.Entidad;

namespace EFSRT_RopaStore.Repositorio.Interface
{
    public interface IComprobante
    {
        IEnumerable<CompraProducto> listado();

        IEnumerable<DetalleCompra> listadoDetalle();
        IEnumerable<DetalleCompra> GetBoleta(string id);

    }
}
