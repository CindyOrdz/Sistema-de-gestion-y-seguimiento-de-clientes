using System.Numerics;


namespace EntidadesNegocio.EntidadesDto
{
    public class RegistroEvidenciaDTO
    {
        public long id { get; set; }
        public byte[] archivo { get; set; }
        public String extension { get; set; }
        public String tipo { get; set; }
        public String nombre { get; set; }
        public String rutaRelativa { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaCaptura { get; set; }
        public String descripcion { get; set; } = string.Empty;

    }
}
