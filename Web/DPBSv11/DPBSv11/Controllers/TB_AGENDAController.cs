using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DPBSv11.Models;

namespace DPBSv11.Controllers
{
    public class TB_AGENDAController : Controller
    {
        private DPBSv11Entities1 db = new DPBSv11Entities1();

        // GET: TB_AGENDA
        public ActionResult Index()
        {
            var tB_AGENDA = db.TB_AGENDA.Where(a => a.SITUACAO == "1").Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO);
            return View(tB_AGENDA.ToList());
        }

        public ActionResult AgendaDisponivelCabelo()
        {
         
            var tB_AGENDA = db.TB_AGENDA.Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.COD_SERVICO == 1 && a.SITUACAO == "1");
            return View(tB_AGENDA.ToList());
        }

        public ActionResult AgendaDisponivelKids()
        {

            var tB_AGENDA = db.TB_AGENDA.Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.COD_SERVICO == 4 && a.SITUACAO == "1");
            return View(tB_AGENDA.ToList());
        }

       

        public ActionResult AgendaDisponivelCabeloBarba()
        {

            var tB_AGENDA = db.TB_AGENDA.Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.COD_SERVICO == 3 && a.SITUACAO == "1");
            return View(tB_AGENDA.ToList());
        }

        public ActionResult AgendaBarba()
        {

            var tB_AGENDA = db.TB_AGENDA.Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO).Where(a => a.COD_SERVICO == 2 && a.SITUACAO == "1");
            return View(tB_AGENDA.ToList());
        }

        // GET: TB_AGENDA/Details/5
        public ActionResult Details(int? id)
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
            return View(tB_AGENDA);
        }

        // GET: TB_AGENDA/Create
        public ActionResult Create()
        {
            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO.Where(a => a.ACESSO == "FUNCIONARIO" && a.SITUACAO == "1"), "COD_FUNCIONARIO", "NOME");
            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO");
            return View();
        }

        // POST: TB_AGENDA/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "COD_AGENDA,DATA,HORA,COD_FUNCIONARIO,COD_SERVICO,SITUACAO")] TB_AGENDA tB_AGENDA)
        {
            if (ModelState.IsValid)
            {
                db.TB_AGENDA.Add(tB_AGENDA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO, "COD_FUNCIONARIO", "NOME", tB_AGENDA.COD_FUNCIONARIO);
            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO", tB_AGENDA.COD_SERVICO);
            return View(tB_AGENDA);
        }


        //LISTAGEM DE HORARIOS DISPONIVEIS
        public ActionResult AgendaDisponivel()
        {
            var tB_AGENDA = db.TB_AGENDA.Where(a => a.SITUACAO == "1").Include(t => t.TB_FUNCIONARIO).Include(t => t.TB_SERVICO);
            return View(tB_AGENDA.ToList());
        }


        //UPDATE SITUACAO PARA 2 DA AGENDA
        public ActionResult UpdateAgenda(int? id)
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
        public ActionResult UpdateAgenda([Bind(Include = "COD_AGENDA,DATA,HORA,COD_FUNCIONARIO,COD_SERVICO")] TB_AGENDA tB_AGENDA)
        {
            if (ModelState.IsValid)
            {
                var a = tB_AGENDA.COD_AGENDA;
                Session["codAgenda"] = a;
                Session["codTB_AGENDA"] = tB_AGENDA.COD_AGENDA;
                
                Session["data"] = tB_AGENDA.DATA;
                Session["hora"] = tB_AGENDA.HORA;
                Session["servico"] = tB_AGENDA.COD_SERVICO;
                Session["funcionario"] = tB_AGENDA.COD_FUNCIONARIO;

                tB_AGENDA.SITUACAO = "2";
                db.Entry(tB_AGENDA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AGENDAMENTOTESTE", "HOME");
            }
            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO.Where(a => a.ACESSO == "FUNCIONARIO" && a.SITUACAO == "1"), "COD_FUNCIONARIO", "NOME", tB_AGENDA.COD_FUNCIONARIO);
            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO", tB_AGENDA.COD_SERVICO);
            return View(tB_AGENDA);
        }



        // GET: TB_AGENDA/Edit/5
        public ActionResult Edit(int? id)
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
        public ActionResult Edit([Bind(Include = "COD_AGENDA,DATA,HORA,COD_FUNCIONARIO,COD_SERVICO")] TB_AGENDA tB_AGENDA)
        {
            if (ModelState.IsValid)
            {
               
                tB_AGENDA.SITUACAO = "2";
                db.Entry(tB_AGENDA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO.Where(a => a.ACESSO == "FUNCIONARIO" && a.SITUACAO == "1"), "COD_FUNCIONARIO", "NOME", tB_AGENDA.COD_FUNCIONARIO);
            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO", tB_AGENDA.COD_SERVICO);
            return View(tB_AGENDA);
        }

        // GET: TB_AGENDA/Delete/5
        public ActionResult Delete(int? id)
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
            return View(tB_AGENDA);
        }

        // POST: TB_AGENDA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_AGENDA tB_AGENDA = db.TB_AGENDA.Find(id);
            db.TB_AGENDA.Remove(tB_AGENDA);
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
    }
}
