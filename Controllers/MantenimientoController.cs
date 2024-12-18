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
    public class MantenimientoController : Controller
    {
        private readonly Conexion _conexion;

        public MantenimientoController()
        {
            _conexion = new Conexion();
        }

        // GET: Mantenimiento
        public ActionResult Index()
        {
            return View();
        }

        // GET: Mantenimiento/List
        public ActionResult List()
        {
            var mantenimiento = _conexion.MantenimientoCollection.Find(_ => true).ToList();
            return View(mantenimiento);
        }

        // GET: Mantenimiento/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID inválido.");
            }

            try
            {
                var mantenimiento = _conexion.MantenimientoCollection.Find(m => m.Id == id).FirstOrDefault(); 

                if (mantenimiento == null)
                {
                    return HttpNotFound("Cliente no encontrado.");
                }

                return View(mantenimiento);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener detalles del cliente (ID: {id}): {ex.Message}";
                return View("Error");
            }
        }


        // GET: Mantenimiento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mantenimiento/Create
        [HttpPost]
        public ActionResult Create(Mantenimiento mantenimiento)
        {
            if (ModelState.IsValid)
            {
                _conexion.MantenimientoCollection.InsertOne(mantenimiento);
                return RedirectToAction("Index");
            }

            return View(mantenimiento);
        }

        // GET: Mantenimiento/Edit/5
        public ActionResult Edit(string id)
        {
            var mantenimiento = _conexion.MantenimientoCollection
                .Find(m => m.Id == id)
                .FirstOrDefault();

            if (mantenimiento == null)
            {
                return HttpNotFound();
            }

            return View(mantenimiento);
        }

        // POST: Mantenimiento/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Mantenimiento mantenimiento)
        {
            if (id != mantenimiento.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Mantenimiento>.Filter.Eq(m => m.Id, id);
                _conexion.MantenimientoCollection.ReplaceOne(filter, mantenimiento);

                return RedirectToAction("Index");
            }

            return View(mantenimiento);
        }


        // GET: Mantenimiento/Delete/5
        public ActionResult Delete(string id)
        {
            var mantenimiento = _conexion.MantenimientoCollection
                .Find(m => m.Id == id)
                .FirstOrDefault();

            if (mantenimiento == null)
            {
                return HttpNotFound();
            }

            return View(mantenimiento);
        }

        // POST: Mantenimiento/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Mantenimiento mantenimiento)
        {
            try
            {
                var filter = Builders<Mantenimiento>.Filter.Eq(m => m.Id, id);
                _conexion.MantenimientoCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
