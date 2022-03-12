using JoiceVireira.Data;
using JoiceVireira.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace JoiceVireira.Services
{
    public class _Home
    {
        static ClimaTempoSimplesEntities db = new ClimaTempoSimplesEntities();

        public static List<CidadeDTO> PreencherCidadeDTO(string contexto)
        {
            var lista = new List<CidadeDTO>();
            try
            {
                if(contexto is string)
                {
                    var listaPrev = BuscaCidadesPrevHoje(contexto);
                    foreach (var c in listaPrev)
                    {
                        var item = new CidadeDTO
                        {
                            Cidade = c.Cidade.Nome + "/" + c.Cidade.Estado.UF,
                            Minima = "Min" + c.TemperaturaMinima + "°C",
                            Maxima = "Max" + c.TemperaturaMaxima + "°C"
                        };
                        lista.Add(item);
                    }

                    return lista;
                }
                else
                {
                    throw new Exception("Erro ao preencher conteiner Ciades");
                }

            }catch(Exception ex)
            {
                throw new Exception("Erro ao preencher dados das Cidades" + ex.Message);
            }
        }

        public static List<PrevisaoClima> BuscaCidadesPrevHoje(string contexto)
        {
            try
            {
                var hoje = DateTime.Today;
                var listaCidadesHoje = db.PrevisaoClima.Where(e => e.DataPrevisao == hoje).ToList();
                var listaRetorno = new List<PrevisaoClima>();

                if(contexto == "quentes")
                {
                    listaCidadesHoje = listaCidadesHoje.OrderByDescending(e => e.TemperaturaMaxima).ToList();
                    for(int i = 0;i < 3; i++)
                    {
                        listaRetorno.Add(listaCidadesHoje[i]);
                    }
                    return listaRetorno;
                }
                else
                {
                    listaCidadesHoje = listaCidadesHoje.OrderBy(e => e.TemperaturaMinima).ToList();
                    for (int i = 0; i < 3; i++)
                    {
                        listaRetorno.Add(listaCidadesHoje[i]);
                    }
                    return listaRetorno;
                }

            }catch(Exception ex)
            {
                throw new Exception("Erro ao Buscar Cidades" + ex.Message);
            }
        }

        public static List<DiaDTO> PreencherDiaDTO(string Cidade)
        {
            try
            {
                var hoje = DateTime.Today;
                var fim = hoje.AddDays(7);
                var listaDias = new List<DiaDTO>();
                var cidadeId = db.Cidade.Where(e => e.Nome.ToUpper() == Cidade.ToUpper()).Select(e => e.Id).FirstOrDefault();
                var listaPrev = db.PrevisaoClima.Where(e => e.DataPrevisao >= hoje && e.DataPrevisao < fim && e.CidadeId == cidadeId).ToList();

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                if (listaPrev != null)
                {
                    foreach (var c in listaPrev)
                    {
                        var DTO = new DiaDTO()
                        {
                            DiaSemana = dtfi.GetDayName(c.DataPrevisao.DayOfWeek),
                            Clima = c.Clima,
                            Minima = "Min" + c.TemperaturaMinima + "°C",
                            Maxima = "Max" + c.TemperaturaMaxima + "°C",

                        };
                        listaDias.Add(DTO);
                    }
                }
                else
                {
                    throw new Exception("Não Existe dados de previsão para essa cidade");
                }

                return listaDias;
            }catch (Exception ex)
            {
                throw new Exception("Erro ao buscar os dados dos Dias" + ex.Message);
            }
            
        }
        
    }
}