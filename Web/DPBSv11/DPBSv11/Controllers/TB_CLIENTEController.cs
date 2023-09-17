using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DPBSv11.Models;

namespace DPBSv11.Controllers
{
    public class TB_CLIENTEController : Controller
    {
        private DPBSv11Entities1 db = new DPBSv11Entities1();

        // GET: TB_CLIENTE
        public ActionResult Index()
        {
            return View(db.TB_CLIENTE.ToList());
        }



        // GET: TB_CLIENTE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_CLIENTE tB_CLIENTE = db.TB_CLIENTE.Find(id);
            if (tB_CLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(tB_CLIENTE);
        }

        // GET: TB_CLIENTE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TB_CLIENTE/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "COD_CLIENTE,DATA_CAD,NOME,CPF,EMAIL,SENHA,TELEFONE,ACESSO")] TB_CLIENTE tB_CLIENTE)
        {
            tB_CLIENTE.DATA_CAD = DateTime.Now;
            if (ModelState.IsValid)
            {
                using (var context = new DPBSv11Entities1())
                {
                    var cad = context.TB_CLIENTE.FirstOrDefault(x => x.CPF == tB_CLIENTE.CPF || x.EMAIL == tB_CLIENTE.EMAIL);
                    if (cad == null)
                    {
                        db.TB_CLIENTE.Add(tB_CLIENTE);
                        db.SaveChanges();
                        return RedirectToAction("Login", "TB_CLIENTE");
                    }
                }
            }
            ModelState.AddModelError("", "CPF ou E-MAIL já cadastrado");
            return View(tB_CLIENTE);
        }

        // GET: TB_CLIENTE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_CLIENTE tB_CLIENTE = db.TB_CLIENTE.Find(id);
            if (tB_CLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(tB_CLIENTE);
        }

        // POST: TB_CLIENTE/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "COD_CLIENTE,DATA_CAD,NOME,CPF,EMAIL,SENHA,TELEFONE,ACESSO")] TB_CLIENTE tB_CLIENTE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_CLIENTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tB_CLIENTE);
        }

        // GET: TB_CLIENTE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_CLIENTE tB_CLIENTE = db.TB_CLIENTE.Find(id);
            if (tB_CLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(tB_CLIENTE);
        }

        // POST: TB_CLIENTE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_CLIENTE tB_CLIENTE = db.TB_CLIENTE.Find(id);
            db.TB_CLIENTE.Remove(tB_CLIENTE);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TB_CLIENTE login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (DPBSv11Entities1 db = new DPBSv11Entities1())
                {
                    var vLogin = db.TB_CLIENTE.Where(p => p.EMAIL.Equals(login.EMAIL)).FirstOrDefault();
                    /*Verificar se a variavel vLogin está vazia. 
                    Isso pode ocorrer caso o usuário não existe. 
              Caso não exista ele vai cair na condição else.*/
                    if (vLogin != null)
                    {
                        /*Código abaixo verifica se o usuário que retornou na variavel tem está 
                          ativo. Caso não esteja cai direto no else*/





                        if (vLogin.ACESSO.Equals("CLIENTE"))
                        {
                            /*Código abaixo verifica se a senha digitada no site é igual a 
                            senha que está sendo retornada 
                             do banco. Caso não cai direto no else*/

                            if (Equals(vLogin.SENHA, login.SENHA))
                            {
                                FormsAuthentication.SetAuthCookie(vLogin.EMAIL, false);
                                if (Url.IsLocalUrl(returnUrl)
                                && returnUrl.Length > 1
                                && returnUrl.StartsWith("/")
                                && !returnUrl.StartsWith("//")
                                && returnUrl.StartsWith("/\\"))
                                {
                                    return Redirect(returnUrl);
                                }
                                /*código abaixo cria uma session para armazenar o nome do usuário*/
                                                Session["nomeUsuarioLogado"] = vLogin.NOME.ToString();
                                                Session["codigoCliente"] = vLogin.COD_CLIENTE.ToString();
                                                Session["emailCliente"] = vLogin.EMAIL.ToString();
                                                Session["cpfCliente"] = vLogin.CPF.ToString();
                                                Session["telCliente"] = vLogin.TELEFONE.ToString();
                                                Session["senhaCliente"] = vLogin.SENHA.ToString();
                                                Session["comentario"] = vLogin.NOME;
                                /*código abaixo cria uma session para armazenar o sobrenome do usuário*/

                                if (vLogin.ACESSO == "CLIENTE")
                                {
                                    return RedirectToAction("Agendamento", "TB_CLIENTE");
                                }
                               
                            }


                            /*Else responsável da validação da senha*/
                            else
                            {
                                /*Escreve na tela a mensagem de erro informada*/
                                ModelState.AddModelError("", "Senha informada Inválida!!!");
                                /*Retorna a tela de login*/
                                return View(new TB_CLIENTE());
                            }
                        }

                        /*Else responsável por verificar se o usuário está ativo*/
                        else
                        {
                            /*Escreve na tela a mensagem de erro informada*/
                            ModelState.AddModelError("", "E-mail informado inválido!!!");
                            /*Retorna a tela de login*/
                            return View(new TB_CLIENTE());
                        }
                    }





                    /*Else responsável por verificar se o usuário existe*/
                    else
                    {
                        /*Escreve na tela a mensagem de erro informada*/
                      
                        ModelState.AddModelError("", "Você não possui uma conta");
                        /*Retorna a tela de login*/
                        return View(new TB_CLIENTE());
                    }

                }
            }
            /*Caso os campos não esteja de acordo com a solicitação retorna a tela de login 
            com as mensagem dos campos*/
            return View(login);
        }



        //OUTRA MANEIRA DE VALIDAÇÃO

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(TB_CLIENTE u)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (var db = new DPBSv10Entities()) // Nome do entity localizado no Usuario.Context
        //        {
        //            //var login = from a in db.usuarios select a;
        //            var v = db.TB_CLIENTE.Where(a => a.EMAIL.Equals(u.EMAIL) && a.SENHA.Equals(u.SENHA)).FirstOrDefault();
        //            if (v != null)
        //            {
        //                if (v.ACESSO.Equals("CLIENTE"))
        //                {
        //                    Session["nomeUsuarioLogado"] = v.NOME.ToString();
        //                    Session["codigoCliente"] = v.COD_CLIENTE.ToString();
        //                    Session["emailCliente"] = v.EMAIL.ToString();
        //                    Session["cpfCliente"] = v.CPF.ToString();
        //                    return RedirectToAction("Agendamento", "HOME");
        //                }



        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Usuário ou senha inválidos");
        //                return View(new TB_CLIENTE());
        //            }


        //        }


        //    }


        //    return View(u);
        //}


        public ActionResult Login()
        {
            return View();
        }

        //ÁREA DO CLIENTE
        public ActionResult AgendaCliente()
        {
            var x = Convert.ToInt32(Session["codigoCliente"]);
            var tB_AGENDAMENTO = db.TB_AGENDAMENTO.Include(t => t.TB_CLIENTE).Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.SITUACAO == "1" && a.COD_CLIENTE == x);
            //tB_AGENDAMENTO = db.TB_AGENDAMENTO
            return View(tB_AGENDAMENTO.ToList());

        }


        public ActionResult AreaCliente()
        {
            return View();
        }


        //Edit da AreaCliente
        public ActionResult EditClient(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_CLIENTE tB_CLIENTE = db.TB_CLIENTE.Find(id);
            if (tB_CLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(tB_CLIENTE);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClient([Bind(Include = "COD_CLIENTE,NOME,CPF,EMAIL,SENHA,TELEFONE,ACESSO")] TB_CLIENTE tB_CLIENTE)
        {
            if (ModelState.IsValid)
            {


                tB_CLIENTE.DATA_CAD = DateTime.Now;
                tB_CLIENTE.ACESSO = "CLIENTE";
                db.Entry(tB_CLIENTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Login");

            }
            return View(tB_CLIENTE);
        }


        public ActionResult EstilosCliente()
        {
            return View();
        }

        //relatórios
        //public ActionResult RelatorioCliente(int? id)
        //{
        //    var tB_AGENDAMENTO = db.TB_AGENDAMENTO.Include(t => t.TB_CLIENTE).Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.SITUACAO == "1");

        //    return View(tB_AGENDAMENTO.ToList());

        //}


        public ActionResult RelatorioCliente(int? id)
        {
            var x = Convert.ToInt32(Session["codigoCliente"]);
            var tB_AGENDAMENTO = db.TB_AGENDAMENTO.Include(t => t.TB_CLIENTE).Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.SITUACAO == "2" && a.COD_CLIENTE == x);

            return View(tB_AGENDAMENTO.ToList());

        }

      


        public ActionResult PromocoesCliente()
        {
            var tB_PROMOCAO = db.TB_PROMOCAO.Include(t => t.TB_SERVICO);
            return View(tB_PROMOCAO.ToList());
        }

        //CANCELAR SERVIÇO

        public ActionResult Cancelar(int? id, int? agenda)
        {
            Session[("codAgendaCliente")] = id;
            Session[("codTB_AGENDA")] = agenda;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_AGENDAMENTO tB_AGENDAMENTO = db.TB_AGENDAMENTO.Find(id);
            if (tB_AGENDAMENTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.COD_CLIENTE = new SelectList(db.TB_CLIENTE, "COD_CLIENTE", "NOME", tB_AGENDAMENTO.COD_CLIENTE);
            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO, "COD_FUNCIONARIO", "NOME", tB_AGENDAMENTO.COD_FUNCIONARIO);
            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO", tB_AGENDAMENTO.COD_SERVICO);
            return View(tB_AGENDAMENTO);
        }

        // POST: TB_AGENDAMENTO/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancelar([Bind(Include = "COD_AGENDAMENTO,COD_FUNCIONARIO,DATA,HORA,COD_SERVICO,COD_CLIENTE,MOTIVO_CANCELAMENTO")] TB_AGENDAMENTO tB_AGENDAMENTO, [Bind(Include = "COD_AGENDA,DATA,HORA,COD_FUNCIONARIO,COD_SERVICO")] TB_AGENDA tB_AGENDA, int? agenda)
        {
            if (ModelState.IsValid)
            {
                switch (tB_AGENDAMENTO.MOTIVO_CANCELAMENTO)
                {
                    case "Cliente não gostou do preço final do serviço": tB_AGENDAMENTO.MOTIVO_CANCELAMENTO = "Cliente não gostou do preço final do serviço"; break;
                    case "Cliente desistiu do serviço solicitado": tB_AGENDAMENTO.MOTIVO_CANCELAMENTO = "Cliente desistiu do serviço solicitado"; break;
                    case "Cliente não poderá comparecer no dia agendado": tB_AGENDAMENTO.MOTIVO_CANCELAMENTO = "Cliente não poderá comparecer no dia agendado"; break;
                    case "Motivos pessoais": tB_AGENDAMENTO.MOTIVO_CANCELAMENTO = "Motivos pessoais"; break;
                    case "Outros": tB_AGENDAMENTO.MOTIVO_CANCELAMENTO = "Outros"; break;
                }

                tB_AGENDA.COD_AGENDA = Convert.ToInt32(Session["codTB_AGENDA"]);
                tB_AGENDA.DATA = tB_AGENDAMENTO.DATA;
                tB_AGENDA.HORA = tB_AGENDAMENTO.HORA;
                tB_AGENDA.COD_SERVICO = tB_AGENDAMENTO.COD_SERVICO;
                tB_AGENDA.COD_FUNCIONARIO = tB_AGENDAMENTO.COD_FUNCIONARIO;
                tB_AGENDA.SITUACAO = "1";


                tB_AGENDAMENTO.COD_AGENDA = Convert.ToInt32(Session["codTB_AGENDA"]);
                //tB_AGENDAMENTO.MOTIVO_CANCELAMENTO = tB_AGENDAMENTO.MOTIVO_CANCELAMENTO;
                tB_AGENDAMENTO.SITUACAO = "0";
                db.Entry(tB_AGENDA).State = EntityState.Modified;
                db.Entry(tB_AGENDAMENTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AgendaCliente");
            }
            ViewBag.COD_CLIENTE = new SelectList(db.TB_CLIENTE, "COD_CLIENTE", "NOME", tB_AGENDAMENTO.COD_CLIENTE);
            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO, "COD_FUNCIONARIO", "NOME", tB_AGENDAMENTO.COD_FUNCIONARIO);
            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO", tB_AGENDAMENTO.COD_SERVICO);
            return View(tB_AGENDAMENTO);
        }
        public ActionResult Agendamento()
        {
            return View();
        }

        

    }
}
