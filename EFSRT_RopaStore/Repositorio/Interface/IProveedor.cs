using RopaStore.Domain.Entidad;

namespace EFSRT_RopaStore.Repositorio.Interface
{
    public interface IProveedor
    {
        IEnumerable<Proveedor> GetProveedores();
        IEnumerable<Proveedor> GetProveedores(string nombre);
        string InsertProveedor(Proveedor reg);
        string UpdateProveedor(Proveedor reg);
        string DeleteProveedor(Proveedor reg);
        Proveedor GetProveedor(string idproveedor);
    }
}
