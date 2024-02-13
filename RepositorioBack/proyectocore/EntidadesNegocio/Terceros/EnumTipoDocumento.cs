namespace EntidadesNegocio.Terceros
{
    public enum EnumTipoDocumento
    {
        //    '11', 'Registro Civil de Nacimiento'
        //    '12', 'Tarjeta de Identidad'
        //    '13', 'Cedula de Ciudadania'
        //    '21', 'Tarjeta de Extranjeria'
        //    '22', 'Cedula de Extranjeria'
        //    '31', 'NIT'
        //    '41', 'Pasaporte'
        //    '42', 'Tipo de documento de extranjeria'
        //    '43', 'Sin identificación del exterior o para uso definido por la DIAN'

        RegistroCivilNacimiento = 11, TarjetaIdentidad = 12, CedulaCiudadania = 13,
        TarjetaExtranjeria = 21, CedulaExtranjeria = 22,
        NIT = 31, Pasaporte = 41, TipoDocumentoExtrajeria = 42,
        SinidentificaciónExterior_UsoDefinidoDIAN = 43

    }
}
