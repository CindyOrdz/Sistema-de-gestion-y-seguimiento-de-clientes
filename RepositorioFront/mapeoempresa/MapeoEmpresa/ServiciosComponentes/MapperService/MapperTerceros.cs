using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.LugarProcedencia;

namespace ServiciosComponentes.MapperService
{
    public static class MapperTerceros
    {
        public static TerceroInterfazGraficaDTO ClonarTerceroInterfazGraficaDTO(TerceroInterfazGraficaDTO terceroDTO)
        {
            return new TerceroInterfazGraficaDTO
            {
                Identificacion = terceroDTO.Identificacion,
                Nombre1 = terceroDTO.Nombre1,
                Nombre2 = terceroDTO.Nombre2,
                Apellido1 = terceroDTO.Apellido1,
                Apellido2 = terceroDTO.Apellido2,
                RazonSocial = terceroDTO.RazonSocial,
                ActividadEconomica = terceroDTO.ActividadEconomica,
                ConMontoDeLey = terceroDTO.ConMontoDeLey,
                Celular = terceroDTO.Celular,
                Digito = terceroDTO.Digito,
                Direccion = terceroDTO.Direccion,
                Email = terceroDTO.Email,
                Naturaleza = terceroDTO.Naturaleza,
                Telefono = terceroDTO.Telefono,
                TipoDocumento = terceroDTO.TipoDocumento,
                TipoTercero = terceroDTO.TipoTercero,
                Ubicacion = ClonarUbicacionInterfazGraficaVentaDTO(terceroDTO.Ubicacion),
                Sedes = terceroDTO.Sedes,
                Impuestos = terceroDTO.Impuestos,
                Cargo = terceroDTO.Cargo,
                FechaInicio = terceroDTO.FechaInicio,
                FechaFin = terceroDTO.FechaFin,


            };


        }

        public static SedeInterfazGraficaTerceroDTO ClonarSedeInterfazGraficaTerceroDTO(SedeInterfazGraficaTerceroDTO sedeDTO)
        {

            return new SedeInterfazGraficaTerceroDTO
            {
                Id = sedeDTO.Id,
                Responsable = sedeDTO.Responsable,
                Telefono = sedeDTO.Telefono,
                Email1 = sedeDTO.Email1,
                Email2 = sedeDTO.Email2,
                Direccion = sedeDTO.Direccion,
                Ubicacion = ClonarUbicacionInterfazGraficaVentaDTO(sedeDTO.Ubicacion)

            };
        }

        public static UbicacionInterfazGraficaVentaDTO ClonarUbicacionInterfazGraficaVentaDTO(UbicacionInterfazGraficaVentaDTO ubicacionDTO)
        {
            return new UbicacionInterfazGraficaVentaDTO
            {
                Pais = ClonarPaisInterGraficaVentaDTO(ubicacionDTO.Pais),
                DepartamentoProvincia = ClonarDepartamentoInterGraficaventaDTO(ubicacionDTO.DepartamentoProvincia),
                Ciudad = ClonarCiudadInterGraficaventaDTO(ubicacionDTO.Ciudad),
            };
        }


        public static PaisInterfazGraficaVentaDTO ClonarPaisInterGraficaVentaDTO(PaisInterfazGraficaVentaDTO paisDTO)
        {

            return new PaisInterfazGraficaVentaDTO
            {
                Codigo = paisDTO.Codigo,
                Nombre = paisDTO.Nombre,

            };
        }

        public static DepartamentoProvinciaInterfazGraficaVentaDTO ClonarDepartamentoInterGraficaventaDTO(DepartamentoProvinciaInterfazGraficaVentaDTO departamentoDTO)
        {

            return new DepartamentoProvinciaInterfazGraficaVentaDTO
            {
                Codigo = departamentoDTO.Codigo,
                Nombre = departamentoDTO.Nombre,
                CodigoIso = departamentoDTO.CodigoIso,
                Pais = departamentoDTO.Pais,

            };
        }

        public static CiudadInterfazGraficaVentaDTO ClonarCiudadInterGraficaventaDTO(CiudadInterfazGraficaVentaDTO ciudadDTO)
        {

            return new CiudadInterfazGraficaVentaDTO
            {
                Codigo = ciudadDTO.Codigo,
                Nombre = ciudadDTO.Nombre,
                Departamento = ciudadDTO.Departamento,
                Pais = ciudadDTO.Pais,
                TarifaIca = ciudadDTO.TarifaIca,
                TarifaReteIca = ciudadDTO.TarifaReteIca,



            };
        }
    }
}
