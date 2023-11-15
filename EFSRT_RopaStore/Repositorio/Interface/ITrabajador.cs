using RopaStore.Domain.Entidad;

namespace EFSRT_RopaStore.Repositorio.Interface
{
    public interface ITrabajador
    {
        IEnumerable<Trabajador> GetTrabajadores();
        IEnumerable<Trabajador> GetTrabajadores(string nombre);
        string InsertTrabajador(Trabajador reg);
        string UpdateTrabajador(Trabajador reg);
        string DeleteTrabajador(Trabajador reg);
        Trabajador GetTrabajador(string idtrabajador);
        Trabajador GetUsuario(string correo);
    }
}
