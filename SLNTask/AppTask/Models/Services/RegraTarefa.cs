using AppTask.Models.interfaces;

namespace AppTask.Models.Services
{
    public class RegraTarefa : IRegraTarefa
    {
        public bool validarDataFinal(DateTime? datainicial, DateTime? datafinal)
        {
            return datainicial<datafinal;
        }
    }
}
