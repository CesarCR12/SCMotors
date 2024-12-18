using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SCMotors.Models;

namespace SCMotors.Controllers
{
    public class VentasController : Controller
    {
        private readonly Conexion _conexion;

        public VentasController()
        {
            _conexion = new Conexion();
        }

        // GET: Ventas
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ventas/List
        public ActionResult List()
        {
            var ventas = _conexion.VentasCollection.Find(_ => true).ToList();
            return View(ventas);
        }

        // GET: Ventas/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID inválido.");
            }

            try
            {
                var ventas = _conexion.VentasCollection.Find(v => v.Id == id).FirstOrDefault();

                if (ventas == null)
                {
                    return HttpNotFound("Auto no encontrado.");
                }

                return View(ventas);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener detalles del cliente (ID: {id}): {ex.Message}";
                return View("Error");
            }
        }


        // GET: Ventas/Create
        public ActionResult Create()
        {
            var autos = _conexion.AutosCollection
                            .Find(_ => true)
                            .Project(c => new SelectListItem
                            {
                                Value = c.Id,
                                Text = c.Modelo
                            })
                            .ToList();

            ViewBag.Autos = autos;

            return View();
        }



        // POST: Ventas/Create
        [HttpPost]
        public ActionResult Create(Ventas ventas)
        {
            if (ModelState.IsValid)
            {
                ventas.Vehiculo_id = ventas.Vehiculo_id ?? ObjectId.Empty.ToString();
                _conexion.VentasCollection.InsertOne(ventas);
                return RedirectToAction("Index");
            }

            var autos = _conexion.AutosCollection.Find(_ => true).ToList();
            ViewBag.Autos = new SelectList(autos, "_id", "Modelo");
            return View(ventas);
        }



        // GET: Ventas/Edit/5
        public ActionResult Edit(string id)
        {
            var Ventas = _conexion.VentasCollection
                .Find(v => v.Id == id)
                .FirstOrDefault();

            if (Ventas == null)
            {
                return HttpNotFound();
            }

            var autos = _conexion.AutosCollection
                .Find(_ => true)
                .Project(c => new SelectListItem
                {
                    Value = c.Id,
                    Text = c.Modelo
                })
                .ToList();

            ViewBag.Autos = autos;
            return View(Ventas);
        }

        // POST: Ventas/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Ventas ventas)
        {
            if (id != ventas.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Ventas>.Filter.Eq(r => r.Id, id);
                _conexion.VentasCollection.ReplaceOne(filter, ventas);

                return RedirectToAction("Index");
            }

            var autos = _conexion.AutosCollection
                .Find(_ => true)
                .Project(c => new SelectListItem
                {
                    Value = c.Id,
                    Text = c.Modelo
                })
                .ToList();

            ViewBag.Autos = autos;
            return View(ventas);
        }


        // GET: Ventas/Delete/5
        public ActionResult Delete(string id)
        {
            var ventas = _conexion.VentasCollection
                .Find(v => v.Id == id)
                .FirstOrDefault();

            if (ventas == null)
            {
                return HttpNotFound();
            }

            return View(ventas);
        }

        // POST: Ventas/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Ventas ventas)
        {
            try
            {
                var filter = Builders<Ventas>.Filter.Eq(v => v.Id, id);
                _conexion.VentasCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}