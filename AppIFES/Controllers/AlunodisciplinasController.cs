﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppIFES.Models;

namespace AppIFES.Controllers
{
    public class AlunodisciplinasController : Controller
    {


        private DadosBanco db = new DadosBanco();

        // GET: Alunodisciplinas
        public ActionResult Index(int? id)
        {
            if ((Session["Userid"] == null) || (!Session["UserSupervisor"].Equals("1")))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewBag.idIndex = id;
            List<Alunodisciplina> alunodisciplinas = db.Alunodisciplinas.Where(a => a.idaluno ==id).Include(a => a.aluno).Include(a => a.disciplina).ToList();
            return View(alunodisciplinas);
        }

        // GET: Alunodisciplinas/Details/5
        public ActionResult Details(int? id)
        {
            if ((Session["Userid"] == null) || (!Session["UserSupervisor"].Equals("1")))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alunodisciplina alunodisciplina = db.Alunodisciplinas.Find(id);
            if (alunodisciplina == null)
            {
                return HttpNotFound();
            }
            return View(alunodisciplina);
        }

        // GET: Alunodisciplinas/Create
        public ActionResult Create(int? id)
        {
            if ((Session["Userid"] == null) || (!Session["UserSupervisor"].Equals("1")))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewBag.id = id;
            ViewBag.idaluno = new SelectList(db.Alunoes, "idaluno", "nome", id);
            ViewBag.iddisciplina = new SelectList(db.Disciplinas, "iddisciplina", "descricao");
            return View();
        }

        // POST: Alunodisciplinas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "iddisciplina,idaluno")] Alunodisciplina alunodisciplina)
        {
            if ((Session["Userid"] == null) || (!Session["UserSupervisor"].Equals("1")))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (ModelState.IsValid)
            {
                db.Alunodisciplinas.Add(alunodisciplina);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = alunodisciplina.idaluno });
            }

            ViewBag.idaluno = new SelectList(db.Alunoes, "idaluno", "nome", alunodisciplina.idaluno);
            ViewBag.iddisciplina = new SelectList(db.Disciplinas, "iddisciplina", "descricao", alunodisciplina.iddisciplina);
            return View(alunodisciplina);
        }

        // GET: Alunodisciplinas/Edit/5
        public ActionResult Edit(int? id,int? idaluno)
        {
            if ((Session["Userid"] == null) || (!Session["UserSupervisor"].Equals("1")))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alunodisciplina alunodisciplina = db.Alunodisciplinas.Find(id,idaluno);
            if (alunodisciplina == null)
            {
                return HttpNotFound();
            }
            ViewBag.idaluno = new SelectList(db.Alunoes, "idaluno", "nome", alunodisciplina.idaluno);
            ViewBag.iddisciplina = new SelectList(db.Disciplinas, "iddisciplina", "descricao", alunodisciplina.iddisciplina);
            return View(alunodisciplina);
        }

        // POST: Alunodisciplinas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "iddisciplina,idaluno")] Alunodisciplina alunodisciplina)
        {
            if ((Session["Userid"] == null) || (!Session["UserSupervisor"].Equals("1")))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (ModelState.IsValid)
            {
                db.Entry(alunodisciplina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idaluno = new SelectList(db.Alunoes, "idaluno", "nome", alunodisciplina.idaluno);
            ViewBag.iddisciplina = new SelectList(db.Disciplinas, "iddisciplina", "descricao", alunodisciplina.iddisciplina);
            return View(alunodisciplina);
        }

        // GET: Alunodisciplinas/Delete/5
        public ActionResult Delete(int id, int? id2)
        {
            ViewBag.idDelete = id2;
            if ((Session["Userid"] == null) || (!Session["UserSupervisor"].Equals("1")))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alunodisciplina alunodisciplina = db.Alunodisciplinas.Find(id, id2);
            if (alunodisciplina == null)
            {
                return HttpNotFound();
            }
            alunodisciplina.aluno = db.Alunoes.Find(id2);
            alunodisciplina.disciplina = db.Disciplinas.Find(id);
            return View(alunodisciplina);
        }

        // POST: Alunodisciplinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? id2)
        {
            if ((Session["Userid"] == null) || (!Session["UserSupervisor"].Equals("1")))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            Alunodisciplina alunodisciplina = db.Alunodisciplinas.Find(id, id2);
            db.Alunodisciplinas.Remove(alunodisciplina);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = id2 });
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
