﻿using RopaStore.Domain.Entidad;

namespace EFSRT_RopaStore.Repositorio.Interface
{
    public interface ILote
    {
        IEnumerable<Lote> listado();
    }
}
