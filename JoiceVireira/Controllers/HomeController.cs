using System;
using JoiceVireira.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JoiceVireira.Services;

namespace JoiceVireira.Controllers
{
    public class HomeController : Controller
    {
        private String ViewPath = "~/Views/Home/Partials";
        ClimaTempoSimplesEntities db = new ClimaTempoSimplesEntities();
        private object _home;

        public ActionResult Index()
        {
            try
            {
                ViewBag.Cidades = new SelectList(db.Cidade.Distinct().Select(e => new
                {
                    ID = e.Nome,
                    DESCRICAO = e.Nome
                }).OrderBy(e => e.DESCRICAO).ToList(), "ID", "DESCRICAO");

                return View();
            }catch (Exception ex)
            {
                return Json(new {mensagem = ex.Message},JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult TabDadosCidadesFrias()
        {
            var rota = ViewPath + "/_ContainerCidades.cshtml";
            var contexto = "frias";
            ViewBag.Titulo = contexto;
            ViewBag.Itens = _Home.PreencherCidadeDTO(contexto);
            return PartialView(rota);
        }

        public ActionResult TabDadosCidadesQuentes()
        {
            var rota = ViewPath + "/_ContainerCidades.cshtml";
            var contexto = "quentes";
            ViewBag.Titulo = contexto;
            ViewBag.Itens = _Home.PreencherCidadeDTO(contexto);
            return PartialView(rota);
        }
        public ActionResult TabDadosDia(string Cidade)
        {
            ViewBag.Cidade = string.Empty;
            ViewBag.Itens = null;

            if (Cidade is string)
            {
                ViewBag.Cidade = Cidade;
                ViewBag.Itens = _Home.PreencherDiaDTO(Cidade);
            }
            else
            {
                ViewBag.Cidade = "Anápolis";
                ViewBag.Itens = _Home.PreencherDiaDTO("Anápolis");

            }

            var rota = ViewPath + "/_ContainerDias.cshtml";
            return PartialView(rota);
        }

        public ActionResult Contact()
        {

            return View();
        }
    }
}