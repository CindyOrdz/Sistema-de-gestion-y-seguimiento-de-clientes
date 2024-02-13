using EntidadesNegocio.Terceros;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Inventario;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta;



namespace EntidadesNegocio.InterfazGraficaBlazorDTO
{
    public static class Repository
    {


        private static List<ElementoInterfazGraficaVentaDTO> elementos = new List<ElementoInterfazGraficaVentaDTO>
        {
            new ElementoInterfazGraficaVentaDTO
            {
                CodigoArticulo = 1,
                Nombre = "RODILLO",
                Referencia = "-.59162.-.-",
                Marca = "TIMKEN",
                Valor = 250000,
                ValorUnitario = 210000,
                PorcentajeIva = 10,
                CantidadDisponibleAlmacen = 5,
                CantidadDisponibleBodega = 10,
                CantidadVendidos = 5,
                Ref1 = "-.",
                Ref2 = "591",
                Ref3 = "62.",
                Ref4 = "-.-",
                Ref5 = "4K",
                Ref11 = 2,
                Ref12= 3,
                Ref13= 4,
                Ref14= 5,
                Ref15= 6,
                Ref16= 7,
                Sett = "A3",
                Tipo = "Normal",
                TipoElemento = "Construcción",
                UltimaVenta= 0,
                Unidad = new UnidadInterfazGraficaVentaDTO
                {
                    Id = "01",
                    Unidad = "Unidades",
                },


            },
            new ElementoInterfazGraficaVentaDTO
            {
                CodigoArticulo = 2,
                Nombre = "TALADRO",
                Referencia = "SDU39",
                Marca = "CATERPILLAR",
                Valor = 120000,
                ValorUnitario = 150000,
                PorcentajeIva =19,
                CantidadDisponibleBodega = 5,
                CantidadDisponibleAlmacen = 2,
                CantidadVendidos = 5,
                Ref1 = "R1",
                Ref2 = "S12",
                Ref3 = "DN3.",
                Ref4 = "-.-",
                Ref5 = ",LJ2J",
                Ref11 = 2,
                Ref12= 3,
                Ref13= 4,
                Ref14= 5,
                Ref15= 6,
                Ref16= 7,
                Sett = "A3",
                Tipo = "Normal",
                TipoElemento = "Construcción",
                UltimaVenta= 0,
                Unidad = new UnidadInterfazGraficaVentaDTO
                {
                    Id = "01",
                    Unidad = "Unidades",
                },

            },
            new ElementoInterfazGraficaVentaDTO
            {
                CodigoArticulo = 3,
                Referencia = "DJE78",
                Marca = "CATERPILLAR",
                Valor = 20000,
                ValorUnitario = 24000,
                PorcentajeIva = 20,
                CantidadDisponibleAlmacen = 2,
                CantidadDisponibleBodega = 10,
                CantidadVendidos = 5,
                Nombre = "MARTILLO",
                Ref1 = "WE1",
                Ref2 = "FG12",
                Ref3 = "F3.",
                Ref4 = "-TY-",
                Ref5 = "FMCI",
                Ref11 = 2,
                Ref12= 3,
                Ref13= 4,
                Ref14= 5,
                Ref15= 6,
                Ref16= 7,
                Sett = "B3",
                Tipo = "Normal",
                TipoElemento = "Construcción",
                UltimaVenta= 0,
                Unidad = new UnidadInterfazGraficaVentaDTO
                {
                    Id = "01",
                    Unidad = "Unidades",
                },


            },
            new ElementoInterfazGraficaVentaDTO
            {
                CodigoArticulo = 4,
                Referencia = "39DJSK",
                Marca = "BELLOTA",
                Valor = 20000,
                ValorUnitario = 63000,
                PorcentajeIva = 5,
                CantidadDisponibleBodega = 5,
                CantidadDisponibleAlmacen = 6,
                CantidadVendidos = 5,
                Nombre = "ESPATULA",
                Ref1 = "ESP1",
                Ref2 = "ESP2",
                Ref3 = "ESP3",
                Ref4 = "ESP4",
                Ref5 = "ESP5",
                Ref11 = 2,
                Ref12= 3,
                Ref13= 4,
                Ref14= 5,
                Ref15= 6,
                Ref16= 7,
                Sett = "C3",
                Tipo = "Normal",
                TipoElemento = "Construcción",
                UltimaVenta= 0,
                Unidad = new UnidadInterfazGraficaVentaDTO
                {
                    Id = "01",
                    Unidad = "Unidades",
                },


            },
            new ElementoInterfazGraficaVentaDTO
            {
                CodigoArticulo = 5,
                Referencia = "18JFS7",
                Marca = "DEWALT",
                Valor = 180000,
                ValorUnitario = 150000,
                PorcentajeIva = 10,
                CantidadDisponibleAlmacen = 20,
                CantidadDisponibleBodega = 10,
                CantidadVendidos = 5,
                Nombre = "SIERRA",
                Ref1 = "SRR1",
                Ref2 = "SRR2",
                Ref3 = "SRR3",
                Ref4 = "SRR4",
                Ref5 = "SRR5",
                Ref11 = 2,
                Ref12= 3,
                Ref13= 4,
                Ref14= 5,
                Ref15= 6,
                Ref16= 7,
                Sett = "D3",
                Tipo = "Normal",
                TipoElemento = "Construcción",
                UltimaVenta= 0,
                Unidad = new UnidadInterfazGraficaVentaDTO
                {
                    Id = "01",
                    Unidad = "Unidades",
                },

            },
            new ElementoInterfazGraficaVentaDTO
            {
                CodigoArticulo = 6,
                Referencia = "45SKW2",
                Marca = "IRWIN",
                Valor = 70000,
                ValorUnitario = 60000,
                CantidadDisponibleBodega = 7,
                CantidadDisponibleAlmacen = 14,
                PorcentajeIva =15,
                CantidadVendidos = 5,
                Nombre = "NIVEL",
                Ref1 = "NVL1",
                Ref2 = "NVL2",
                Ref3 = "NVL3",
                Ref4 = "NVL4",
                Ref5 = "NVL5",
                Ref11 = 2,
                Ref12= 3,
                Ref13= 4,
                Ref14= 5,
                Ref15= 6,
                Ref16= 7,
                Sett = "E3",
                Tipo = "Normal",
                TipoElemento = "Construcción",
                UltimaVenta= 0,
                Unidad = new UnidadInterfazGraficaVentaDTO
                {
                    Id = "01",
                    Unidad = "Unidades",
                },


            },
            new ElementoInterfazGraficaVentaDTO
            {
                CodigoArticulo = 7,
                Referencia = "3AFM43",
                Marca = "BELLOTA",
                Valor = 22000,
                ValorUnitario = 19000,
                CantidadDisponibleAlmacen = 3,
                CantidadDisponibleBodega=4,
                PorcentajeIva =12,
                CantidadVendidos = 5,
                Nombre = "ESCOBA",
                Ref1 = "ESCB1",
                Ref2 = "ESCB2",
                Ref3 = "ESCB3",
                Ref4 = "ESCB4",
                Ref5 = "ESCB5",
                Ref11 = 2,
                Ref12= 3,
                Ref13= 4,
                Ref14= 5,
                Ref15= 6,
                Ref16= 7,
                Sett = "F3",
                Tipo = "Normal",
                TipoElemento = "Construcción",
                UltimaVenta= 0,
                Unidad = new UnidadInterfazGraficaVentaDTO
                {
                    Id = "01",
                    Unidad = "Unidades",
                },

            },
            new ElementoInterfazGraficaVentaDTO
            {
                CodigoArticulo = 8,
                Referencia = "MEK292",
                Marca = "BELLOTA",
                Valor = 22400,
                ValorUnitario = 19820,
                CantidadDisponibleBodega = 23,
                CantidadDisponibleAlmacen = 12,
                PorcentajeIva = 2,
                CantidadVendidos = 5,
                Nombre = "RASTRILLO",
                Ref1 = "RSTRL1",
                Ref2 = "RSTRL1",
                Ref3 = "RSTRL1",
                Ref4 = "RSTRL1",
                Ref5 = "RSTRL1",
                Ref11 = 2,
                Ref12= 3,
                Ref13= 4,
                Ref14= 5,
                Ref15= 6,
                Ref16= 7,
                Sett = "G3",
                Tipo = "Normal",
                TipoElemento = "Construcción",
                UltimaVenta= 0,
                Unidad = new UnidadInterfazGraficaVentaDTO
                {
                    Id = "01",
                    Unidad = "Unidades",
                },


            }

        };
        private static List<ClienteInterfazGraficaVentaDTO> clientes = new List<ClienteInterfazGraficaVentaDTO>
        {
            new ClienteInterfazGraficaVentaDTO
            {
                Identificacion = 112273837,
                Nombre1 = "Andres",
                Nombre2 = "Felipe",
                Apellido1 = "Jaimes",
                Apellido2 = "Frias",
                Celular = "322345332",
                Email = "AndresJaimes@gmail.com",
                EnumTipoDocumento = EnumTipoDocumento.CedulaCiudadania,
                Direccion = "Villavicencio",
                Telefono = "984748484",
                Tipo = 2,
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50001",
                        Nombre = "Villavicencio",
                        Departamento = "Meta",
                        Pais = "Colombia",
                        TarifaIca = 12,
                        TarifaReteIca = 0.006,

                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "50",
                        Pais = "CO",
                        Nombre = "META",
                        CodigoIso ="MET",

                    },
                    Direccion = new DireccionInterfazGraficaVentaDTO
                    {
                        Calle = "54 E",
                        Ciudad = "Villavicencio",
                        Pais  = "Colombia",
                        CodigoPostal = "50001",
                        InformacionAdicional = "CASA 7 SEGUNDO PISO",
                        Numero = "22",
                        ProvinciaEstado = "Meta",

                    },
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA",

                    }

                },

            },
            new ClienteInterfazGraficaVentaDTO
            {
                Identificacion = 222222222,
                Nombre1 = "Pedro",
                Nombre2 = "Antonio",
                Apellido1 = "González",
                Apellido2 = "Ramírez",
                Celular = "300111111",
                Email = "pedro.gonzalez@gmail.com",
                EnumTipoDocumento = EnumTipoDocumento.CedulaCiudadania,
                Direccion = "Cali",
                Telefono = "987654321",
                Tipo = 1,
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50002",
                        Nombre = "Bogotá",
                        Departamento = "Cundinamarca",
                        Pais = "Colombia",
                        TarifaIca = 10,
                        TarifaReteIca = 0.005,
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "11",
                        Pais = "CO",
                        Nombre = "CUNDINAMARCA",
                        CodigoIso = "CUN",
                    },
                    Direccion = new DireccionInterfazGraficaVentaDTO
                    {
                        Calle = "Calle 100",
                        Ciudad = "Bogotá",
                        Pais = "Colombia",
                        CodigoPostal = "111111",
                        InformacionAdicional = "APTO 302",
                        Numero = "35",
                        ProvinciaEstado = "Cundinamarca",
                    },
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA",
                    }
                },
            },
            new ClienteInterfazGraficaVentaDTO
            {
                Identificacion = 333333333,
                Nombre1 = "Laura",
                Apellido1 = "Gomez",
                Celular = "310222222",
                Email = "laura.gomez@gmail.com",
                EnumTipoDocumento = EnumTipoDocumento.CedulaCiudadania,
                Direccion = "Barranquilla",
                Telefono = "123456789",
                Tipo = 2,
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50003",
                        Nombre = "Medellín",
                        Departamento = "Antioquia",
                        Pais = "Colombia",
                        TarifaIca = 8,
                        TarifaReteIca = 0.004,
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "05",
                        Pais = "CO",
                        Nombre = "ANTIOQUIA",
                        CodigoIso = "ANT",
                    },
                    Direccion = new DireccionInterfazGraficaVentaDTO
                    {
                        Calle = "Carrera 70",
                        Ciudad = "Medellín",
                        Pais = "Colombia",
                        CodigoPostal = "050001",
                        InformacionAdicional = "APTO 101",
                        Numero = "10",
                        ProvinciaEstado = "Antioquia",
                    },
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA",
                    }
                },

            },
            new ClienteInterfazGraficaVentaDTO
            {
                Identificacion = 444444444,
                Nombre1 = "Carlos",
                Nombre2 = "Alberto",
                Apellido1 = "Rodríguez",
                Apellido2 = "Pérez",
                Celular = "300333333",
                Email = "carlos.rodriguez@gmail.com",
                EnumTipoDocumento = EnumTipoDocumento.CedulaCiudadania,
                Direccion = "Medellín",
                Telefono = "987654321",
                Tipo = 1,
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50004",
                        Nombre = "Cali",
                        Departamento = "Valle del Cauca",
                        Pais = "Colombia",
                        TarifaIca = 15,
                        TarifaReteIca = 0.007,
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "76",
                        Pais = "CO",
                        Nombre = "VALLE DEL CAUCA",
                        CodigoIso = "VCA",
                    },
                    Direccion = new DireccionInterfazGraficaVentaDTO
                    {
                        Calle = "Avenida 3N",
                        Ciudad = "Cali",
                        Pais = "Colombia",
                        CodigoPostal = "760001",
                        InformacionAdicional = "APTO 502",
                        Numero = "25",
                        ProvinciaEstado = "Valle del Cauca",
                    },
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA",
                    }
                }
            },
            new ClienteInterfazGraficaVentaDTO
            {
                Identificacion = 555555555,
                Nombre1 = "María",
                Apellido1 = "Hernández",
                Celular = "310444444",
                Email = "maria.hernandez@gmail.com",
                EnumTipoDocumento = EnumTipoDocumento.CedulaCiudadania,
                Direccion = "Bogotá",
                Telefono = "123456789",
                Tipo = 2,
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50005",
                        Nombre = "Cartagena",
                        Departamento = "Bolívar",
                        Pais = "Colombia",
                        TarifaIca = 10,
                        TarifaReteIca = 0.005,
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "13",
                        Pais = "CO",
                        Nombre = "BOLÍVAR",
                        CodigoIso = "BOL",
                    },
                    Direccion = new DireccionInterfazGraficaVentaDTO
                    {
                        Calle = "Avenida San Martín",
                        Ciudad = "Cartagena",
                        Pais = "Colombia",
                        CodigoPostal = "130001",
                        InformacionAdicional = "CASA 40",
                        Numero = "15",
                        ProvinciaEstado = "Bolívar",
                    },
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA",
                    }
                }
            },
            new ClienteInterfazGraficaVentaDTO
            {
                Identificacion = 666666666,
                Nombre1 = "Andrés",
                Nombre2 = "David",
                Apellido1 = "Mendoza",
                Apellido2 = "Vargas",
                Celular = "320555555",
                Email = "andres.mendoza@gmail.com",
                EnumTipoDocumento = EnumTipoDocumento.CedulaCiudadania,
                Direccion = "Villavicencio",
                Telefono = "987654321",
                Tipo = 1,
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50006",
                        Nombre = "Bucaramanga",
                        Departamento = "Santander",
                        Pais = "Colombia",
                        TarifaIca = 9,
                        TarifaReteIca = 0.005,
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "68",
                        Pais = "CO",
                        Nombre = "SANTANDER",
                        CodigoIso = "SAN",
                    },
                    Direccion = new DireccionInterfazGraficaVentaDTO
                    {
                        Calle = "Carrera 27",
                        Ciudad = "Bucaramanga",
                        Pais = "Colombia",
                        CodigoPostal = "680001",
                        InformacionAdicional = "CASA 10",
                        Numero = "5",
                        ProvinciaEstado = "Santander",
                    },
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA",
                    }
                }
            },
            new ClienteInterfazGraficaVentaDTO
            {
                Identificacion = 777777777,
                Nombre1 = "Diana",
                Apellido1 = "Ortega",
                Celular = "310666666",
                Email = "diana.ortega@gmail.com",
                EnumTipoDocumento = EnumTipoDocumento.CedulaCiudadania,
                Direccion = "Bogotá",
                Telefono = "123456789",
                Tipo = 2,
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50007",
                        Nombre = "Pereira",
                        Departamento = "Risaralda",
                        Pais = "Colombia",
                        TarifaIca = 7,
                        TarifaReteIca = 0.003,
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "66",
                        Pais = "CO",
                        Nombre = "RISARALDA",
                        CodigoIso = "RIS",
                    },
                    Direccion = new DireccionInterfazGraficaVentaDTO
                    {
                        Calle = "Calle 14",
                        Ciudad = "Pereira",
                        Pais = "Colombia",
                        CodigoPostal = "660001",
                        InformacionAdicional = "CASA 3",
                        Numero = "11",
                        ProvinciaEstado = "Risaralda",
                    },
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA",
                    }
                }
            },
            new ClienteInterfazGraficaVentaDTO
            {
                Identificacion = 888888888,
                Nombre1 = "Jorge",
                Apellido1 = "Vega",
                Celular = "310777777",
                Email = "jorge.vega@gmail.com",
                EnumTipoDocumento = EnumTipoDocumento.CedulaCiudadania,
                Direccion = "Barranquilla",
                Telefono = "987654321",
                Tipo = 1,
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50008",
                        Nombre = "Cúcuta",
                        Departamento = "Norte de Santander",
                        Pais = "Colombia",
                        TarifaIca = 8,
                        TarifaReteIca = 0.004,
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "54",
                        Pais = "CO",
                        Nombre = "NORTE DE SANTANDER",
                        CodigoIso = "NDS",
                    },
                    Direccion = new DireccionInterfazGraficaVentaDTO
                    {
                        Calle = "Avenida 5",
                        Ciudad = "Cúcuta",
                        Pais = "Colombia",
                        CodigoPostal = "540001",
                        InformacionAdicional = "CASA 8",
                        Numero = "18",
                        ProvinciaEstado = "Norte de Santander",
                    },
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA",
                    }
                }
            }
        };
        private static List<VentaInterfazGraficaVentaDTO> ventasGuardadas = new List<VentaInterfazGraficaVentaDTO>();
        public static List<EmpleadoInterfazGraficaVentaDTO> ObtenerEmpleados() => new List<EmpleadoInterfazGraficaVentaDTO>();
        public static List<EmpresaInterfazGraficaVentaDTO> ObtenerEmpresas() => new List<EmpresaInterfazGraficaVentaDTO>();
        public static List<ElementoInterfazGraficaVentaDTO> ObtenerElementos() => new List<ElementoInterfazGraficaVentaDTO>();

        public static List<VentaInterfazGraficaVentaDTO> ObtenerVentasGuardadas() => ventasGuardadas;
        /*
        private static List<CatalogoInterfazGraficaVentaDTO> catalogos = new List<CatalogoInterfazGraficaVentaDTO>
        {
            new CatalogoInterfazGraficaVentaDTO{
                id = 1,
                clasificacion = "Hogar",
                descripcion = "descrip 1",
                _elementos = elementos
            },
            new CatalogoInterfazGraficaVentaDTO{
                id = 2,
                clasificacion = "Maquinaria",
                descripcion = "descrip 2",
                _elementos = elementos
            },
            new CatalogoInterfazGraficaVentaDTO{
                id = 3,
                clasificacion = "Ropa",
                descripcion = "descrip 3",
                _elementos = elementos
            },
            new CatalogoInterfazGraficaVentaDTO{
                id = 4,
                clasificacion = "Accesorios",
                descripcion = "descrip 4",
                _elementos = elementos
            }
        };

        public static List<CatalogoInterfazGraficaVentaDTO> ObtenerCatalogos() => new List<CatalogoInterfazGraficaVentaDTO>(catalogos);

        */
        public static ElementoInterfazGraficaVentaDTO ObtenerElementoPorCodigo(long codigoArticulo)
        {
            var elemento = elementos.FirstOrDefault(elemento => elemento.CodigoArticulo == codigoArticulo);
            if (elemento is not null)
            {
                return new ElementoInterfazGraficaVentaDTO
                {
                    CodigoArticulo = elemento.CodigoArticulo,
                    Referencia = elemento.Referencia,
                    Marca = elemento.Marca,
                    Valor = elemento.Valor,
                    ValorUnitario = elemento.ValorUnitario,
                    PorcentajeIva = elemento.PorcentajeIva,
                    CantidadDisponibleAlmacen = elemento.CantidadDisponibleAlmacen,
                    CantidadDisponibleBodega = elemento.CantidadDisponibleBodega,
                    CantidadVendidos = elemento.CantidadVendidos,
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
                    Tipo = elemento.Tipo,
                    TipoElemento = elemento.TipoElemento,
                    UltimaVenta = elemento.UltimaVenta,
                    Unidad = elemento.Unidad


                };
            }

            return null;

        }
        public static void GuardarVenta(VentaInterfazGraficaVentaDTO ventaDTO)
        {
            ventasGuardadas.Add(ventaDTO);
        }
        /*
        public static void InsertarElemento(ElementoInterfazGraficaVentaDTO elemento)
        {
            elementos.Add(elemento);
        }
        public static int ActualizarElemento(ElementoInterfazGraficaVentaDTO elemento)
        {
            int resultado = 0;
            int indiceActualizar = elementos.FindIndex(p => p.CodigoArticulo == elemento.CodigoArticulo);
            if (indiceActualizar != -1)
            {
                elementos[indiceActualizar] = elemento;
                resultado = 1;
            }
            return resultado;
        }

        */
        public static ClienteInterfazGraficaVentaDTO ObtenerClientePorId(long identificacion)
        {
            var cliente2 = clientes.FirstOrDefault(cliente => cliente.Identificacion == identificacion);
            if (cliente2 is not null)
            {
                return new ClienteInterfazGraficaVentaDTO
                {
                    Identificacion = cliente2.Identificacion,
                    Nombre1 = cliente2.Nombre1,
                    Nombre2 = cliente2.Nombre2,
                    Apellido1 = cliente2.Apellido1,
                    Apellido2 = cliente2.Apellido2,
                    Celular = cliente2.Celular,
                    Email = cliente2.Email,
                    EnumTipoDocumento = cliente2.EnumTipoDocumento,
                    Direccion = cliente2.Direccion,
                    Telefono = cliente2.Telefono,
                    Tipo = cliente2.Tipo,
                    Ubicacion = cliente2.Ubicacion,
                };
            }

            return null;
        }
    }
}