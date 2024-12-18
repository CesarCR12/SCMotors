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
    public class AutosController : Controller
    {
        private readonly Conexion _conexion;

        public AutosController()
        {
            _conexion = new Conexion();
        }

        // GET: Autos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Autos/List
        public ActionResult List()
        {
            var autos = _conexion.AutosCollection.Find(_ => true).ToList();
            return View(autos);
        }

        // GET: Autos/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID inválido.");
            }

            try
            {
                var autos = _conexion.AutosCollection.Find(a => a.Id == id).FirstOrDefault(); 

                if (autos == null)
                {
                    return HttpNotFound("Cliente no encontrado.");
                }

                return View(autos);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener detalles del cliente (ID: {id}): {ex.Message}";
                return View("Error");
            }
        }


        // GET: Autos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autos/Create
        [HttpPost]
        public ActionResult Create(Autos autos)
        {
            if (ModelState.IsValid)
            {
                _conexion.AutosCollection.InsertOne(autos);
                return RedirectToAction("Index");
            }

            return View(autos);
        }

        // GET: Autos/Edit/5
        public ActionResult Edit(string id)
        {
            var autos = _conexion.AutosCollection
                .Find(a => a.Id == id)
                .FirstOrDefault();

            if (autos == null)
            {
                return HttpNotFound();
            }

            return View(autos);
        }

        // POST: Autos/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Autos autos)
        {
            if (id != autos.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Autos>.Filter.Eq(a => a.Id, id);
                _conexion.AutosCollection.ReplaceOne(filter, autos); 

                return RedirectToAction("Index");
            }

            return View(autos);
        }


        // GET: Autos/Delete/5
        public ActionResult Delete(string id)
        {
            var autos = _conexion.AutosCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (autos == null)
            {
                return HttpNotFound();
            }

            return View(autos);
        }

        // POST: Autos/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Autos autos)
        {
            try
            {
                var filter = Builders<Autos>.Filter.Eq(a => a.Id, id);
                _conexion.AutosCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
