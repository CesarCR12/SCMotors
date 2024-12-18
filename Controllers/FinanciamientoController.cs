using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using SCMotors.Models;

namespace SCMotors.Controllers
{
    public class FinanciamientoController : Controller
    {
        private readonly Conexion _conexion;

        public FinanciamientoController()
        {
            _conexion = new Conexion();
        }

        // GET: Financiamiento
        public ActionResult Index()
        {
            return View();
        }

        // GET: Financiamiento/List
        public ActionResult List()
        {
            var financiamiento = _conexion.FinanciamientoCollection.Find(_ => true).ToList();
            return View(financiamiento);
        }

        // GET: Financiamiento/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID inválido.");
            }

            try
            {
                var financiamiento = _conexion.FinanciamientoCollection.Find(f => f.Id == id).FirstOrDefault(); 

                if (financiamiento == null)
                {
                    return HttpNotFound("Cliente no encontrado.");
                }

                return View(financiamiento);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener detalles del cliente (ID: {id}): {ex.Message}";
                return View("Error");
            }
        }


        // GET: Financiamiento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Financiamiento/Create
        [HttpPost]
        public ActionResult Create(Financiamiento financiamiento)
        {
            if (ModelState.IsValid)
            {
                _conexion.FinanciamientoCollection.InsertOne(financiamiento);
                return RedirectToAction("Index");
            }

            return View(financiamiento);
        }

        // GET: Financiamiento/Edit/5
        public ActionResult Edit(string id)
        {
            var financiamiento = _conexion.FinanciamientoCollection
                .Find(f=> f.Id == id)
                .FirstOrDefault();

            if (financiamiento == null)
            {
                return HttpNotFound();
            }

            return View(financiamiento);
        }

        // POST: Financiamiento/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Financiamiento financiamiento)
        {
            if (id != financiamiento.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Financiamiento>.Filter.Eq(f => f.Id, id);
                _conexion.FinanciamientoCollection.ReplaceOne(filter, financiamiento); 

                return RedirectToAction("Index");
            }

            return View(financiamiento);
        }


        // GET: Financiamiento/Delete/5
        public ActionResult Delete(string id)
        {
            var financiamiento = _conexion.FinanciamientoCollection
                .Find(f => f.Id == id)
                .FirstOrDefault();

            if (financiamiento == null)
            {
                return HttpNotFound();
            }

            return View(financiamiento);
        }

        // POST: Financiamiento/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Autos autos)
        {
            try
            {
                var filter = Builders<Financiamiento>.Filter.Eq(f => f.Id, id);
                _conexion.FinanciamientoCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
