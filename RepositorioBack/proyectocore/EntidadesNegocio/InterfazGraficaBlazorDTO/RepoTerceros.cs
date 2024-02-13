using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.ImpuestosYDescuentos;
using EntidadesNegocio.Terceros;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO
{
    public static class RepoTerceros
    {
        private static List<TerceroInterfazGraficaDTO> terceros = new List<TerceroInterfazGraficaDTO>
        {
            new TerceroInterfazGraficaDTO
            {
                Identificacion = 1122628133,
                Digito = 1,
                Nombre1 = "Johan",
                Nombre2 = "Estiven",
                Apellido1 = "Robayo",
                Apellido2 = "Linares",
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA"
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "50",
                        Nombre = "Meta",
                        CodigoIso = "MET",
                    },
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50110",
                        Nombre = "BARRANCA DE UPÍA"
                    }

                },
                Telefono = "639383838",
                Direccion = "MZNA A CASA 5 B. PORTALES DEL LLANO",
                Celular = "3223942282",
                Email= "jlinaresgarzon@gmail.com",
                TipoTercero = "CLIENTE",
                TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                {
                    Codigo = 13,
                    Nombre = "Cédula de Cuidadanía"
                },
                Impuestos = new List<ImpuestoInterfazGraficaVentaDTO>
                {
                    new ImpuestoInterfazGraficaVentaDTO
                    {
                        Id ="06",
                        Nombre = "ReteFuente",
                    },
                    new ImpuestoInterfazGraficaVentaDTO
                    {
                        Id ="05",
                        Nombre = "ReteIVA",
                    },


                },
            },
            new TerceroInterfazGraficaDTO
            {
                Identificacion = 1122628133,
                Digito = 1,
                Nombre1 = "Johan",
                Nombre2 = "Estiven",
                Apellido1 = "Robayo",
                Apellido2 = "Linares",
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA"
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "50",
                        Nombre = "Meta",
                        CodigoIso = "MET",
                    },
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50110",
                        Nombre = "BARRANCA DE UPÍA"
                    }

                },
                Telefono = "639383838",
                Direccion = "MZNA A CASA 5 B. PORTALES DEL LLANO",
                Celular = "3223942282",
                Email= "jlinaresgarzon@gmail.com",
                TipoTercero = "EMPLEADO",
                TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                {
                    Codigo = 13,
                    Nombre = "Cédula de Cuidadanía"
                }
            },
            new TerceroInterfazGraficaDTO
            {
                Identificacion = 1122628133,
                Digito = 1,
                RazonSocial="Johan Estiven Robayo SAS",
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA"
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "50",
                        Nombre = "Meta",
                        CodigoIso = "MET",
                    },
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50110",
                        Nombre = "BARRANCA DE UPÍA"
                    }

                },
                Telefono = "639383838",
                Direccion = "MZNA A CASA 5 B. PORTALES DEL LLANO",
                Celular = "3223942282",
                Email= "jlinaresgarzon@gmail.com",
                TipoTercero = "EMPRESA",
                TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                {
                    Codigo = 13,
                    Nombre = "Cédula de Cuidadanía"
                },
            },
            new TerceroInterfazGraficaDTO
            {
                Identificacion = 11334556783,
                Digito = 2,
                Nombre1 = "Santiago",
                Apellido1 = "Ortiz",
                Apellido2 = "Suarez",
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA"
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "11",
                        Nombre = "Bogotá",
                        CodigoIso = "DC",
                    },
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "11001",
                        Nombre = "Bogotá, D.C"
                    }

                },
                Telefono = "34951395",
                Direccion = "Barrio chapinero",
                Celular = "3234998065",
                Email= "santiago.suarez@gmail.com",
                TipoTercero = "CLIENTE",
                TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                {
                    Codigo = 13,
                    Nombre = "Cédula de Cuidadanía"
                },
            },
            new TerceroInterfazGraficaDTO
            {
                Identificacion = 11445467899,
                Digito = 4,
                Nombre1 = "Sharon",
                Nombre2 = "Daniela",
                Apellido1 = "Robayo",
                Apellido2 = "Niño",
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA"
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "50",
                        Nombre = "Meta",
                        CodigoIso = "MET",
                    },
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50001",
                        Nombre = "VILLAVICENCIO"
                    }

                },
                Telefono = "358385319",
                Direccion = "Barrio catumare",
                Celular = "3124592245",
                Email= "sharon.robayo@gmail.com",
                TipoTercero = "CLIENTE",
                TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                {
                    Codigo = 13,
                    Nombre = "Cédula de Cuidadanía"
                },
            },
            new TerceroInterfazGraficaDTO
            {
                Identificacion = 11214567890,
                Digito = 6,
                Nombre1 = "Cindy",
                Nombre2 = "Lorena",
                Apellido1 = "Ordoñez",
                Apellido2 = "Villamil",
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA"
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "50",
                        Nombre = "Meta",
                        CodigoIso = "MET",
                    },
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50001",
                        Nombre = "VILLAVICENCIO"
                    }

                },
                Telefono = "87474859",
                Direccion = "Barrio el Buque",
                Celular = "3222895732",
                Email= "cindy.Ordoñez@gmail.com",
                TipoTercero = "CLIENTE",
                TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                {
                    Codigo = 13,
                    Nombre = "Cédula de Cuidadanía"
                },
            },
            new TerceroInterfazGraficaDTO
            {
                Identificacion = 11882772772,
                Digito = 8,
                Nombre1 = "Brian",
                Apellido1 = "Montes",
                Apellido2 = "Arias",
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA"
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "50",
                        Nombre = "Meta",
                        CodigoIso = "MET",
                    },
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50573",
                        Nombre = "VILLAVICENCIO"
                    }

                },
                Telefono = "98637262",
                Direccion = "Barrio centro",
                Celular = "3134556235",
                Email= "Brian.Arias@gmail.com",
                TipoTercero = "CLIENTE",
                TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                {
                    Codigo = 13,
                    Nombre = "Cédula de Cuidadanía"
                },
            },
            new TerceroInterfazGraficaDTO
            {
                Identificacion = 87938882,
                Digito = 3,
                RazonSocial = "Repuestos General Motors SAS",
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA"
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "11",
                        Nombre = "Bogotá",
                        CodigoIso = "DC",
                    },
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "11001",
                        Nombre = "Bogotá, D.C"
                    }

                },
                Telefono = "83847646",
                Direccion = "Cra 34 #22-50",
                Celular = "3224558762",
                Email = "RGeneralMotors@gmail.com",
                TipoTercero = "EMPRESA",
                TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                {
                    Codigo = 31,
                    Nombre = "NIT"

                },
                Naturaleza = "Mixta",
                ActividadEconomica= "Repuestos",
            },
            new TerceroInterfazGraficaDTO
            {
                Identificacion = 673883002,
                Digito = 3,
                RazonSocial = "Motores Diesel SAS",
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA"
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "11",
                        Nombre = "Bogotá",
                        CodigoIso = "DC",
                    },
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "11001",
                        Nombre = "Bogotá, D.C"
                    }

                },
                Telefono = "97665656",
                Direccion = "Cra 50 #21-40",
                Celular = "3134782461",
                Email = "MotoresDieselColombia@gmail.com",
                TipoTercero = "EMPRESA",
                TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                {
                    Codigo = 31,
                    Nombre = "NIT"

                },
                Naturaleza = "Privada",
                ActividadEconomica= "Motores",
            },
            new TerceroInterfazGraficaDTO
            {
                Identificacion = 728993002,
                Digito = 3,
                RazonSocial = "Variedades Centauros SAS",
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA"
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "50",
                        Nombre = "Meta",
                        CodigoIso = "MET",
                    },
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50573",
                        Nombre = "VILLAVICENCIO"
                    }

                },
                Sedes = new List<SedeInterfazGraficaTerceroDTO>
                {
                    new SedeInterfazGraficaTerceroDTO
                    {
                        Id = 1,
                        Responsable ="Carlos Andres Pizarro Rodriguez",
                        Direccion = "cara 13 #22-12",
                        Email1 = "carlos.pizarro@gmail.com",
                        Email2 = "carlos.rodriguez@gmail.com",
                        Telefono = "3223942284",
                        Ubicacion = new UbicacionInterfazGraficaVentaDTO
                        { 
                            Pais = new PaisInterfazGraficaVentaDTO()
                            { 
                                Codigo = "CO",
                                Nombre = "COLOMBIA"
                            },
                            DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                            {
                                Codigo = "50",
                                Nombre = "Meta",
                                CodigoIso = "MET",
                            },
                            Ciudad = new CiudadInterfazGraficaVentaDTO
                            {
                                Codigo = "50573",
                                Nombre = "VILLAVICENCIO"
                            }
                        },
                        
                    },
                    new SedeInterfazGraficaTerceroDTO
                    {
                        Id = 2,
                        Responsable ="Manuel Matinez Mendoza",
                        Direccion = "Avenida 41 #22-11",
                        Email1 = "manuel.martinez@gmail.com",
                        Email2 = "manuel.mendoza@gmail.com",
                        Telefono = "3223942284",
                        Ubicacion = new UbicacionInterfazGraficaVentaDTO
                        {
                            Pais = new PaisInterfazGraficaVentaDTO()
                            {
                                Codigo = "CO",
                                Nombre = "COLOMBIA"
                            },
                            DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                            {
                                Codigo = "11",
                                Nombre = "Bogotá",
                                CodigoIso = "DC",
                            },
                            Ciudad = new CiudadInterfazGraficaVentaDTO
                            {
                                Codigo = "11001",
                                Nombre = "Bogotá, D.C"
                            }

                        },
                    },
                    new SedeInterfazGraficaTerceroDTO
                    {
                        Id = 3,
                        Responsable ="Camila Andrade Muñoz",
                        Direccion = "Calle 22 # 33-12",
                        Email1 = "camila.Andrade@gmail.com",
                        Email2 = "VariedadesCentaurosMedellin@gmail.com",
                        Telefono = "3223942284",
                        Ubicacion = new UbicacionInterfazGraficaVentaDTO
                        {
                            Pais = new PaisInterfazGraficaVentaDTO()
                            {
                                Codigo = "CO",
                                Nombre = "COLOMBIA"
                            },
                            DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                            {
                                Codigo = "05",
                                Nombre = "Antioquia",
                                CodigoIso = "ANT",
                                Pais = "CO"
                            },
                            Ciudad = new CiudadInterfazGraficaVentaDTO
                            {
                                Pais = "CO",
                                Departamento = "05",
                                Codigo = "05002",
                                Nombre = "ABEJORRAL",
                            },

                        },
                    }
                },
                Telefono = "763535352",
                Direccion = "Avn 40 #45-5",
                Celular = "3558902288",
                Email = "Variedades.centauros@gmail.com",
                TipoTercero = "EMPRESA",
                TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                {
                    Codigo = 31,
                    Nombre = "NIT"

                },
                Naturaleza = "Publica",
                ActividadEconomica= "Tecnología",
            },
            new TerceroInterfazGraficaDTO
            {
                Identificacion = 839562115,
                Digito = 3,
                RazonSocial = "Variedades Tech SAS",
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA"
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "50",
                        Nombre = "Meta",
                        CodigoIso = "MET",
                    },
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50573",
                        Nombre = "VILLAVICENCIO"
                    }

                },
                Telefono = "938475720",
                Direccion = "Cra 25E #22A-05",
                Celular = "3105556982",
                Email = "Variedades.tech@gmail.com",
                TipoTercero = "EMPRESA",
                TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                {
                    Codigo = 31,
                    Nombre = "NIT"

                },
                Naturaleza = "Privada",
                ActividadEconomica= "Tecnología",
            },
            new TerceroInterfazGraficaDTO
            {
                Identificacion = 3129983883,
                Digito = 3,
                RazonSocial = "Repuestos General Motors SAS",
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA"
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "11",
                        Nombre = "Bogotá",
                        CodigoIso = "DC",
                    },
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "11001",
                        Nombre = "Bogotá, D.C"
                    }

                },
                Telefono = "83847646",
                Direccion = "Cra 34 #22-50",
                Celular = "3224558762",
                Email = "RGeneralMotors@gmail.com",
                TipoTercero = "CLIENTE",
                TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                {
                    Codigo = 31,
                    Nombre = "NIT"

                },
                Impuestos = new List<ImpuestoInterfazGraficaVentaDTO>
                {
                    new ImpuestoInterfazGraficaVentaDTO
                    {
                        Id ="06",
                        Nombre = "ReteFuente",
                    },
                    new ImpuestoInterfazGraficaVentaDTO
                    {
                        Id ="05",
                        Nombre = "ReteIVA",
                    },
                    new ImpuestoInterfazGraficaVentaDTO
                    {
                        Id ="07",
                        Nombre = "ReteIca",
                    },


                },
                Naturaleza = "Mixta",
                ActividadEconomica= "Repuestos",
            },
            new TerceroInterfazGraficaDTO
            {
                Identificacion = 9383773737,
                Digito = 3,
                RazonSocial = "Motores Diesel SAS",
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA"
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "11",
                        Nombre = "Bogotá",
                        CodigoIso = "DC",
                    },
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "11001",
                        Nombre = "Bogotá, D.C"
                    }

                },
                Telefono = "97665656",
                Direccion = "Cra 50 #21-40",
                Celular = "3134782461",
                Email = "MotoresDieselColombia@gmail.com",
                TipoTercero = "CLIENTE",
                TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                {
                    Codigo = 31,
                    Nombre = "NIT"

                },
                Impuestos = new List<ImpuestoInterfazGraficaVentaDTO>
                {
                    new ImpuestoInterfazGraficaVentaDTO
                    {
                        Id ="06",
                        Nombre = "ReteFuente",
                    },
                    new ImpuestoInterfazGraficaVentaDTO
                    {
                        Id ="07",
                        Nombre = "ReteIca",
                    },
                },
                Naturaleza = "Privada",
                ActividadEconomica= "Motores",
            },
            new TerceroInterfazGraficaDTO
            {
                Identificacion = 9393801820,
                Digito = 3,
                RazonSocial = "Variedades Centauros SAS",
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA"
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "50",
                        Nombre = "Meta",
                        CodigoIso = "MET",
                    },
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50573",
                        Nombre = "VILLAVICENCIO"
                    }

                },
                Sedes = new List<SedeInterfazGraficaTerceroDTO>
                {
                    new SedeInterfazGraficaTerceroDTO
                    {
                        Id = 5,
                        Responsable ="Carlos Andres Pizarro Rodriguez",
                        Direccion = "cara 13 #22-12",
                        Email1 = "carlos.pizarro@gmail.com",
                        Email2 = "carlos.rodriguez@gmail.com",
                        Telefono = "3223942284",
                        Ubicacion = new UbicacionInterfazGraficaVentaDTO
                        {
                            Pais = new PaisInterfazGraficaVentaDTO()
                            {
                                Codigo = "CO",
                                Nombre = "COLOMBIA"
                            },
                            DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                            {
                                Codigo = "50",
                                Nombre = "Meta",
                                CodigoIso = "MET",
                            },
                            Ciudad = new CiudadInterfazGraficaVentaDTO
                            {
                                Codigo = "50573",
                                Nombre = "VILLAVICENCIO"
                            }
                        },

                    },
                    new SedeInterfazGraficaTerceroDTO
                    {
                        Id = 6,
                        Responsable ="Manuel Matinez Mendoza",
                        Direccion = "Avenida 41 #22-11",
                        Email1 = "manuel.martinez@gmail.com",
                        Email2 = "manuel.mendoza@gmail.com",
                        Telefono = "3223942284",
                        Ubicacion = new UbicacionInterfazGraficaVentaDTO
                        {
                            Pais = new PaisInterfazGraficaVentaDTO()
                            {
                                Codigo = "CO",
                                Nombre = "COLOMBIA"
                            },
                            DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                            {
                                Codigo = "11",
                                Nombre = "Bogotá",
                                CodigoIso = "DC",
                            },
                            Ciudad = new CiudadInterfazGraficaVentaDTO
                            {
                                Codigo = "11001",
                                Nombre = "Bogotá, D.C"
                            }

                        },
                    },
                    new SedeInterfazGraficaTerceroDTO
                    {
                        Id = 7,
                        Responsable ="Camila Andrade Muñoz",
                        Direccion = "Calle 22 # 33-12",
                        Email1 = "camila.Andrade@gmail.com",
                        Email2 = "VariedadesCentaurosMedellin@gmail.com",
                        Telefono = "3223942284",
                        Ubicacion = new UbicacionInterfazGraficaVentaDTO
                        {
                            Pais = new PaisInterfazGraficaVentaDTO()
                            {
                                Codigo = "CO",
                                Nombre = "COLOMBIA"
                            },
                            DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                            {
                                Codigo = "05",
                                Nombre = "Antioquia",
                                CodigoIso = "ANT",
                                Pais = "CO"
                            },
                            Ciudad = new CiudadInterfazGraficaVentaDTO
                            {
                                Pais = "CO",
                                Departamento = "05",
                                Codigo = "05002",
                                Nombre = "ABEJORRAL",
                            },

                        },
                    }
                },
                Telefono = "763535352",
                Direccion = "Avn 40 #45-5",
                Celular = "3558902288",
                Email = "Variedades.centauros@gmail.com",
                TipoTercero = "CLIENTE",
                TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                {
                    Codigo = 31,
                    Nombre = "NIT"

                },
                Impuestos = new List<ImpuestoInterfazGraficaVentaDTO>
                {
                    new ImpuestoInterfazGraficaVentaDTO
                    {
                        Id ="06",
                        Nombre = "ReteFuente",
                    },
                    new ImpuestoInterfazGraficaVentaDTO
                    {
                        Id ="05",
                        Nombre = "ReteIVA",
                    },
                    new ImpuestoInterfazGraficaVentaDTO
                    {
                        Id ="07",
                        Nombre = "ReteIca",
                    },


                },
                Naturaleza = "Publica",
                ActividadEconomica= "Tecnología",
            },
            new TerceroInterfazGraficaDTO
            {
                Identificacion = 928736666,
                Digito = 3,
                RazonSocial = "Variedades Tech SAS",
                Ubicacion = new UbicacionInterfazGraficaVentaDTO
                {
                    Pais = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = "CO",
                        Nombre = "COLOMBIA"
                    },
                    DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = "50",
                        Nombre = "Meta",
                        CodigoIso = "MET",
                    },
                    Ciudad = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = "50573",
                        Nombre = "VILLAVICENCIO"
                    }

                },
                Telefono = "938475720",
                Direccion = "Cra 25E #22A-05",
                Celular = "3105556982",
                Email = "Variedades.tech@gmail.com",
                TipoTercero = "CLIENTE",
                TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                {
                    Codigo = 31,
                    Nombre = "NIT"

                },
                Impuestos = new List<ImpuestoInterfazGraficaVentaDTO>
                {
                    new ImpuestoInterfazGraficaVentaDTO
                    {
                        Id ="05",
                        Nombre = "ReteIVA",
                    },
                    new ImpuestoInterfazGraficaVentaDTO
                    {
                        Id ="07",
                        Nombre = "ReteIca",
                    },
                },
                Naturaleza = "Privada",
                ActividadEconomica= "Tecnología",
            }
        };

        private static List<TipoDocumentoInterfazGraficaTercerosDTO> tiposDocumentos = new List<TipoDocumentoInterfazGraficaTercerosDTO>
        {
            new TipoDocumentoInterfazGraficaTercerosDTO
            { 
                Codigo = 11,
                Nombre = "Registro civil"
            },
            new TipoDocumentoInterfazGraficaTercerosDTO
            {
                Codigo = 12,
                Nombre = "Tarjeta de identidad"
            },
            new TipoDocumentoInterfazGraficaTercerosDTO
            {
                Codigo = 13,
                Nombre = "Cédula de ciudadanía"
            },
            new TipoDocumentoInterfazGraficaTercerosDTO
            {
                Codigo = 21,
                Nombre = "Tarjeta de extranjería"
            },
            new TipoDocumentoInterfazGraficaTercerosDTO
            {
                Codigo = 22,
                Nombre = "Cédula de extranjería"
            },
            new TipoDocumentoInterfazGraficaTercerosDTO
            {
                Codigo = 31,
                Nombre = "NIT"
            },
            new TipoDocumentoInterfazGraficaTercerosDTO
            {
                Codigo = 41,
                Nombre = "Pasaporte"
            },
            new TipoDocumentoInterfazGraficaTercerosDTO
            {
                Codigo = 42,
                Nombre = "Documento de identificación extranjero"
            },
            new TipoDocumentoInterfazGraficaTercerosDTO
            {
                Codigo = 50,
                Nombre = "NIT de otro país"
            },
            new TipoDocumentoInterfazGraficaTercerosDTO
            {
                Codigo = 91,
                Nombre = "NUIP"
            },

        };


        private static List<ImpuestoInterfazGraficaVentaDTO> impuestos = new List<ImpuestoInterfazGraficaVentaDTO>
        {
            new ImpuestoInterfazGraficaVentaDTO
            {
                Id ="06",
                Nombre = "ReteFuente",
            },
            new ImpuestoInterfazGraficaVentaDTO
            {
                Id ="07",
                Nombre = "ReteIca",
            },
            new ImpuestoInterfazGraficaVentaDTO
            {
                Id ="05",
                Nombre = "ReteIVA",
            },
            new ImpuestoInterfazGraficaVentaDTO
            {
                Id ="03",
                Nombre = "ICA",
            },

        };


        public static List<ImpuestoInterfazGraficaVentaDTO> ObtenerImpuestos() => impuestos;
        public static List<TerceroInterfazGraficaDTO> ObtenerTerceros() => terceros;
        public static List<TipoDocumentoInterfazGraficaTercerosDTO> ObtenerTiposDocumento() => tiposDocumentos;

        public static List<TerceroInterfazGraficaDTO> BuscarTerceroPorIdentificacion(double identificacion,string tipoTercero)
        {

            var terceroABuscar = terceros
                .Where(tercero => 
                tercero.Identificacion == identificacion &&
                tercero.TipoTercero.Equals(tipoTercero)
                ).ToList();

            return terceroABuscar;
        }

        public static List<TerceroInterfazGraficaDTO> FiltrarTercerosPorIniciales(string iniciales, string tipoTercero)
        {
            var tercerosFiltrados = terceros
                .Where(tercero => 
                (tercero.RazonSocial.IndexOf(iniciales, StringComparison.OrdinalIgnoreCase) >= 0 ||
                tercero.Nombre1.IndexOf(iniciales, StringComparison.OrdinalIgnoreCase) >= 0 ||
                tercero.Nombre2.IndexOf(iniciales, StringComparison.OrdinalIgnoreCase) >= 0 || 
                tercero.Apellido1.IndexOf(iniciales, StringComparison.OrdinalIgnoreCase) >= 0 ||
                tercero.Apellido2.IndexOf(iniciales, StringComparison.OrdinalIgnoreCase) >= 0) &&
                tercero.TipoTercero.Equals(tipoTercero)
                ).ToList();
            return tercerosFiltrados;
        }

        public static void AgregarSedeTercero(long identificacionTercero,SedeInterfazGraficaTerceroDTO sede)
        {
            var tercero = terceros.FirstOrDefault(ter => ter.Identificacion == identificacionTercero);
            if (tercero is not null)
            {
                if (tercero.Sedes is null)
                {
                    tercero.Sedes = new List<SedeInterfazGraficaTerceroDTO>();
                }
                tercero.Sedes.Add(sede);
            }
        
        }

        public static void ActualizarSedeTercero(long identificacionTercero, SedeInterfazGraficaTerceroDTO sede)
        {

            var tercero = terceros.FirstOrDefault(ter => ter.Identificacion == identificacionTercero);
            var sedeTercero = tercero.Sedes.FirstOrDefault(sedeTer => sedeTer.Id == sede.Id);
            if (tercero is not null && sedeTercero is not null)
            {
                sedeTercero = sede;
            }

        }

        public static List<SedeInterfazGraficaTerceroDTO> ObtenerSedesTercero(long identificacionTercero)
        {
            
            var tercero = terceros.FirstOrDefault(ter => ter.Identificacion == identificacionTercero);
            List<SedeInterfazGraficaTerceroDTO> sedesTercero = new List<SedeInterfazGraficaTerceroDTO>(tercero.Sedes);

            return sedesTercero;
        }

        public static TerceroInterfazGraficaDTO BuscarTerceroPorId(long identificacion, string tipoTercero)
        {

            var terceroABuscar = terceros.FirstOrDefault( ter => ter.Identificacion == identificacion && ter.TipoTercero.Equals(tipoTercero));

            return terceroABuscar;
        }
    }
}
