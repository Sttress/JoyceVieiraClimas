//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JoiceVireira.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class PrevisaoClima
    {
        public int Id { get; set; }
        public int CidadeId { get; set; }
        public System.DateTime DataPrevisao { get; set; }
        public string Clima { get; set; }
        public Nullable<decimal> TemperaturaMinima { get; set; }
        public Nullable<decimal> TemperaturaMaxima { get; set; }
    
        public virtual Cidade Cidade { get; set; }
    }
}
