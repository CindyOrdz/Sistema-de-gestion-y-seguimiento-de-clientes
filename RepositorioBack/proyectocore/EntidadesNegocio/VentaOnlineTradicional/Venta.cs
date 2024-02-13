using EntidadesNegocio.Descuentos;
using EntidadesNegocio.Terceros;
using EntidadesNegocio.Pagos;
using EntidadesNegocio.Impuestos;
using System.Numerics;
using EntidadesNegocio.ElementosInventario;

namespace EntidadesNegocio.VentaOnlineTradicional
{

    public abstract class Venta
    {
        protected Tercero _cliente;
        protected Tercero _empresaVendedora;
        protected Empleado _responsable;
        protected Empleado _responsableComision;
        protected Double id;
        protected Double numero;
        protected String tipoVenta;
        protected DateTime fecha;
        protected DateTime hora;
        


        private String tipoPago;
        protected List<Pago> _formasDePago;
        protected List<Impuesto> _impuestos;

        protected Double subTotal;
        protected Double iva;
        protected Double total;
        protected Double totalCosto;

        /*

            private String cancelado;    
        */

        protected Double valorEfectivo;
        protected Double valorCredito;
        protected Double valorCheque;
        protected Double valorTarjeta;


        protected String orden;
        protected String empresa;
        protected String origen;
        protected String impreso;
        protected String tipoPrecio;
        protected Double reteFuente;
        protected Double reteIva;
        protected Double reteIca;
        protected String entregarEn;
        protected String legalizado;
        protected Double tasaCambio;
        protected String autorizarAnular;
        protected String anulado;
        protected DateTime fechaAnulacion;
        protected String ventaHabilitadaNuevamente;
        protected DateTime fechaHoraHabilitacion;
        protected String usuario;
        protected String pc;
        protected List<DetalleVenta> _detallesVenta;
        protected List<EstrategiaDescuentos> _estrategiaDescuentos;
        protected Double descuento;
        protected Double porcentajeDescuento;




        protected abstract Double GenerarID();
        protected abstract Boolean AnularVenta();
        protected abstract Venta CrearVentaAPartirDeVentaAnulada();


        public Venta(Tercero cliente, Tercero empresaVendedora, Empleado responsable, Empleado responsableComision, Double totalCosto, Double descuento, Double porcentajeDescuento, String empresa, String tipoPrecio, String entregarEn, String usuario, String pc)
        {
            //Inicializar();
            _empresaVendedora = empresaVendedora;
            _cliente = cliente;
            _responsable = responsable;
            _responsableComision = responsableComision;
            _impuestos = new List<Impuesto>();
            _formasDePago = new List<Pago>();
            _estrategiaDescuentos = new List<EstrategiaDescuentos>();
            _detallesVenta = new List<DetalleVenta>();
            _impuestos.Add(new Iva()); 
            this.totalCosto = totalCosto;
            this.descuento = descuento;
            this.porcentajeDescuento = porcentajeDescuento;
            this.empresa = empresa;
            this.tipoPrecio = tipoPrecio;
            this.entregarEn = entregarEn;
            this.usuario = usuario;
            this.pc = pc;
           
        }

        public Venta()
        {
            Inicializar();
        }

        private void Inicializar()
        {
            descuento = 0.0;

            _estrategiaDescuentos = new List<EstrategiaDescuentos>();
            _detallesVenta = new List<DetalleVenta>();
            _cliente = null;
            _responsable = null;
            _responsableComision = null;
            id = 0.0;
            numero = 0.0;
            tipoVenta = "CONTADO";
            fecha = new DateTime();
            hora = new DateTime();



            //private String tipo_pago;
            _formasDePago = new List<Pago>();
            _impuestos = new List<Impuesto>();


            _impuestos.Add(new Iva());
            _formasDePago.Add(new ContadoEfectivo(20.0));
            _formasDePago.Add(new ContadoCheque(30.0));
            _formasDePago.Add(new ContadoTarjeta(50.0));

            subTotal = 0.0;
            iva = 0.0;
            total = 0.0;
            totalCosto = 0.0;


            descuento = 0.0;
            porcentajeDescuento = 0.0;

            orden = "---";
            empresa = "---";
            origen = "---";
            impreso = "---";
            tipoPrecio = "---";

            reteFuente = 0.0;
            reteIva = 0.0;
            reteIca = 0.0;


            entregarEn = "almacen";
            legalizado = "N";



            tasaCambio = 0.0;

            autorizarAnular = "N";
            anulado = "N";
            fechaAnulacion = new DateTime();
            ventaHabilitadaNuevamente = "N";
            fechaHoraHabilitacion = new DateTime();
            usuario = "---";
            pc = "---";


        }

        public Boolean AgregarDescuentoVenta()
        {
            Boolean valor = false;



            return valor;
        }


        public Boolean CambiarResponsableVenta(Empleado empleado)
        {
            Boolean valor = true;
            _responsable = empleado;
            _responsableComision = empleado;
            return valor;
        }

        public Boolean CambiarTipoPago(String tipo)
        {

            Boolean valor = false;

            switch (tipo)
            {
                case "CONTADO":
                    tipoPago = tipo;
                    ModificarInformacionCartera();
                    valor = true;
                    break;
                case "CREDITO":
                    tipoPago = tipo;//CONTADO - CREDITO
                    ModificarInformacionCartera();
                    valor = true;
                    break;
            }

            return valor;
        }

        private void ModificarInformacionCartera()
        {
        }


        
        public Boolean ModificarFormaPago(List<Pago> formaDepago)
        {
            Boolean valor = false;

            Double total = 0.0;

            foreach (Pago p in formaDepago)
            {
                total = total + p.ObtenerValor();

                if (p is ContadoCheque)
                {
                    valorCheque = p.ObtenerValor();
                }
                if (p is ContadoEfectivo)
                {
                    valorEfectivo = p.ObtenerValor();
                }
                if (p is ContadoTarjeta)
                {
                    valorTarjeta = p.ObtenerValor();
                }
                if (p is Credito)
                {
                    valorCredito = p.ObtenerValor();
                }

            }

            if (total >= this.total)
            {
                valor = true;
                _formasDePago = formaDepago;
            }
            else
            {
                valorCheque = 0;
                valorEfectivo = 0;
                valorTarjeta = 0;
                valorCredito = 0;
            }

            return valor;
        }


       

        public Boolean CambiarClienteVenta(Tercero cliente)
        {
            Boolean valor = true;
            _cliente = cliente;
            return valor;
        }

        
        public void AdicionarElemento(BigInteger item, DetalleElemento detalleElemento,Double cantidad, Double valor)
        {
            DetalleVenta detalleVenta = new DetalleVenta(item,detalleElemento, cantidad, valor);

            _impuestos.ForEach(impuesto =>
            {
                var impuestoDetalleVenta = impuesto.Clonar();
                detalleVenta.AdicionarImpuesto(impuestoDetalleVenta);

            });

            _detallesVenta.Add(detalleVenta);
            
            CalcularSubtotal();

        }

        public void RemoverArticulo(BigInteger item)
        {
            foreach (var detalleVenta in _detallesVenta)
            {
                if (detalleVenta.ObtenerItem() == item)
                {
                    _detallesVenta.Remove(detalleVenta);
                }
            }
            CalcularSubtotal();
        }

        private void CalcularSubtotal()
        {
            subTotal = 0;

            foreach (var detalleVenta in _detallesVenta)
            { 
                subTotal += detalleVenta.ObtenerSubTotal();
            }
            //subTotal = OperacionesDian.RedondeoDIAN(subTotal,2);

            CalcularDescuentos();
            CalcularImpuestos();
            
            
        }


        private void CalcularImpuestos()
        {
            Double valorImpuestos = 0;
            foreach (var impuesto in _impuestos)
            {
                impuesto.CalcularImpuesto(this);
                valorImpuestos += impuesto.ObtenerValor();
                switch (impuesto)
                {
                    case Iva:
                        iva = impuesto.ObtenerValor();
                        break;
                    case ReteIva:
                        reteIva = Math.Abs(impuesto.ObtenerValor());
                        break;
                    case ReteFuente:
                        reteFuente = Math.Abs(impuesto.ObtenerValor());
                        break;
                    case ReteIca:
                        reteIca = Math.Abs(impuesto.ObtenerValor());
                        break;
                }
            }


            if ((_cliente.ConMontoDeLey().Equals("S") && (this.ObtenerSubtotal() + this.ObtenerIva()) > ValoresImpuestos.Instancia.MontoDeLey) || (_cliente.ConMontoPersonalizado().Equals("S") && (this.ObtenerSubtotal() + this.ObtenerIva()) > ValoresImpuestos.Instancia.MontoPersonalizado))
            {
                foreach (var detalle in _detallesVenta)
                {
                    List<Impuesto> impuestosDetalle = detalle.ObtenerImpuestos();
                    Double valorImpuestosDetalle = 0;

                    foreach (var impuestoDetalle in impuestosDetalle)
                    {
                        impuestoDetalle.CalcularImpuestoDetalleVenta(detalle);
                        valorImpuestosDetalle += impuestoDetalle.ObtenerValor();
                    }

                    Double totalDetalleVenta = detalle.ObtenerSubTotal() + valorImpuestosDetalle;
                    detalle.ModificarTotalDetalleVenta(totalDetalleVenta);
                }
            }
            
            total = subTotal + valorImpuestos;

        }

        public void CalcularDescuentos()
        {
            descuento = 0;
            porcentajeDescuento = 0;    
            
            foreach (var estrategiaDescuento in _estrategiaDescuentos)
            {
                descuento += estrategiaDescuento.ObtenerTotalDescuento(this);
                porcentajeDescuento += estrategiaDescuento.ObtenerPorcentajeDescuento();
                  
            }
            subTotal = subTotal - descuento;
        }

        public Double TotalMediosDePago()
        {
            Double total = 0.0;

            foreach (var pago in _formasDePago)
                total += pago.ObtenerValor();

            return total;
        }


        public Double TotalPago()
        {

            return total;
        }

        public override string ToString()
        {
            return "\nVenta{" + "\ncliente=" + _cliente + ", \nresponsable=" + _responsable + ", \nresponsable_comision=" + _responsableComision + ", "
                    + "\nid=" + id + ", numero=" + numero + ", tipo_venta=" + tipoVenta + ", fecha=" + fecha + ", hora=" + hora + ", "
                    + "\ntipo_pago=" + _formasDePago + ", \nsub_total=" + subTotal.ToString("C") + ", iva=" + iva.ToString("C") + ", total=" + total.ToString("C") + ", totalcosto=" + totalCosto.ToString("C") + ", "
                    + "\ndescuento=" + descuento.ToString("C") + ", porcentaje_descuento=" + porcentajeDescuento + ", orden=" + orden + ", empresa=" + empresa + ", "
                    + "\norigen=" + origen + ", impreso=" + impreso + ", tipo_precio=" + tipoPrecio + ", "
                    + "\nretefuente=" + reteFuente.ToString("C") + ", reteiva=" + reteIva.ToString("C") + ", reteica=" + reteIca.ToString("C") + ", "
                    + "\n entregarEn=" + entregarEn + ", legalizado=" + legalizado + ", "
                    + "\ntasa_cambio=" + tasaCambio + ", autorizar_anular=" + autorizarAnular + ", anulado=" + anulado + ", fechaanulacion=" + fechaAnulacion + ","
                    + "\n venta_habilitada_nuevamente=" + ventaHabilitadaNuevamente + ", fechaHoraHabilitacion=" + fechaHoraHabilitacion + ", "
                    + "\nDetalle=" + _detallesVenta + ", "
                    + "\nusuario=" + usuario + ", pc=" + pc + ", descuento=" + descuento + '}' + "\n";
        }

        



        public Double ObtenerSubtotal()
        {
            return subTotal;
        }

        public Double ObtenerTotalVenta()
        {
            CalcularImpuestos();
            return total;
        }

        public Double ObtenerIva()
        {
            return iva;
        }

        public List<DetalleVenta> ObtenerDetallesVenta()
        {
            return _detallesVenta;

        }

        public void AdicionarImpuesto(Impuesto impuesto)
        { 
            _impuestos.Add(impuesto);   
            
        }

        public void AdicionarImpuestos(List<Impuesto> impuestos)
        {

            impuestos.ForEach( impuesto =>
            {
                _impuestos.Add(impuesto);

            });

        }
        


        public Tercero ObtenerEmpresaVendedora()
        {
            return _empresaVendedora;
        }

        public void ModificarEstrategiaDescuentos(List<EstrategiaDescuentos> estrategiaDescuentos)
        {
            estrategiaDescuentos.ForEach(descuento =>
            {
                _estrategiaDescuentos.Add(descuento);
            });
        }

        public void CancelarDescuentos()
        {
            _estrategiaDescuentos.Clear();
            CalcularSubtotal();
        }


        public List<Impuesto> ObtenerImpuestos() => _impuestos;
        public List<EstrategiaDescuentos> ObtenerEstrategiaDescuentos() => _estrategiaDescuentos;

        public Tercero ObtenerCliente() => _cliente;


    }
}
 