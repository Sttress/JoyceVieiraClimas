using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoiceVireira.Models
{
    public class PrevisaoClima
    {
        public int Id { get; set; }
        public int CidadeId { get; set; }
        public DateTime DataPrevisao { get; set; }
        public string Clima { get; set; }
        public short TemperaturaMinima { get; set; }
        public short TemperaturaMaxima { get; set; }
    }
}