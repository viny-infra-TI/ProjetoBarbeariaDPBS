using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DPBSv11.Models;
using DPBSv11.ViewModel;
using System.Data.Entity;
using System.Net;

namespace DPBSv11.Controllers
{
    public class HomeController : Controller
    {
        private DPBSv11Entities1 db = new DPBSv11Entities1();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Estilos()
        {
            return View();
        }

        public ActionResult Sobre()
        {
            return View();
        }

        public ActionResult Login()
        {

            {
                return View();
            }
        }

        public ActionResult LoginFuncionario()
        {
            return View();
        }



        public ActionResult AgendamentoKids()
        {
            return View();
        }

        public ActionResult AgendamentoConcluido()
        {
            return View();
        }

        public ActionResult AgendamentoCabelo()
        {
            return View();
        }

        public ActionResult AgendamentoBarbaCabelo()
        {
            return View();
        }

        public ActionResult AgendamentoBarba()
        {
            return View();
        }





        public ActionResult CadastrarFuncionarioTeste()
        {
            {
                return View();
            }
        }


        public ActionResult TESTEINDEX()
        {
            return View();
        }

        // GET: Pedido
        public ActionResult AGENDAMENTOTESTE()
        {
            DPBSv11Entities1 db = new DPBSv11Entities1();

            TB_CLIENTE cli = new TB_CLIENTE();
            TB_AGENDAMENTO agend = new TB_AGENDAMENTO();
            TB_FUNCIONARIO func = new TB_FUNCIONARIO();
            TB_SERVICO serv = new TB_SERVICO();

            //List<AgendamentoViewModel> listaAgendamento = new List<AgendamentoViewModel>();
            //String cliente = Session["nomeUsuarioLogado"].ToString();

            //var listaAgendamentoCliente = (from cli in db.TB_CLIENTE
            //                               join agen in db.TB_AGENDAMENTO on
            //          cli.COD_CLIENTE equals agen.COD_CLIENTE
            //                               select new
            //                               { cli.NOME, cli.EMAIL, cli.CPF }).ToList();
            //foreach (var item in listaAgendamentoCliente)
            //{
            //    AgendamentoViewModel cliVM = new AgendamentoViewModel();
            //    cliVM.NOME = item.NOME;
            //    cliVM.EMAIL = item.EMAIL;
            //    cliVM.CPF = item.CPF;
            //    listaAgendamento.Add(cliVM);
            //}
            //ViewBag.model1 = listaAgendamento;

            // if (func.ACESSO == "FUNCIONARIO") { 


            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO.Where(a => a.ACESSO == "FUNCIONARIO" && a.SITUACAO == "1"), "COD_FUNCIONARIO", "NOME");
            //}

            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO");

            ViewBag.Valor = new SelectList(db.TB_SERVICO, "COD_SERVICO", "VALOR");

            ViewBag.DATA = new SelectList(db.TB_AGENDA.Where(a => a.SITUACAO == "3"), "DATA", "DATA");
            ViewBag.HORA = new SelectList(db.TB_AGENDA.Where(a => a.SITUACAO == "3"), "HORA", "HORA");

            //var tempo = db.TB_AGENDA.Select(a => new
            //{
            //    COD_AGENDA = a.COD_AGENDA,
            //    HORA = a.HORA

            //}).ToList();

            //ViewBag.HORA = new MultiSelectList(tempo, "COD_AGENDA", "HORA");
            return View();


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AGENDAMENTOTESTE(AgendamentoViewModel cliente)
        {
            TB_CLIENTE cli = new TB_CLIENTE();
            TB_AGENDAMENTO agend = new TB_AGENDAMENTO();
            TB_FUNCIONARIO func = new TB_FUNCIONARIO();
            TB_SERVICO serv = new TB_SERVICO();
            TB_AGENDA agenda = new TB_AGENDA();




            //primeiro cadastro a pessoa da tabela cliente
            if (ModelState.IsValid)
            {
                using (DPBSv11Entities1 db = new DPBSv11Entities1())
                {
                    var z = new SelectList(db.TB_AGENDA.Where(a => a.SITUACAO == "3"), "DATA", "DATA");
                    var x = Convert.ToInt32(Session["codigoCliente"]);
                    agend.COD_CLIENTE = Convert.ToInt32(Session["codigoCliente"]);
                    agend.COD_AGENDA = Convert.ToInt32(Session["codTB_AGENDA"]);
                    agend.DATA = Convert.ToDateTime(Session["data"]);
                    agend.HORA = Convert.ToDateTime(Session["hora"]);
                    agend.COD_FUNCIONARIO = Convert.ToInt32(Session["funcionario"]);
                    agend.COD_SERVICO = Convert.ToInt32(Session["servico"]);
                    agend.SITUACAO = cliente.SITUACAO;


                    db.TB_AGENDAMENTO.Add(agend);
                    db.SaveChanges();

                    
                

                        return RedirectToAction("AgendamentoConcluido", "Home");
                   
                }

                }
            return View();
        }

        //cancelar agenda
        public ActionResult CancelarAgenda(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_AGENDA tB_AGENDA = db.TB_AGENDA.Find(id);
            if (tB_AGENDA == null)
            {
                return HttpNotFound();
            }
            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO, "COD_FUNCIONARIO", "NOME", tB_AGENDA.COD_FUNCIONARIO);
            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO", tB_AGENDA.COD_SERVICO);
            return View(tB_AGENDA);
        }

        // POST: TB_AGENDA/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelarAgenda([Bind(Include = "COD_AGENDA,DATA,HORA,COD_FUNCIONARIO,COD_SERVICO")] TB_AGENDA tB_AGENDA)
        {
            if (ModelState.IsValid)
            {
                //Session["codAgenda"] = tB_AGENDA.COD_AGENDA;
                Session["data"] = tB_AGENDA.DATA;
                Session["hora"] = tB_AGENDA.HORA;
                Session["servico"] = tB_AGENDA.COD_SERVICO;
                Session["funcionario"] = tB_AGENDA.COD_FUNCIONARIO;

                tB_AGENDA.SITUACAO = "1";
                db.Entry(tB_AGENDA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AgendaDisponivel", "TB_AGENDA");
            }
            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO.Where(a => a.ACESSO == "FUNCIONARIO" && a.SITUACAO == "1"), "COD_FUNCIONARIO", "NOME", tB_AGENDA.COD_FUNCIONARIO);
            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO", tB_AGENDA.COD_SERVICO);
            return View(tB_AGENDA);
        }


    }
}