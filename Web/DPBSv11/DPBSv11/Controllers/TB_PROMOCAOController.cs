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
    public class TB_PROMOCAOController : Controller
    {
        private DPBSv11Entities1 db = new DPBSv11Entities1();

        // GET: TB_PROMOCAO
        public ActionResult Index()
        {
            var tB_PROMOCAO = db.TB_PROMOCAO.Include(t => t.TB_SERVICO);
            return View(tB_PROMOCAO.ToList());
        }


        public ActionResult PromocoesCli()
        {
            var tB_PROMOCAO = db.TB_PROMOCAO.Include(t => t.TB_SERVICO);
            return View(tB_PROMOCAO.ToList());
        }


        // GET: TB_PROMOCAO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_PROMOCAO tB_PROMOCAO = db.TB_PROMOCAO.Find(id);
            if (tB_PROMOCAO == null)
            {
                return HttpNotFound();
            }
            return View(tB_PROMOCAO);
        }

        // GET: TB_PROMOCAO/Create
        public ActionResult Create()
        {
            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO");
            return View();
        }

        // POST: TB_PROMOCAO/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "COD_PROMOCAO,COD_SERVICO,DESCRICAO,VALOR,DATA_INI,DATA_FIM")] TB_PROMOCAO tB_PROMOCAO)
        {
            if (ModelState.IsValid)
            {
                db.TB_PROMOCAO.Add(tB_PROMOCAO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO", tB_PROMOCAO.COD_SERVICO);
            return View(tB_PROMOCAO);
        }

        // GET: TB_PROMOCAO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_PROMOCAO tB_PROMOCAO = db.TB_PROMOCAO.Find(id);
            if (tB_PROMOCAO == null)
            {
                return HttpNotFound();
            }
            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO", tB_PROMOCAO.COD_SERVICO);
            return View(tB_PROMOCAO);
        }

        // POST: TB_PROMOCAO/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "COD_PROMOCAO,COD_SERVICO,DESCRICAO,VALOR,DATA_INI,DATA_FIM")] TB_PROMOCAO tB_PROMOCAO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_PROMOCAO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.COD_SERVICO = new SelectList(db.TB_SERVICO, "COD_SERVICO", "SERVICO", tB_PROMOCAO.COD_SERVICO);
            return View(tB_PROMOCAO);
        }

        // GET: TB_PROMOCAO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_PROMOCAO tB_PROMOCAO = db.TB_PROMOCAO.Find(id);
            if (tB_PROMOCAO == null)
            {
                return HttpNotFound();
            }
            return View(tB_PROMOCAO);
        }

        // POST: TB_PROMOCAO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_PROMOCAO tB_PROMOCAO = db.TB_PROMOCAO.Find(id);
            db.TB_PROMOCAO.Remove(tB_PROMOCAO);
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

        //Visualização das promoções
        public ActionResult Promocoes()
        {
            var tB_PROMOCAO = db.TB_PROMOCAO.Include(t => t.TB_SERVICO);
            return View(tB_PROMOCAO.ToList());
        }

        public ActionResult DetalhesPromocoes(int? id)
        {

            Session[("codPromo")] = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_PROMOCAO tB_PROMOCAO = db.TB_PROMOCAO.Find(id);
            if (tB_PROMOCAO == null)
            {
                return HttpNotFound();
            }
            return View(tB_PROMOCAO);
        }



        public ActionResult DetalhesPromocoesCli(int? id)
        {

            Session[("codPromo")] = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_PROMOCAO tB_PROMOCAO = db.TB_PROMOCAO.Find(id);
            if (tB_PROMOCAO == null)
            {
                return HttpNotFound();
            }
            return View(tB_PROMOCAO);
        }

    }
}
