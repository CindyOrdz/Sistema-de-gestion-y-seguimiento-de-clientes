using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Inventario;
using EntidadesNegocio.ClasesDao.ElementosDAO;


namespace ServiciosComponentes.InventarioServices
{
    public class ElementoService
    {
        public readonly ElementoDAO _elementoDAO;
        public ElementoService(ElementoDAO elementolDAO)
        {
            _elementoDAO = elementolDAO;
        }


        public int BuscarIdPorCatalogo(string clasificacion)
        {
            return _elementoDAO.BuscarIdPorCatalogo(clasificacion);
        }

        public string BuscarCatalogoPorId(long id)
        {
            return _elementoDAO.BuscarCatalogoPorId(id);
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementos(long identificacionEmpresa)
        {
            return _elementoDAO.ObtenerElementos(identificacionEmpresa); ;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosCarrusel()
        {
            return _elementoDAO.ObtenerElementosCarrusel(); 
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorReferencia(string ref1, string ref2, string ref3, string ref4, string ref5, long identificacionEmpresa)
        {
            var Elementos = _elementoDAO.ObtenerElementosPorReferencia(ref1, ref2, ref3, ref4, ref5, identificacionEmpresa);
            return Elementos;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorCodigo(int codigo, long identificacionEmpresa)
        {
            var Elementos = _elementoDAO.ObtenerElementosPorCodigo(codigo, identificacionEmpresa);
            return Elementos;
        }

        public ElementoInterfazGraficaVentaDTO ObtenerElementoEmpresaPorCodigo(long codigo, long identificacionEmpresa)
        {
            return _elementoDAO.ObtenerElementoEmpresaPorCodigo(codigo, identificacionEmpresa);
        }

        public long GenerarCodigoElemento()
        { 
            return _elementoDAO.GenerarCodigoElemento();
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorNombre(string nombre, long identificacionEmpresa)
        {
            var Elementos = _elementoDAO.ObtenerElementosPorNombre(nombre, identificacionEmpresa);
            return Elementos;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorSett(string sett, long identificacionEmpresa)
        {
            var Elementos = _elementoDAO.ObtenerElementosPorSett(sett, identificacionEmpresa);
            return Elementos;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorMarca(string marca, long identificacionEmpresa)
        {
            var Elementos = _elementoDAO.ObtenerElementosPorMarca(marca, identificacionEmpresa);
            return Elementos;
        }
        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorTodasLasOpciones(string ref1, string ref2, string ref3, string ref4, string ref5, int codigo, string nombre, string sett, string marca, long identificacionEmpresa)
        {
            var Elementos = _elementoDAO.ObtenerElementosPorTodasLasOpciones(ref1, ref2, ref3, ref4, ref5, codigo, nombre, sett, marca, identificacionEmpresa);
            return Elementos;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorNombreYReferencias(string ref1, string ref2, string ref3, string ref4, string ref5, string nombre, long identificacionEmpresa)
        {
            var Elementos = _elementoDAO.ObtenerElementosPorNombreYReferencias(ref1, ref2, ref3, ref4, ref5, nombre, identificacionEmpresa);
            return Elementos;
        }

        public bool InsertarElemento(ElementoInterfazGraficaVentaDTO elemento)
        {
            return _elementoDAO.InsertarElemento(elemento);
        }

        public bool ActualizarElemento(ElementoInterfazGraficaVentaDTO elemento)
        {
            return _elementoDAO.ActualizarElemento(elemento);
        }

        public List<MarcasInterfazGraficaVentaDTO> ObtenerMarcas()
        {
            return _elementoDAO.ObtenerMarcas(); ;
        }

        public List<CatalogoInterfazGraficaVentaDTO> ObtenerCatalogos()
        {
            return _elementoDAO.ObtenerCatalogos(); ;
        }
        public List<UnidadInterfazGraficaVentaDTO> ObtenerUnidades()
        {
            return _elementoDAO.ObtenerUnidades(); ;
        }

        public string BuscarUnidadPorId(String id)
        {
            return _elementoDAO.BuscarUnidadPorId(id);
        }

        public string BuscarIdPorUnidad(String Unidad)
        {
            return _elementoDAO.BuscarIdPorUnidad(Unidad);
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPaginado(long idempresa, int pag, int paginarPor)
        {
            return _elementoDAO.ObtenerElementosPaginado(idempresa, pag, paginarPor);
        }

        public int ObtenerCantidadElementosTotalesPorEmpresa(long idempresa)
        {
            return _elementoDAO.ObtenerCantidadElementosTotalesPorEmpresa(idempresa);
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorNombrePaginado(string nombre, long identificacionEmpresa, int pag, int paginarPor)
        {
            return _elementoDAO.ObtenerElementosPorNombrePaginado(nombre, identificacionEmpresa, pag, paginarPor);
        }

        public int ObtenerCantidadElementosTotalesPorNombrePaginado(long idempresa, string nombre) {
            return _elementoDAO.ObtenerCantidadElementosTotalesPorNombrePaginado(idempresa, nombre);
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorCodigoPaginado(int codigo, long identificacionEmpresa, int pag, int paginarPor)
        {
            return _elementoDAO.ObtenerElementosPorCodigoPaginado(codigo, identificacionEmpresa, pag, paginarPor);
        }

        public int ObtenerCantidadElementosTotalesPorCodigoPaginado(long idempresa, int codigo)
        {
            return _elementoDAO.ObtenerCantidadElementosTotalesPorCodigoPaginado(idempresa, codigo);
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorSettPaginado(string sett, long identificacionEmpresa, int pag, int paginarPor)
        {
            return _elementoDAO.ObtenerElementosPorSettPaginado(sett, identificacionEmpresa, pag, paginarPor);
        }

        public int ObtenerCantidadElementosTotalesPorSettPaginado(long idempresa, string sett)
        {
            return _elementoDAO.ObtenerCantidadElementosTotalesPorSettPaginado(idempresa, sett);
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorMarcaPaginado(string marca, long identificacionEmpresa, int pag, int paginarPor)
        {
            return _elementoDAO.ObtenerElementosPorMarcaPaginado(marca, identificacionEmpresa, pag, paginarPor);
        }

        public int ObtenerCantidadElementosTotalesPorMarcaPaginado(long idempresa, string sett)
        {
            return _elementoDAO.ObtenerCantidadElementosTotalesPorMarcaPaginado(idempresa, sett);
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorReferenciaPaginado(String ref1, String ref2, String ref3, String ref4, String ref5, long identificacionEmpresa, int pag, int paginarPor)
        {
            return _elementoDAO.ObtenerElementosPorReferenciaPaginado(ref1, ref2, ref3, ref4, ref5, identificacionEmpresa, pag, paginarPor);
        }

        public int ObtenerCantidadElementosPorReferenciaPaginado(String ref1, String ref2, String ref3, String ref4, String ref5, long identificacionEmpresa)
        {
            return _elementoDAO.ObtenerCantidadElementosPorReferenciaPaginado(ref1, ref2, ref3, ref4, ref5, identificacionEmpresa);
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorNombreYReferenciasPaginado(string ref1, string ref2, string ref3, string ref4, string ref5, string nombre, long identificacionEmpresa, int pag, int paginarPor)
        {
            return _elementoDAO.ObtenerElementosPorNombreYReferenciasPaginado(ref1, ref2, ref3, ref4, ref5, nombre,identificacionEmpresa, pag, paginarPor);
        }

        public int ObtenerCantidadElementosPorNombreYReferenciasPaginado(string ref1, string ref2, string ref3, string ref4, string ref5, string nombre, long identificacionEmpresa)
        {
            return _elementoDAO.ObtenerCantidadElementosPorNombreYReferenciasPaginado(ref1, ref2, ref3, ref4, ref5, nombre,identificacionEmpresa);
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorTodasLasOpcionesPaginado(string ref1, string ref2, string ref3, string ref4, string ref5, int codigo, string nombre, string sett, string marca, long identificacionEmpresa, int pag, int paginarPor)
        {
            return _elementoDAO.ObtenerElementosPorTodasLasOpcionesPaginado(ref1, ref2, ref3, ref4, ref5, codigo, nombre, sett, marca, identificacionEmpresa, pag, paginarPor);
        }

        public int ObtenerCantidadElementosPorTodasLasOpcionesPaginado(string ref1, string ref2, string ref3, string ref4, string ref5, int codigo, string nombre, string sett, string marca, long identificacionEmpresa)
        {
            return _elementoDAO.ObtenerCantidadElementosPorTodasLasOpcionesPaginado(ref1, ref2, ref3, ref4, ref5, codigo, nombre, sett, marca,identificacionEmpresa);
        }

    }
}
