using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DPBSv11.Models;

namespace DPBSv10.Controllers
{
    public class TB_AGENDAMENTOController : Controller
    {
        private DPBSv11Entities1 db = new DPBSv11Entities1();

        // GET: TB_AGENDAMENTO
        public ActionResult Index()
        {
            var tB_AGENDAMENTO = db.TB_AGENDAMENTO.Include(t => t.TB_CLIENTE).Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.SITUACAO == "1");
            return View(tB_AGENDAMENTO.ToList());

        }



        //CONTROLE DA AGENDA
        public ActionResult AgendaCancelados()
        {
            var tB_AGENDAMENTO = db.TB_AGENDAMENTO.Include(t => t.TB_CLIENTE).Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.SITUACAO == "0");
            return View(tB_AGENDAMENTO.ToList());

        }

        public ActionResult AgendaFinalizados()
        {
            var tB_AGENDAMENTO = db.TB_AGENDAMENTO.Include(t => t.TB_CLIENTE).Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.SITUACAO == "2");
            return View(tB_AGENDAMENTO.ToList());

        }


        // GET: TB_AGENDAMENTO/Details/5
        public ActionResult Details(int? id)
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

        // GET: TB_AGENDAMENTO/Create
        public ActionResult Create()
        {
            ViewBag.COD_CLIENTE = new SelectList(db.TB_CLIENTE, "COD_CLIENTE", "NOME");
            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO, "COD_FUNCIONARIO", "NOME");
            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO");
            return View();
        }

        // POST: TB_AGENDAMENTO/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "COD_AGENDAMENTO,DATA,HORA,COD_FUNCIONARIO,COD_SERVICO,COD_CLIENTE,SITUACAO")] TB_AGENDAMENTO tB_AGENDAMENTO)
        {
            if (ModelState.IsValid)
            {
                db.TB_AGENDAMENTO.Add(tB_AGENDAMENTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.COD_CLIENTE = new SelectList(db.TB_CLIENTE, "COD_CLIENTE", "NOME", tB_AGENDAMENTO.COD_CLIENTE);
            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO, "COD_FUNCIONARIO", "NOME", tB_AGENDAMENTO.COD_FUNCIONARIO);
            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO", tB_AGENDAMENTO.COD_SERVICO);
            return View(tB_AGENDAMENTO);
        }

        // GET: TB_AGENDAMENTO/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.COD_CLIENTE = new SelectList(db.TB_CLIENTE, "COD_CLIENTE", "NOME", tB_AGENDAMENTO.COD_CLIENTE);
            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO.Where(a => a.ACESSO == "FUNCIONARIO" && a.SITUACAO == "1"), "COD_FUNCIONARIO", "NOME", tB_AGENDAMENTO.COD_FUNCIONARIO);
            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO", tB_AGENDAMENTO.COD_SERVICO);
            return View(tB_AGENDAMENTO);
        }

        // POST: TB_AGENDAMENTO/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "COD_AGENDAMENTO,DATA,HORA,COD_FUNCIONARIO,COD_SERVICO,COD_CLIENTE,SITUACAO")] TB_AGENDAMENTO tB_AGENDAMENTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_AGENDAMENTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.COD_CLIENTE = new SelectList(db.TB_CLIENTE, "COD_CLIENTE", "NOME", tB_AGENDAMENTO.COD_CLIENTE);
            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO, "COD_FUNCIONARIO", "NOME", tB_AGENDAMENTO.COD_FUNCIONARIO);
            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO", tB_AGENDAMENTO.COD_SERVICO);
            return View(tB_AGENDAMENTO);
        }

        // GET: TB_AGENDAMENTO/Delete/5
        public ActionResult Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_AGENDAMENTO tB_AGENDAMENTO = db.TB_AGENDAMENTO.Find(id);
            db.TB_AGENDAMENTO.Remove(tB_AGENDAMENTO);
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

        public ActionResult FiltrarAgenda()
        {
            //var tB_AGENDAMENTO = db.TB_AGENDAMENTO.Include(t => t.TB_CLIENTE).Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.SITUACAO == "2" || a.SITUACAO == "3");
            //return View(tB_AGENDAMENTO.ToList());

            var tB_AGENDA = db.TB_AGENDA.Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.SITUACAO == "3");
            return View(tB_AGENDA.ToList());


        }

        //public ActionResult teste(int? id)
        //{
        //    return View(db.TB_AGENDAMENTO
        //        .Where(x => x.COD_AGENDAMENTO == id) 
                
        //        .OrderBy(x => x.COD_FUNCIONARIO)
        //        .ToList<TB_AGENDAMENTO>());
        //}



        public ActionResult Agendamento()
        {
            return View();
        }


        //RELATORIO CLIENTE CANCELADOS
        public ActionResult RelatorioClienteCancelado(int? id)
        {
            var x = Convert.ToInt32(Session["codigoCliente"]);
            var tB_AGENDAMENTO = db.TB_AGENDAMENTO.Include(t => t.TB_CLIENTE).Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.SITUACAO == "0" && a.COD_CLIENTE == x);

            return View(tB_AGENDAMENTO.ToList());

        }



        //public ActionResult Agenda(int? id)
        //{
        //    var v = new TB_AGENDAMENTO();

        //    id = Convert.ToInt32(Session["codFuncionario"]);
        //    if (id == v.COD_FUNCIONARIO)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var tB_AGENDAMENTO = db.TB_AGENDAMENTO.Where(a => a.COD_FUNCIONARIO == id).ToList();
        //    if (tB_AGENDAMENTO == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tB_AGENDAMENTO);
        //}





    }
}
