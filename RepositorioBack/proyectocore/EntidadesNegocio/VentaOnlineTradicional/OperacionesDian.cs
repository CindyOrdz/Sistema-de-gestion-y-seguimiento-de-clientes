namespace EntidadesNegocio.VentaOnlineTradicional
{
    public static class OperacionesDian
    {

        public static Double RedondeoDIAN(Double entrada, int decimales)
        {
            //Double v1 = Math.Round(entrada, decimales, MidpointRounding.AwayFromZero);
            return Math.Round(entrada, decimales, MidpointRounding.AwayFromZero);
        }
    }
}
