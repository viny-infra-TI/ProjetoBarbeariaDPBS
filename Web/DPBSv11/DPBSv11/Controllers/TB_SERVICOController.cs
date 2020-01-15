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
    public class TB_SERVICOController : Controller
    {
        private DPBSv11Entities1 db = new DPBSv11Entities1();

        // GET: TB_SERVICO
        public ActionResult Index()
        {
            var tB_SERVICO = db.TB_SERVICO.Include(t => t.TB_FUNCIONARIO);
            return View(tB_SERVICO.ToList());
        }

        // GET: TB_SERVICO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_SERVICO tB_SERVICO = db.TB_SERVICO.Find(id);
            if (tB_SERVICO == null)
            {
                return HttpNotFound();
            }
            return View(tB_SERVICO);
        }

        // GET: TB_SERVICO/Create
        public ActionResult Create()
        {
            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO, "COD_FUNCIONARIO", "NOME");
            return View();
        }

        // POST: TB_SERVICO/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "COD_SERVICO,SERVICO,VALOR,COD_PROMOCAO,COD_FUNCIONARIO")] TB_SERVICO tB_SERVICO)
        {
            if (ModelState.IsValid)
            {
                db.TB_SERVICO.Add(tB_SERVICO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO, "COD_FUNCIONARIO", "NOME", tB_SERVICO.COD_FUNCIONARIO);
            return View(tB_SERVICO);
        }

        // GET: TB_SERVICO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_SERVICO tB_SERVICO = db.TB_SERVICO.Find(id);
            if (tB_SERVICO == null)
            {
                return HttpNotFound();
            }
            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO, "COD_FUNCIONARIO", "NOME", tB_SERVICO.COD_FUNCIONARIO);
            return View(tB_SERVICO);
        }

        // POST: TB_SERVICO/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "COD_SERVICO,SERVICO,VALOR,COD_PROMOCAO,COD_FUNCIONARIO")] TB_SERVICO tB_SERVICO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_SERVICO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.COD_FUNCIONARIO = new SelectList(db.TB_FUNCIONARIO, "COD_FUNCIONARIO", "NOME", tB_SERVICO.COD_FUNCIONARIO);
            return View(tB_SERVICO);
        }

        // GET: TB_SERVICO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_SERVICO tB_SERVICO = db.TB_SERVICO.Find(id);
            if (tB_SERVICO == null)
            {
                return HttpNotFound();
            }
            return View(tB_SERVICO);
        }

        // POST: TB_SERVICO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_SERVICO tB_SERVICO = db.TB_SERVICO.Find(id);
            db.TB_SERVICO.Remove(tB_SERVICO);
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
