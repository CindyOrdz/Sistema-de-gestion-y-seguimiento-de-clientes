using EntidadesNegocio.Descuentos;
using EntidadesNegocio.ElementosInventario;
using EntidadesNegocio.Impuestos;
using EntidadesNegocio.LugarProcedencia;
using EntidadesNegocio.Terceros;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.ImpuestosYDescuentos;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Inventario;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiciosComponentes.MapperService
{
    public static class Mapper
    {

        public static Elemento MapearElementoInterfazGraficaVentaDTOAElemento(ElementoInterfazGraficaVentaDTO elementoDTO) =>
            new Elemento
            (
                new BigInteger(elementoDTO.CodigoArticulo),
                elementoDTO.TipoElemento,
                elementoDTO.Nombre,
                MapearUnidadInterfazGraficaVentaDTOAUnidad(elementoDTO.Unidad),
                elementoDTO.Ref1,
                elementoDTO.Ref2,
                elementoDTO.Ref3,
                elementoDTO.Ref4,
                elementoDTO.Ref5,
                elementoDTO.Sett,
                elementoDTO.Valor,
                elementoDTO.Tipo,
                elementoDTO.UltimaVenta,
                elementoDTO.CantidadDisponibleAlmacen,
                elementoDTO.CantidadDisponibleBodega,
                elementoDTO.CantidadVendidos,
                elementoDTO.Ref11,
                elementoDTO.Ref12,
                elementoDTO.Ref13,
                elementoDTO.Ref14,
                elementoDTO.Ref15,
                elementoDTO.Ref16,
                elementoDTO.PorcentajeIva
            );

        public static Empresa MapearEmpresaInterfazGraficaVentaDTOAEmpresa(EmpresaInterfazGraficaVentaDTO empresaDTO) =>
            new Empresa
            (
                empresaDTO.Identificacion,
                empresaDTO.TipoDocumento.Codigo,
                empresaDTO.RazonSocial,
                MapearUbicacionInterfazGraficaVentaDTOAUbicacion(empresaDTO.Ubicacion),
                empresaDTO.Telefono,
                empresaDTO.Email,
                empresaDTO.Celular

            );

        public static Empleado MapearEmpleadoInterfazGraficaVentaDTOAEmpleado(EmpleadoInterfazGraficaVentaDTO empleadoDTO) =>
            new Empleado
            (
                identificacion: empleadoDTO.Identificacion,
                tipo: empleadoDTO.TipoDocumento.Codigo,
                nombre1: empleadoDTO.Nombre1,
                nombre2: empleadoDTO.Nombre2,
                apellido1: empleadoDTO.Apellido1,
                apellido2: empleadoDTO.Apellido2,
                telefono: empleadoDTO.Telefono,
                email: empleadoDTO.Email,
                celular: empleadoDTO.Celular,
                direccion: empleadoDTO.Direccion
            );

        public static Cliente MapearClienteInterfazGraficaVentaDTOACliente(TerceroInterfazGraficaDTO clienteDTO) =>
            new Cliente
            (
                identificacion: clienteDTO.Identificacion,
                tipo: clienteDTO.TipoDocumento.Codigo,
                nombre1: clienteDTO.Nombre1,
                nombre2: clienteDTO.Nombre2,
                apellido1: clienteDTO.Apellido1,
                apellido2: clienteDTO.Apellido2,
                telefono: clienteDTO.Telefono,
                email: clienteDTO.Email,
                celular: clienteDTO.Celular,
                direccion: clienteDTO.Direccion,
                conMontoDeLey: clienteDTO.ConMontoDeLey,
                ConMontoPersonalizado: clienteDTO.ConMontoPersonalizado,
                montoPersonalizado: clienteDTO.MontoPersonalizado
            );

        public static Unidad MapearUnidadInterfazGraficaVentaDTOAUnidad(UnidadInterfazGraficaVentaDTO unidadDTO) =>
            new Unidad
            (
                codigo: unidadDTO.Id,
                descripcion: unidadDTO.Unidad

            );

        public static Pais MapearPaisInterfazGraficaVentaDTOAPais(PaisInterfazGraficaVentaDTO paisDTO) =>
            new Pais
            (
                paisDTO.Codigo,
                paisDTO.Nombre
            );

        public static Ciudad MapearCiudadInterfazGraficaVentaDTOACiudad(CiudadInterfazGraficaVentaDTO ciudadDTO) =>
            new Ciudad
            (
                ciudadDTO.Pais,
                ciudadDTO.Departamento,
                ciudadDTO.Codigo,
                ciudadDTO.Nombre
            );

        public static DepartamentoProvincia MapearDepartamentoProvinciaInterfazGraficaVentaDTOADepartamentoProvincia(DepartamentoProvinciaInterfazGraficaVentaDTO departamentoProvinciaDTO) =>
            new DepartamentoProvincia
            (
                departamentoProvinciaDTO.Pais,
                departamentoProvinciaDTO.Codigo,
                departamentoProvinciaDTO.Nombre,
                departamentoProvinciaDTO.CodigoIso
            );

        public static Direccion MapearDireccionInterfazGraficaVentaDTOADireccion(DireccionInterfazGraficaVentaDTO direccionDTO) =>
            new Direccion
            (
                direccionDTO.CodigoPostal,
                direccionDTO.Calle,
                direccionDTO.Numero,
                direccionDTO.Ciudad,
                direccionDTO.ProvinciaEstado,
                direccionDTO.Pais,
                direccionDTO.InformacionAdicional
            );

        public static Ubicacion MapearUbicacionInterfazGraficaVentaDTOAUbicacion(UbicacionInterfazGraficaVentaDTO ubicacionDTO) =>
            new Ubicacion
            (
                MapearPaisInterfazGraficaVentaDTOAPais(ubicacionDTO.Pais),
                MapearDepartamentoProvinciaInterfazGraficaVentaDTOADepartamentoProvincia(ubicacionDTO.DepartamentoProvincia),
                MapearCiudadInterfazGraficaVentaDTOACiudad(ubicacionDTO.Ciudad)

            );

        public static ImpuestoInterfazGraficaVentaDTO MapearImpuestoAImpuestoInterfazGraficaVentaDTO(Impuesto impuesto) =>
            new ImpuestoInterfazGraficaVentaDTO
            {
                Id = impuesto.ObtenerId(),
                Nombre = impuesto.ObtenerNombre(),
                Porcentaje = impuesto.ObtenerPorcentaje(),
                Valor = impuesto.ObtenerValor(),
            };

        public static List<ImpuestoInterfazGraficaVentaDTO> MapearListaImpuestoAListaImpuestoInterfazGraficaVentaDTO(List<Impuesto> impuestos)
        {
            List<ImpuestoInterfazGraficaVentaDTO> impuestosVenta = new List<ImpuestoInterfazGraficaVentaDTO>();
            impuestos.ForEach(impuesto =>
            {
                impuestosVenta.Add(MapearImpuestoAImpuestoInterfazGraficaVentaDTO(impuesto));

            });

            return impuestosVenta;
        }

        public static DescuentoInterfazGraficaVentaDTO MapearEstrategiaDescuentosADescuentoInterfazGraficaVentaDTO(EstrategiaDescuentos estrategiaDescuento) =>
            new DescuentoInterfazGraficaVentaDTO
            {
                Nombre = estrategiaDescuento.ObtenerNombre(),
                Porcentaje = estrategiaDescuento.ObtenerPorcentajeDescuento(),
                Valor = estrategiaDescuento.ObtenerValor(),

            };

        public static List<DescuentoInterfazGraficaVentaDTO> MapearListaEstrategiaDescuentosAListaDescuentoInterfazGraficaVentaDTO(List<EstrategiaDescuentos> estrategiasDescuento)
        {
            List<DescuentoInterfazGraficaVentaDTO> descuentos = new List<DescuentoInterfazGraficaVentaDTO>();
            estrategiasDescuento.ForEach(estrategiaDescuento =>
            {
                descuentos.Add(MapearEstrategiaDescuentosADescuentoInterfazGraficaVentaDTO(estrategiaDescuento));

            });

            return descuentos;
        }

        public static VentaInterfazGraficaVentaDTO ClonarVentaInterfazGraficaVentaDTO(VentaInterfazGraficaVentaDTO ventaDTO) =>
            new VentaInterfazGraficaVentaDTO
            {
                Anulada = ventaDTO.Anulada,
                Cliente = ventaDTO.Cliente,
                DescripcionOrden = ventaDTO.DescripcionOrden,
                Descuentos = ventaDTO.Descuentos,
                DestinoProducto = ventaDTO.DestinoProducto,
                DetallesVenta = ventaDTO.DetallesVenta,
                Empleado = ventaDTO.Empleado,
                Empresa = ventaDTO.Empresa,
                FechaVenta = ventaDTO.FechaVenta,
                Hora = ventaDTO.Hora,
                Impuestos = ventaDTO.Impuestos,
                IVA = ventaDTO.IVA,
                SubTotal = ventaDTO.SubTotal,
                TipoPago = ventaDTO.TipoPago,
                TipoPrecio = ventaDTO.TipoPrecio,
                TipoVenta = ventaDTO.TipoVenta,
                Total = ventaDTO.Total,
                Descuento = ventaDTO.Descuento,
                PorcentajeDescuento = ventaDTO.PorcentajeDescuento,
                ValorCheque = ventaDTO.ValorCheque, 
                ValorCredito = ventaDTO.ValorCredito,
                ValorEfectivo = ventaDTO.ValorEfectivo,
                ValorTarjeta = ventaDTO.ValorTarjeta,
                PC = ventaDTO.PC,
                Origen  = ventaDTO.Origen,
                
            };

        public static ElementoInterfazGraficaVentaDTO ClonarElementoInterfazGraficaVentaDTO(ElementoInterfazGraficaVentaDTO elemento) {
            return new ElementoInterfazGraficaVentaDTO
            {
                CodigoArticulo = elemento.CodigoArticulo,
                Tipo = elemento.Tipo,
                TipoElemento = elemento.TipoElemento,
                Nombre = elemento.Nombre,
                Ref1 = elemento.Ref1,
                Ref2 = elemento.Ref2,
                Ref3 = elemento.Ref3,
                Ref4 = elemento.Ref4,
                Ref5 = elemento.Ref5,
                Ref11 = elemento.Ref11,
                Ref12 = elemento.Ref12,
                Ref13 = elemento.Ref13,
                Ref14 = elemento.Ref14,
                Ref15 = elemento.Ref15,
                Ref16 = elemento.Ref16,
                Sett = elemento.Sett,
                Unidad = elemento.Unidad,
                CantidadVendidos = elemento.CantidadVendidos,
                Referencia = elemento.Referencia,
                Marca = elemento.Marca,
                IdMarca = elemento.IdMarca,
                UltimaVenta = elemento.UltimaVenta,
                ValorUnitario = elemento.ValorUnitario,
                Valor = elemento.Valor,
                CantidadDisponibleBodega = elemento.CantidadDisponibleBodega,
                CantidadDisponibleAlmacen = elemento.CantidadDisponibleAlmacen,
                CantidadSolicitadaEnCarrito = elemento.CantidadSolicitadaEnCarrito,
                PorcentajeIva = elemento.PorcentajeIva,
                IdEmpresa = elemento.IdEmpresa,
                Empresa = elemento.Empresa,
                url_imagen = elemento.url_imagen,
                Catalogo = elemento.Catalogo,
                IdCatalogo = elemento.IdCatalogo
            };
        }



    }
}
