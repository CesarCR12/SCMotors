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
    public class EmpleadosController : Controller
    {
        private readonly Conexion _conexion;

        public EmpleadosController()
        {
            _conexion = new Conexion();
        }

        // GET: Empleados
        public ActionResult Index()
        {
            return View();
        }

        // GET: Empleados/List
        public ActionResult List()
        {
            var empleados = _conexion.EmpleadosCollection.Find(_ => true).ToList();
            return View(empleados);
        }

        // GET: Empleados/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID inválido.");
            }

            try
            {
                var empleados = _conexion.EmpleadosCollection.Find(e => e.Id == id).FirstOrDefault(); 

                if (empleados == null)
                {
                    return HttpNotFound("Cliente no encontrado.");
                }

                return View(empleados);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener detalles del cliente (ID: {id}): {ex.Message}";
                return View("Error");
            }
        }


        // GET: Empleados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _conexion.EmpleadosCollection.InsertOne(empleados);
                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"Error al crear la compra: {ex.Message}";
                }
            }

            return View(empleados);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(string id)
        {
            var empleados = _conexion.EmpleadosCollection
                .Find(a => a.Id == id)
                .FirstOrDefault();

            if (empleados == null)
            {
                return HttpNotFound();
            }

            return View(empleados);
        }

        // POST: Empleados/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Empleados empleados)
        {
            if (id != empleados.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Empleados>.Filter.Eq(e => e.Id, id);
                _conexion.EmpleadosCollection.ReplaceOne(filter, empleados); 

                return RedirectToAction("Index");
            }

            return View(empleados);
        }


        // GET: Empleados/Delete/5
        public ActionResult Delete(string id)
        {
            var empleados = _conexion.EmpleadosCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (empleados == null)
            {
                return HttpNotFound();
            }

            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Autos autos)
        {
            try
            {
                var filter = Builders<Empleados>.Filter.Eq(e => e.Id, id);
                _conexion.EmpleadosCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
