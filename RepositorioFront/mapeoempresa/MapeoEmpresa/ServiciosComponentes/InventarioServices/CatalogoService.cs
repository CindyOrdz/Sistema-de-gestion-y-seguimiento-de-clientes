using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Inventario;
using EntidadesNegocio.ClasesDao.CatalogoDAO;
using System.Numerics;

namespace ServiciosComponentes.InventarioServices
{
    public class CatalogoService
    {
        public readonly CatalogoDAO _catalogoDAO;

        public long ObtenerIdCatalogoSegunIdElemento(long id) {
            return _catalogoDAO.ObtenerIdCatalogoSegunIdElemento(id);
        }



        public List<CatalogoInterfazGraficaVentaDTO> ObtenerCatalogos()
        {
            var Catalogos = _catalogoDAO.ObtenerCatalogos();
            Catalogos.ForEach(catalogo =>
                {
                    catalogo._elementos = _catalogoDAO.ObtenerElementosSegunCatalogos(catalogo.id);
                });
            return Catalogos;
        }
        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosSegunCatalogos(long id)
        {
            return _catalogoDAO.ObtenerElementosSegunCatalogos(id);
        }


        public List<CatalogoInterfazGraficaVentaDTO> ObtenerCatalogosPorEmpresa(long identificacionEmpresa)
        {
            return _catalogoDAO.ObtenerCatalogosPorEmpresa(identificacionEmpresa);
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosSegunCatalogosPaginado(long id, int pag, int paginarPor)
        {
            return _catalogoDAO.ObtenerElementosSegunCatalogosPaginado(id, pag, paginarPor);
        }

        public CatalogoService(CatalogoDAO catalogolDAO)
        {
            _catalogoDAO = catalogolDAO;
        }
        public async Task InsertarCatalogo(CatalogoInterfazGraficaVentaDTO catalogo)
        {
            _catalogoDAO.InsertarCatalogo(catalogo);
        }


        public List<CatalogoInterfazGraficaVentaDTO> BuscarCatalogoOElemento(string textoBuscar)
        {
            var Catalogos = _catalogoDAO.BuscarCatalogoOElemento(textoBuscar);
            return Catalogos;
        }
        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorCatalogo(int id)
        {
            var elementosCatalogo = _catalogoDAO.ObtenerElementosPorCatalogo(id);
            return elementosCatalogo;
        }

        public int ObtenerCantidadElementosSegunCatalogos(BigInteger idCatalogo) {
            return _catalogoDAO.ObtenerCantidadElementosSegunCatalogos(idCatalogo);
        }

    }
}