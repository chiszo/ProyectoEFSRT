using RopaStore.Domain.Entidad;

namespace EFSRT_RopaStore.Repositorio.Interface
{
    public interface ICargo
    {
        IEnumerable<Cargo> listado();
    }
}
