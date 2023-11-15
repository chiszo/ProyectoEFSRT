using RopaStore.Domain.Entidad;

namespace EFSRT_RopaStore.Repositorio.Interface
{
    public interface ITipoprod
    {
        IEnumerable<TipoProducto> listado();
    }
}
