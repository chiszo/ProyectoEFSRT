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

        public IEnumerable<DetalleCompra> GetBoleta(string id)
        {
            return listadoDetalle().Where(x => x.codcomprapro.StartsWith(id, StringComparison.CurrentCultureIgnoreCase));
        }

        public IEnumerable<CompraProducto> listado()
        {
            List<CompraProducto> temporal = new List<CompraProducto>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_boleta", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new CompraProducto()
                    {
                        codcomprapro = dr.GetString(0),
                        fechapedido = dr.GetDateTime(1),
                        idproveedor = dr.GetString(2),
                        montoT = dr.GetDecimal(3)

                    });
                }
                dr.Close();
            }
            return temporal;
        }


        public IEnumerable<DetalleCompra> listadoDetalle()
        {
            List<DetalleCompra> temporal = new List<DetalleCompra>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_boleta_deta", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new DetalleCompra()
                    {
                        codcomprapro = dr.GetString(0),
                        idproducto = dr.GetString(1),
                        preciocompra = dr.GetDecimal(2),
                        cantidad = dr.GetInt16(3),
                        monto = dr.GetDecimal(4)

                    });
                }
                dr.Close();
            }
            return temporal;
        }

    }
}
