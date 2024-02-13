using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using System.Collections.Generic;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO
{
    public static class RepoUbicacion
    {
        private static List<PaisInterfazGraficaVentaDTO> paises = new List<PaisInterfazGraficaVentaDTO>
        {
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "CO",
                Nombre = "COLOMBIA",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "AF",
                Nombre = "AFGANISTAN",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "AX",
                Nombre = "ISLAS ÅLAND",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "DE",
                Nombre = "ALEMANIA",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "AD",
                Nombre = "ANDORRA",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "AO",
                Nombre = "ANGOLA",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "AI",
                Nombre = "ANGUILA",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "AQ",
                Nombre = "ANTARTIDA",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "AG",
                Nombre = "ANTIGUA Y BARBUDA",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "SA",
                Nombre = "ARABIA SAUDITA",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "SA",
                Nombre = "ARABIA SAUDITA",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "DZ",
                Nombre = "ARGELIA",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "AR",
                Nombre = "ARGENTINA",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "AM",
                Nombre = "ARMENIA",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "AW",
                Nombre = "ARUBA",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "AW",
                Nombre = "ARUBA",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "AU",
                Nombre = "AUSTRALIA",
            },
            new PaisInterfazGraficaVentaDTO
            {
                Codigo = "AT",
                Nombre = "AUSTRIA",
            },
            
        };

        private static List<DepartamentoProvinciaInterfazGraficaVentaDTO> departamentos = new List<DepartamentoProvinciaInterfazGraficaVentaDTO>
        {
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "08",
                Nombre = "Atlántico",
                CodigoIso = "ATL",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            { 
                Codigo = "91",
                Nombre = "Amazonas",
                CodigoIso = "AMA",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "05",
                Nombre = "Antioquia",
                CodigoIso = "ANT",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "81",
                Nombre = "Arauca",
                CodigoIso = "ARA",
                Pais = "CO"
            },
            
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "11",
                Nombre = "Bogotá",
                CodigoIso = "DC",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "13",
                Nombre = "Bolívar",
                CodigoIso = "BOL",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "15",
                Nombre = "Boyacá",
                CodigoIso = "BOY",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "17",
                Nombre = "Caldas",
                CodigoIso = "CAL",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "18",
                Nombre = "Caquetá",
                CodigoIso = "CAQ",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "85",
                Nombre = "Casanare",
                CodigoIso = "CAS",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "19",
                Nombre = "Cauca",
                CodigoIso = "CAU",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "20",
                Nombre = "Cesar",
                CodigoIso = "CES",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "27",
                Nombre = "Córdoba",
                CodigoIso = "COR",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "25",
                Nombre = "Cundinamarca",
                CodigoIso = "CUN",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "94",
                Nombre = "Guainía",
                CodigoIso = "GUA",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "95",
                Nombre = "Guaviare",
                CodigoIso = "GUV",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "41",
                Nombre = "Huila",
                CodigoIso = "HUI",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "44",
                Nombre = "La Guajira",
                CodigoIso = "LAG",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "47",
                Nombre = "Magdalena",
                CodigoIso = "MAG",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "50",
                Nombre = "Meta",
                CodigoIso = "MET",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "52",
                Nombre = "Nariño",
                CodigoIso = "NAR",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "54",
                Nombre = "Norte de Santander",
                CodigoIso = "NSA",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "86",
                Nombre = "Putumayo",
                CodigoIso = "PUT",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "63",
                Nombre = "Quindío",
                CodigoIso = "QUI",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "66",
                Nombre = "Risaralda",
                CodigoIso = "RIS",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "88",
                Nombre = "San Andrés y Providencia",
                CodigoIso = "SAP",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "68",
                Nombre = "Santander",
                CodigoIso = "SAN",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "70",
                Nombre = "Sucre",
                CodigoIso = "SUC",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "73",
                Nombre = "Tolima",
                CodigoIso = "TOL",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "76",
                Nombre = "Valle del Cauca",
                CodigoIso = "VAC",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "97",
                Nombre = "Vaupés",
                CodigoIso = "VAU",
                Pais = "CO"
            },
            new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = "99",
                Nombre = "Vichada",
                CodigoIso = "VID",
                Pais = "CO"
            },
        };

        private static List<CiudadInterfazGraficaVentaDTO> ciudades = new List<CiudadInterfazGraficaVentaDTO>
        {
            new CiudadInterfazGraficaVentaDTO
            { 
                Pais = "CO",
                Departamento = "05",
                Codigo = "05001",
                Nombre = "MEDELLÍN",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "05",
                Codigo = "05002",
                Nombre = "ABEJORRAL",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "05",
                Codigo = "05004",
                Nombre = "ABRIAQUÍ",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "05",
                Codigo = "05021",
                Nombre = "ALEJANDRÍA",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "08",
                Codigo = "08001",
                Nombre = "BARRANQUILLA",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "08",
                Codigo = "08078",
                Nombre = "BARANOA",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "08",
                Codigo = "08433",
                Nombre = "MALAMBO",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "08",
                Codigo = "08758",
                Nombre = "SOLEDAD",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "08",
                Codigo = "08675",
                Nombre = "SANTA LUCÍA",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "13",
                Codigo = "13001",
                Nombre = "CARTAGENA DE INDIAS",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "13",
                Codigo = "13212",
                Nombre = "CÓRDOBA",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "13",
                Codigo = "13629",
                Nombre = "SAN CRISTÓBAL",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "13",
                Codigo = "13873",
                Nombre = "VILLANUEVA",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "50",
                Codigo = "50001",
                Nombre = "VILLAVICENCIO",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "50",
                Codigo = "50006",
                Nombre = "ACACÍAS",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "50",
                Codigo = "50110",
                Nombre = "BARRANCA DE UPÍA",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "50",
                Codigo = "50226",
                Nombre = "CUMARAL",
            },
            new CiudadInterfazGraficaVentaDTO
            {
                Pais = "CO",
                Departamento = "50",
                Codigo = "50400",
                Nombre = "LEJANÍAS",
            },

        };

        public static List<PaisInterfazGraficaVentaDTO> ObtenerPaises() => paises;

        public static List<DepartamentoProvinciaInterfazGraficaVentaDTO> ObtenerDepartamentosPais(string codigoPais)
        {
            var dptos = departamentos.Where(dpto => dpto.Pais == codigoPais).ToList();
            return new List<DepartamentoProvinciaInterfazGraficaVentaDTO> (dptos);
        }


        public static List<CiudadInterfazGraficaVentaDTO> ObtenerCiudaddesDeDepartamento(string codigoDepartamento)
        {
            var ciudades2 = ciudades.Where(ciudad => ciudad.Departamento == codigoDepartamento).ToList();
            return new List <CiudadInterfazGraficaVentaDTO> (ciudades2);
        }

        public static void AgregarDepartamento(DepartamentoProvinciaInterfazGraficaVentaDTO departamentoDTO)
        { 
            departamentos.Add(departamentoDTO); 
        }

        public static void AgregarCiudad(CiudadInterfazGraficaVentaDTO ciudadDTO)
        { 
            ciudades.Add(ciudadDTO);
        }


    }
}
