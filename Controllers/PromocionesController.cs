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
    public class PromocionesController : Controller
    {
        private readonly Conexion _conexion;

        public PromocionesController()
        {
            _conexion = new Conexion();
        }

        // GET: Promociones
        public ActionResult Index()
        {
            return View();
        }

        // GET: Promociones/List
        public ActionResult List()
        {
            var promociones = _conexion.PromocionesCollection.Find(_ => true).ToList();
            return View(promociones);
        }

        // GET: Promociones/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID inválido.");
            }

            try
            {
                var promociones = _conexion.PromocionesCollection.Find(p => p.Id == id).FirstOrDefault(); 

                if (promociones == null)
                {
                    return HttpNotFound("Cliente no encontrado.");
                }

                return View(promociones);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener detalles del cliente (ID: {id}): {ex.Message}";
                return View("Error");
            }
        }


        // GET: Promociones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Promociones/Create
        [HttpPost]
        public ActionResult Create(Promociones promociones)
        {
            if (ModelState.IsValid)
            {
                _conexion.PromocionesCollection.InsertOne(promociones);
                return RedirectToAction("Index");
            }

            return View(promociones);
        }

        // GET: Promociones/Edit/5
        public ActionResult Edit(string id)
        {
            var promociones = _conexion.PromocionesCollection
                .Find(p => p.Id == id)
                .FirstOrDefault();

            if (promociones == null)
            {
                return HttpNotFound();
            }

            return View(promociones);
        }

        // POST: Promociones/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Promociones promociones)
        {
            if (id != promociones.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Promociones>.Filter.Eq(p => p.Id, id);
                _conexion.PromocionesCollection.ReplaceOne(filter, promociones); 

                return RedirectToAction("Index");
            }

            return View(promociones);
        }


        // GET: Promociones/Delete/5
        public ActionResult Delete(string id)
        {
            var promociones = _conexion.PromocionesCollection
                .Find(p => p.Id == id)
                .FirstOrDefault();

            if (promociones == null)
            {
                return HttpNotFound();
            }

            return View(promociones);
        }

        // POST: Promociones/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Promociones promociones)
        {
            try
            {
                var filter = Builders<Promociones>.Filter.Eq(p => p.Id, id);
                _conexion.PromocionesCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}