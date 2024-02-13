using EntidadesNegocio.Descuentos;
using EntidadesNegocio.Impuestos;
using EntidadesNegocio.VentaOnlineTradicional;
using EntidadesNegocio.Terceros;
using EntidadesNegocio.ElementosInventario;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta;
using ServiciosComponentes.MapperService;
using EntidadesNegocio.InterfazGraficaBlazorDTO.ParametrosComponentes;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.ImpuestosYDescuentos;

namespace ServiciosComponentes.VentaTradicionalService
{
    public class VentaService
    {
        private  Venta _venta;

        public VentaService(Venta venta)
        {
            _venta = venta;
        }

        public void IniciarVenta(VentaInterfazGraficaVentaDTO ventaDTO, ParametrosImpuestosInterfazGraficaVentaDTO parametosImpuestos)
        {
            ValoresImpuestos.Instancia.PorcentajeReteIva = parametosImpuestos.PorcentajeReteIva;
            ValoresImpuestos.Instancia.PorcentajeReteFuente = parametosImpuestos.PorcentajeReteFuente;
            ValoresImpuestos.Instancia.MontoDeLey = parametosImpuestos.UVT * parametosImpuestos.ValorUVT;
            ValoresDescuentos.Instancia.PorcentajeDescuentoValorCompra = 10;
            TarifasTerritorialesEmpresa.Instancia.TarifaReteIca = 0.006;


            ventaDTO.Impuestos = new List<ImpuestoInterfazGraficaVentaDTO>();

            Marca marca = new Marca();
            DateTime fechaDetalle = DateTime.Now;

            List<DetalleElemento> detallesElementosVenta = new List<DetalleElemento>();

            for (int i = 0; i < ventaDTO.DetallesVenta.Count - 1; i++) //el ultimo es la fila que se agrega por defecto, por eso no lo tomo
            {
                var elemento = Mapper.MapearElementoInterfazGraficaVentaDTOAElemento(ventaDTO.DetallesVenta[i].Elemento);
                detallesElementosVenta.Add(new DetalleElemento(elemento, ventaDTO.DetallesVenta[i].Elemento.ValorUnitario, fechaDetalle, marca));
            }


            Cliente cliente = Mapper.MapearClienteInterfazGraficaVentaDTOACliente(ventaDTO.Cliente);
            Empleado empleado = Mapper.MapearEmpleadoInterfazGraficaVentaDTOAEmpleado(ventaDTO.Empleado);
            Empresa empresaVendedora = Mapper.MapearEmpresaInterfazGraficaVentaDTOAEmpresa(ventaDTO.Empresa);


            switch (ventaDTO.TipoVenta)
            {
                case ParametrosTiposVenta.COTIZACION:
                    _venta = new Cotizacion(cliente, empresaVendedora, empleado, empleado, 0, 0, 0, ventaDTO.Cliente.RazonSocial, ventaDTO.TipoPago, "ALMACEN", "usuario", "pc");
                    break;
                case ParametrosTiposVenta.ORDEN_PEDIDO:
                    _venta = new OrdenPedido(cliente, empresaVendedora, empleado, empleado, 0, 0, 0, ventaDTO.Cliente.RazonSocial, ventaDTO.TipoPago, "ALMACEN", "usuario", "pc");
                    break;
                case ParametrosTiposVenta.FACTURA:
                    _venta = new Factura(cliente, empresaVendedora, empleado, empleado, 0, 0, 0, ventaDTO.Cliente.RazonSocial, ventaDTO.TipoPago, "ALMACEN", "usuario", "pc");
                    break;
                case ParametrosTiposVenta.POS:
                    _venta = new POS(cliente, empresaVendedora, empleado, empleado, 0, 0, 0, ventaDTO.Cliente.RazonSocial, ventaDTO.TipoPago, "ALMACEN", "usuario", "pc");
                    break;
            }



            List<Impuesto> impuestos = new List<Impuesto>();

            ventaDTO.Cliente.Impuestos.ForEach(impuesto =>
            {
                if (impuesto.Vigente.Equals("S"))
                {
                    switch (impuesto.Id)
                    {
                        case "05":
                            impuestos.Add(new ReteIva());
                            break;
                        case "06":
                            impuestos.Add(new ReteFuente());
                            break;
                        case "07":
                            impuestos.Add(new ReteIca());
                            break;
                    }
                }
            });



            _venta.AdicionarImpuestos(impuestos);
            for (int i = 0; i < ventaDTO.DetallesVenta.Count - 1; i++) //el ultimo es la fila que se agrega por defecto
            {
                var detalleVenta = ventaDTO.DetallesVenta[i];
                _venta.AdicionarElemento(detalleVenta.Item, detallesElementosVenta[i], detalleVenta.CantidadSolicitada, detalleVenta.Elemento.ValorUnitario);
            }

            ventaDTO.SubTotal = _venta.ObtenerSubtotal();
            ventaDTO.Total = _venta.ObtenerTotalVenta();
            ventaDTO.IVA = _venta.ObtenerIva();


            var impuestosVenta = _venta.ObtenerImpuestos().Where(imp => Math.Abs(imp.ObtenerValor()) > 0).ToList();

            ventaDTO.Impuestos = Mapper.MapearListaImpuestoAListaImpuestoInterfazGraficaVentaDTO(impuestosVenta);



        }


        public void CalcularDescuentoVenta(ResumenVentaDTO resumenVentaDTO)
        {
            DescuentoPorPorcentaje descuento = new DescuentoPorPorcentaje();
            resumenVentaDTO.PorcentajeDescuento = descuento.ObtenerPorcentajeDescuento(resumenVentaDTO.Descuento, _venta);
            List<EstrategiaDescuentos> estrategiaDescuentos = new List<EstrategiaDescuentos>();
            estrategiaDescuentos.Add(descuento);

            _venta.ModificarEstrategiaDescuentos(estrategiaDescuentos);
            _venta.CalcularDescuentos();

            resumenVentaDTO.SubTotal = _venta.ObtenerSubtotal();
            resumenVentaDTO.Total = _venta.ObtenerTotalVenta();

        
        }

        public void CancelarDescuentoVenta()
        {
            _venta.CancelarDescuentos();
        
        }

    }
}
