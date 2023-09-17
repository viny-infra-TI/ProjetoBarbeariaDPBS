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
    public class TB_FUNCAOController : Controller
    {
        private DPBSv11Entities1 db = new DPBSv11Entities1();

        // GET: TB_FUNCAO
        public ActionResult Index()
        {
            return View(db.TB_FUNCAO.ToList());
        }

        // GET: TB_FUNCAO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_FUNCAO tB_FUNCAO = db.TB_FUNCAO.Find(id);
            if (tB_FUNCAO == null)
            {
                return HttpNotFound();
            }
            return View(tB_FUNCAO);
        }

        // GET: TB_FUNCAO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TB_FUNCAO/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "COD_FUNCAO,FUNCAO")] TB_FUNCAO tB_FUNCAO)
        {
            if (ModelState.IsValid)
            {
                db.TB_FUNCAO.Add(tB_FUNCAO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tB_FUNCAO);
        }

        // GET: TB_FUNCAO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_FUNCAO tB_FUNCAO = db.TB_FUNCAO.Find(id);
            if (tB_FUNCAO == null)
            {
                return HttpNotFound();
            }
            return View(tB_FUNCAO);
        }

        // POST: TB_FUNCAO/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "COD_FUNCAO,FUNCAO")] TB_FUNCAO tB_FUNCAO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_FUNCAO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tB_FUNCAO);
        }

        // GET: TB_FUNCAO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_FUNCAO tB_FUNCAO = db.TB_FUNCAO.Find(id);
            if (tB_FUNCAO == null)
            {
                return HttpNotFound();
            }
            return View(tB_FUNCAO);
        }

        // POST: TB_FUNCAO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_FUNCAO tB_FUNCAO = db.TB_FUNCAO.Find(id);
            db.TB_FUNCAO.Remove(tB_FUNCAO);
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
