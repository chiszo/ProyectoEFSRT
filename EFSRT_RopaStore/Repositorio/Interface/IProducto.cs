using RopaStore.Domain.Entidad;

namespace EFSRT_RopaStore.Repositorio.Interface
{
    public interface IProducto
    {
        IEnumerable<Producto> GetProductos();
        IEnumerable<Producto> GetProductos(string nombre);
        IEnumerable<Producto> GetProveedor(string idproveedor);
        string InsertProductos(Producto reg);
        string UpdateProductos(Producto reg);
        string DeleteProductos(Producto reg);
        Producto GetProducto(string idproducto);

    }
}
