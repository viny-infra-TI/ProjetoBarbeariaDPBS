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
    public class TB_FUNCIONARIOController : Controller
    {
        private DPBSv11Entities1 db = new DPBSv11Entities1();

        // GET: TB_FUNCIONARIO
        public ActionResult Index()
        {
            var tB_FUNCIONARIO = db.TB_FUNCIONARIO.Include(t => t.TB_FUNCAO).Where(a => a.SITUACAO == "1");
            return View(tB_FUNCIONARIO.ToList());
        }



        public ActionResult FuncionariosDesligados()
        {
            var tB_FUNCIONARIO = db.TB_FUNCIONARIO.Include(t => t.TB_FUNCAO).Where(a => a.SITUACAO =="0");
            return View(tB_FUNCIONARIO.ToList());
        }

        //área do funcionário
        public ActionResult AreaFuncionario(int? id)
        {
            var x = Convert.ToInt32(Session["codFuncionario"]);
             var tB_AGENDAMENTO = db.TB_AGENDAMENTO.Include(t => t.TB_CLIENTE).Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.SITUACAO == "1" && a.COD_FUNCIONARIO == x ); 
            //tB_AGENDAMENTO = db.TB_AGENDAMENTO
            return View(tB_AGENDAMENTO.ToList());
           
                //if (id == null)
                //{
                //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //}
                //TB_AGENDAMENTO tB_AGENDAMENTO = db.TB_AGENDAMENTO.Find(id);
                //if (tB_AGENDAMENTO == null)
                //{
                //    return HttpNotFound();
                //}
                //return View(tB_AGENDAMENTO);
            }



        //RELATÓRIO DE SERVIÇOS FINALIZADOS
        public ActionResult RelatorioFuncionario(int? id)
        {
            var x = Convert.ToInt32(Session["codFuncionario"]);
            var tB_AGENDAMENTO = db.TB_AGENDAMENTO.Include(t => t.TB_CLIENTE).Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.SITUACAO == "2" && a.COD_FUNCIONARIO == x);
           
            return View(tB_AGENDAMENTO.ToList());

        }

        public ActionResult RelatorioFuncionarioCancelado(int? id)
        {
            var x = Convert.ToInt32(Session["codFuncionario"]);
            var tB_AGENDAMENTO = db.TB_AGENDAMENTO.Include(t => t.TB_CLIENTE).Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.SITUACAO == "0" && a.COD_FUNCIONARIO == x);

            return View(tB_AGENDAMENTO.ToList());

        }


        public ActionResult RelatorioGeral(int? id)
        {
            var tB_AGENDAMENTO = db.TB_AGENDAMENTO.Include(t => t.TB_CLIENTE).Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.SITUACAO == "2");

            return View(tB_AGENDAMENTO.ToList());

        }

        public ActionResult RelatorioCancelados(int? id)
        {
            var tB_AGENDAMENTO = db.TB_AGENDAMENTO.Include(t => t.TB_CLIENTE).Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.SITUACAO == "0");

            return View(tB_AGENDAMENTO.ToList());

        }

        //DESLIGAR FUNCIONARIO
        public ActionResult DesligarFuncionario(int? id)
        {
            Session[("nome")] = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_FUNCIONARIO tB_FUNCIONARIO = db.TB_FUNCIONARIO.Find(id);
            if (tB_FUNCIONARIO == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ACESSO = new SelectList(db.TB_FUNCIONARIO, "ACESSO", "ACESSO");
            ViewBag.COD_FUNCAO = new SelectList(db.TB_FUNCAO, "COD_FUNCAO", "FUNCAO", tB_FUNCIONARIO.COD_FUNCAO);
            return View(tB_FUNCIONARIO);
        }

        // POST: TB_FUNCIONARIO/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DesligarFuncionario([Bind(Include = "COD_FUNCIONARIO,,NOME,CPF,EMAIL,COD_FUNCAO")] TB_FUNCIONARIO tB_FUNCIONARIO)
        {
            if (ModelState.IsValid)
            {
                
                tB_FUNCIONARIO.ACESSO = "FUNCIONARIO";
                tB_FUNCIONARIO.DATA_CAD = DateTime.Now;
                tB_FUNCIONARIO.SITUACAO = "0";
                tB_FUNCIONARIO.SENHA = Convert.ToString(Session["senhaFuncionario"]);
                db.Entry(tB_FUNCIONARIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.COD_FUNCAO = new SelectList(db.TB_FUNCAO, "COD_FUNCAO", "FUNCAO", tB_FUNCIONARIO.COD_FUNCAO);
            return View(tB_FUNCIONARIO);
        }





        //FINALIZAR UM SERVIÇO, MUDANDO A SITUAÇAÕ DE 1 PARA 2
        public ActionResult FimServ(int? id)
        {
            Session[("codAgenda")] = id;

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
        public ActionResult FimServ([Bind(Include = "COD_AGENDAMENTO,COD_FUNCIONARIO,DATA,HORA,COD_SERVICO,COD_CLIENTE")] TB_AGENDAMENTO tB_AGENDAMENTO)
        {
            if (ModelState.IsValid)
            {
               //Session[("codAgenda")] = tB_AGENDAMENTO.COD_AGENDAMENTO;
               
                tB_AGENDAMENTO.SITUACAO = "2";
               
                db.Entry(tB_AGENDAMENTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AreaFuncionario");
            }
            ViewBag.COD_CLIENTE = new SelectList(db.TB_CLIENTE, "COD_CLIENTE", "NOME", tB_AGENDAMENTO.COD_CLIENTE);
            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO, "COD_FUNCIONARIO", "NOME", tB_AGENDAMENTO.COD_FUNCIONARIO);
            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO", tB_AGENDAMENTO.COD_SERVICO);
            return View(tB_AGENDAMENTO);
        }






        public ActionResult Agenda(int? id)
        {
            Session[("codAgenda")] = id;
            var situ = new TB_AGENDAMENTO();
            var tB_AGENDAMENTO = db.TB_AGENDAMENTO.Include(t => t.TB_CLIENTE).Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO);

            if (situ.SITUACAO == "2")
            {
                return View(tB_AGENDAMENTO.ToList());

            }
            return View();

        }




        // GET: TB_FUNCIONARIO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_FUNCIONARIO tB_FUNCIONARIO = db.TB_FUNCIONARIO.Find(id);
            if (tB_FUNCIONARIO == null)
            {
                return HttpNotFound();
            }
            return View(tB_FUNCIONARIO);
        }

        // GET: TB_FUNCIONARIO/Create
        public ActionResult Create()
        {
            ViewBag.COD_FUNCAO = new SelectList(db.TB_FUNCAO, "COD_FUNCAO", "FUNCAO");
            ViewBag.ACESSO = new SelectList(db.TB_FUNCIONARIO, "ACESSO", "ACESSO");
            return View();
        }

        // POST: TB_FUNCIONARIO/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "COD_FUNCIONARIO,DATA_CAD,NOME,CPF,EMAIL,SENHA,COD_FUNCAO,ACESSO,SITUACAO")] TB_FUNCIONARIO tB_FUNCIONARIO)
        {

            tB_FUNCIONARIO.DATA_CAD = DateTime.Now;
            if (ModelState.IsValid)
            {
                using (var context = new DPBSv11Entities1())
                {
                    var cad = context.TB_FUNCIONARIO.FirstOrDefault(x => x.CPF == tB_FUNCIONARIO.CPF || x.EMAIL == tB_FUNCIONARIO.EMAIL);
                    if (cad == null)
                    {
                        switch (tB_FUNCIONARIO.ACESSO)
                        {
                            case "ADMINISTRADOR": tB_FUNCIONARIO.ACESSO = "ADMINISTRADOR"; break;
                            case "FUNCIONARIO": tB_FUNCIONARIO.ACESSO = "FUNCIONARIO"; break;
                        }
                        db.TB_FUNCIONARIO.Add(tB_FUNCIONARIO);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }

            ViewBag.COD_FUNCAO = new SelectList(db.TB_FUNCAO, "COD_FUNCAO", "FUNCAO", tB_FUNCIONARIO.COD_FUNCAO);


            ModelState.AddModelError("", "CPF ou E-MAIL já cadastrado");
            ViewBag.cpfcadastrado = "CPF já esta cadastrado em nosso sistema!";
            ViewBag.emailcadastrado = "E-mail já esta cadastrado em nosso sistema!";



            return View(new TB_FUNCIONARIO());
        }


        // GET: TB_FUNCIONARIO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_FUNCIONARIO tB_FUNCIONARIO = db.TB_FUNCIONARIO.Find(id);
            if (tB_FUNCIONARIO == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ACESSO = new SelectList(db.TB_FUNCIONARIO, "ACESSO", "ACESSO");
            ViewBag.COD_FUNCAO = new SelectList(db.TB_FUNCAO, "COD_FUNCAO", "FUNCAO", tB_FUNCIONARIO.COD_FUNCAO);
            return View(tB_FUNCIONARIO);
        }

        // POST: TB_FUNCIONARIO/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "COD_FUNCIONARIO,DATA_CAD,NOME,CPF,EMAIL,SENHA,COD_FUNCAO,ACESSO,SITUACAO")] TB_FUNCIONARIO tB_FUNCIONARIO)
        {
            if (ModelState.IsValid)
            {
                switch (tB_FUNCIONARIO.ACESSO)
                {
                    case "ADMINISTRADOR": tB_FUNCIONARIO.ACESSO = "ADMINISTRADOR"; break;
                    case "FUNCIONARIO": tB_FUNCIONARIO.ACESSO = "FUNCIONARIO"; break;
                }
                db.Entry(tB_FUNCIONARIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.COD_FUNCAO = new SelectList(db.TB_FUNCAO, "COD_FUNCAO", "FUNCAO", tB_FUNCIONARIO.COD_FUNCAO);
            return View(tB_FUNCIONARIO);
        }

        // GET: TB_FUNCIONARIO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_FUNCIONARIO tB_FUNCIONARIO = db.TB_FUNCIONARIO.Find(id);
            if (tB_FUNCIONARIO == null)
            {
                return HttpNotFound();
            }
            return View(tB_FUNCIONARIO);
        }

        // POST: TB_FUNCIONARIO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_FUNCIONARIO tB_FUNCIONARIO = db.TB_FUNCIONARIO.Find(id);
            db.TB_FUNCIONARIO.Remove(tB_FUNCIONARIO);
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





        //FINALIZAR SERVIÇO

        public ActionResult Finalizar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_AGENDAMENTO tB_AGENDAMENTO = db.TB_AGENDAMENTO.Find(id);
            if (tB_AGENDAMENTO == null)
            {
                return HttpNotFound();
            }
            return View(tB_AGENDAMENTO);
        }

        // POST: TB_AGENDAMENTO/Delete/5
        [HttpPost, ActionName("Finalizar")]
        [ValidateAntiForgeryToken]
        public ActionResult FinalizarConfirmed(int id)
        {
            TB_AGENDAMENTO tB_AGENDAMENTO = db.TB_AGENDAMENTO.Find(id);
            db.TB_AGENDAMENTO.Remove(tB_AGENDAMENTO);
            db.SaveChanges();
            return RedirectToAction("AreaFuncionario");
        }








        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TB_FUNCIONARIO login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (DPBSv11Entities1 db = new DPBSv11Entities1())
                {
                    //var codAgenda = new TB_AGENDAMENTO();
                    var vLogin = db.TB_FUNCIONARIO.Where(p => p.EMAIL.Equals(login.EMAIL)).FirstOrDefault();
                    
                    /*Verificar se a variavel vLogin está vazia. 
                    Isso pode ocorrer caso o usuário não existe. 
              Caso não exista ele vai cair na condição else.*/
                    if (vLogin != null)
                    {
                        /*Código abaixo verifica se o usuário que retornou na variavel tem está 
                          ativo. Caso não esteja cai direto no else*/





                        if (vLogin.ACESSO.Equals("ADMINISTRADOR") && Equals(vLogin.SITUACAO, "1") || vLogin.ACESSO.Equals("FUNCIONARIO") && Equals(vLogin.SITUACAO, "1"))
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
                                Session["codFuncionario"] = vLogin.COD_FUNCIONARIO.ToString();
                                //Session["codAgenda"] = codAgenda.COD_AGENDAMENTO.ToString();
                                Session["nomeUsuarioLogado"] = vLogin.NOME.ToString();
                                Session["cpfFuncionario"] = vLogin.CPF.ToString();
                                Session["emailFuncionario"] = vLogin.EMAIL.ToString();
                                Session["senhaFuncionario"] = vLogin.SENHA.ToString();
                                Session["codFuncao"] = vLogin.COD_FUNCAO.ToString();
                                




                                /*código abaixo cria uma session para armazenar o sobrenome do usuário*/

                                if (vLogin.ACESSO == "ADMINISTRADOR")
                                {
                                    return RedirectToAction("Index", "TB_FUNCIONARIO");
                                }
                                else if (vLogin.ACESSO == "FUNCIONARIO")
                                {
                                    return RedirectToAction("AreaFuncionario", "TB_FUNCIONARIO");
                                }
                            }


                            /*Else responsável da validação da senha*/
                            else
                            {
                                /*Escreve na tela a mensagem de erro informada*/
                                ModelState.AddModelError("", "Senha informada Inválida!!!");

                                /*Retorna a tela de login*/
                                return View(new TB_FUNCIONARIO());
                            }
                        }

                        /*Else responsável por verificar se o usuário está ativo*/
                        else
                        {
                            /*Escreve na tela a mensagem de erro informada*/
                            ModelState.AddModelError("", "Usuário não autorizado para usar o sistema!!!");
                            /*Retorna a tela de login*/
                            return View(new TB_FUNCIONARIO());
                        }
                    }





                    /*Else responsável por verificar se o usuário existe*/
                    else
                    {
                        /*Escreve na tela a mensagem de erro informada*/
                        ModelState.AddModelError("", "E-mail informado inválido!!!");
                        /*Retorna a tela de login*/
                        return View(new TB_FUNCIONARIO());
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
        //public ActionResult Login(TB_FUNCIONARIO u)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (var db = new DPBSv10Entities()) // Nome do entity localizado no Usuario.Context
        //        {
        //            //var login = from a in db.usuarios select a;
        //            var v = db.TB_FUNCIONARIO.Where(a => a.EMAIL.Equals(u.EMAIL) && a.SENHA.Equals(u.SENHA)).FirstOrDefault();
        //            if (v != null)
        //            {
        //                if (v.ACESSO.Equals("ADMINISTRADOR"))
        //                {
        //                    Session["nomeUsuarioLogado"] = v.NOME.ToString();
        //                    Session["login"] = v.NOME.ToString();
        //                    return RedirectToAction("Index", "TB_FUNCIONARIO");
        //                }
        //                if (v.ACESSO.Equals("FUNCIONARIO"))
        //                {
        //                    Session["nomeUsuarioLogado"] = v.NOME.ToString();
        //                    return RedirectToAction("AreaFuncionario", "TB_FUNCIONARIO");
        //                }

        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Usuário ou senha inválidos");
        //                return View(new TB_FUNCIONARIO());
        //            }
        //        }

        //    }


        //    return View(u);
        //}


        public ActionResult Login()
        {

            return View();

        }

        //PERFIL FUNCIONARIO
        public ActionResult Perfil()
        {

            return View();

        }


        public ActionResult EditarFunc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_FUNCIONARIO tB_FUNCIONARIO = db.TB_FUNCIONARIO.Find(id);
            if (tB_FUNCIONARIO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ACESSO = new SelectList(db.TB_FUNCIONARIO, "ACESSO", "ACESSO");
            ViewBag.COD_FUNCAO = new SelectList(db.TB_FUNCAO, "COD_FUNCAO", "FUNCAO", tB_FUNCIONARIO.COD_FUNCAO);
            return View(tB_FUNCIONARIO);
        }

        // POST: TB_FUNCIONARIO/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarFunc([Bind(Include = "COD_FUNCIONARIO,NOME,CPF,EMAIL,SENHA")] TB_FUNCIONARIO tB_FUNCIONARIO)
        {
            if (ModelState.IsValid)
            {
                tB_FUNCIONARIO.DATA_CAD = DateTime.Now;
                tB_FUNCIONARIO.ACESSO = "FUNCIONARIO";
                tB_FUNCIONARIO.SITUACAO = "1";
                tB_FUNCIONARIO.COD_FUNCAO = Convert.ToInt32(Session["codFuncao"]);
                //tB_FUNCIONARIO.COD_FUNCAO = Convert.ToInt32("BARBEIRO");
                db.Entry(tB_FUNCIONARIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            ViewBag.COD_FUNCAO = new SelectList(db.TB_FUNCAO, "COD_FUNCAO", "FUNCAO", tB_FUNCIONARIO.COD_FUNCAO);
            return View(tB_FUNCIONARIO);
        }

        // GET: TB_FUNCIONARIO/Delete/5




        //public ActionResult Agenda(int? id)
        //{
        //    id = Convert.ToInt32(Session["codFuncionario"]);
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TB_FUNCIONARIO tB_FUNCIONARIO = db.TB_FUNCIONARIO.Find(id);
        //    if (tB_FUNCIONARIO == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tB_FUNCIONARIO);
        //}



        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_AGENDAMENTO tB_AGENDAMENTO = db.TB_AGENDAMENTO.Find(id);
            if (tB_AGENDAMENTO == null)
            {
                return HttpNotFound();
            }
            return View(tB_AGENDAMENTO);
        }



    }
}
