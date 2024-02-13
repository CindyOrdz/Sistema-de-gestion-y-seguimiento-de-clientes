using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RegistroEvidencia
{
    public class RegistroArchivo
    {
        private long id;
        private byte[] archivo;
        private String extension;
        private String tipo;
        private String nombre;
        private String rutaRelativa;
        private DateTime fechaRegistro;
        private DateTime fechaCaptura;
       

        public RegistroArchivo(long id,String ruta, String extension, String tipo, String nombre, DateTime fechaRegistro, DateTime fechaCaptura)
        {
            this.id = id;
            this.rutaRelativa = ruta;
            this.extension = extension;
            this.tipo = tipo;
            this.nombre = nombre;
            this.fechaCaptura = fechaCaptura;
            this.fechaRegistro = fechaRegistro;
        }

        public RegistroArchivo(byte[] archivo, String extension, String tipo, String nombre,DateTime fechaCaptura)
        {
            this.archivo = archivo;
            this.extension = extension;
            this.tipo = tipo;
            this.nombre = nombre;
            this.fechaCaptura = fechaCaptura;
        }

        public RegistroArchivo() { }

        public long ObtenerIdArchivo()
        {
            return id;
        }

        public String ObtenerNombreArchivo()
        {
            return nombre;
        }

        public String ObtenerTipoArchivo()
        {
            return tipo;
        }

        public String ObtenerExtensionArchivo()
        {
            return extension;
        }

        public String ObtenerRutaRelativaArchivo()
        {
            return rutaRelativa;
        }

        public DateTime ObtenerFechaCaptura()
        {
            return fechaCaptura;
        }

        public DateTime ObtenerFechaRegistro()
        {
            return fechaRegistro;
        }

        public byte[] ObtenerArchivo()
        {
            return archivo;
        }

        public async Task GuardarArchivoVisita(BigInteger id)
        {
            // Leer el contenido del archivo JSON
            string configJson = File.ReadAllText("Config.json");

            // Analizar el JSON en un objeto JsonDocument
            using (JsonDocument document = JsonDocument.Parse(configJson))
            {
                // Obtener rutaCarpetaDestino del objeto JsonDocument
                string? rutaCarpetaDestino = document.RootElement.GetProperty("rutaCarpetaDestinoVisita").GetString();

                string nombreCarpetaVisita = "IDV" + id.ToString(); 

                // Comprueba si la carpeta con el idvisita existe, y créala si no existe.
                string rutaCarpetaVisita = Path.Combine(rutaCarpetaDestino, nombreCarpetaVisita);
                if (!Directory.Exists(rutaCarpetaVisita))
                {
                    Directory.CreateDirectory(rutaCarpetaVisita);
                }

                string rutaCompleta = Path.Combine(rutaCarpetaVisita, $"{nombreCarpetaVisita}_{nombre}{extension}");
                rutaRelativa = rutaCompleta;

                await File.WriteAllBytesAsync(rutaCompleta, archivo);
            }
        }

        public async Task GuardarArchivoRevision(BigInteger idRevision, int? idAnomalia = null)
        {
            // Leer el contenido del archivo JSON
            string configJson = File.ReadAllText("Config.json");

            // Analizar el JSON en un objeto JsonDocument
            using (JsonDocument document = JsonDocument.Parse(configJson))
            {
                // Obtener rutaCarpetaDestino del objeto JsonDocument
                string? rutaCarpetaDestino = document.RootElement.GetProperty("rutaCarpetaDestinoRevision").GetString();

                string nombreCarpetaRevision = "REV" + idRevision.ToString();
                string rutaCompleta = "";

                // Comprueba si la carpeta con el idvisita existe, y la crea en caso de que no.
                string rutaCarpetaRevision = Path.Combine(rutaCarpetaDestino, nombreCarpetaRevision);
                if (!Directory.Exists(rutaCarpetaRevision))
                {
                    Directory.CreateDirectory(rutaCarpetaRevision);
                }

                if (idAnomalia.HasValue)
                {
                    string subcarpetaAnomalia = Path.Combine(rutaCarpetaRevision, "Anomalias");

                    // Comprueba si la carpeta anomalias existe, y la crea en caso de que no.
                    if (!Directory.Exists(subcarpetaAnomalia))
                    {
                        Directory.CreateDirectory(subcarpetaAnomalia);
                    }

                    rutaCompleta = Path.Combine(subcarpetaAnomalia, $"AN{idAnomalia.ToString()}_{nombre}{extension}");
                }
                else
                {
                    rutaCompleta = Path.Combine(rutaCarpetaRevision, $"{nombreCarpetaRevision}_{nombre}{extension}");
                }

                
                rutaRelativa = rutaCompleta;

                await File.WriteAllBytesAsync(rutaCompleta, archivo);
            }
        }

        public void BorrarArchivo(string rutaArchivo)
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    Console.WriteLine("El archivo no existe en la ruta especificada");
                    return;
                }

                File.Delete(rutaArchivo);
                Console.WriteLine("El archivo se ha borrado correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al borrar el archivo: {ex.Message}");
            }


        }

        public override string ToString()
        {
            return "RegistroArchivo{" + "archivo=" + archivo + ", extension=" + extension + ", tipo=" + tipo + ", nombre=" + nombre + ", fechaRegistro=" + fechaRegistro.ToString("d") + ", fechaCaptura=" + fechaCaptura.ToString("d") +  "}";
        }


    }
}
