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
    public class SegurosController : Controller
    {
        private readonly Conexion _conexion;

        public SegurosController()
        {
            _conexion = new Conexion();
        }

        // GET: Seguros
        public ActionResult Index()
        {
            return View();
        }

        // GET: Seguros/List
        public ActionResult List()
        {
            var seguros = _conexion.SegurosCollection.Find(_ => true).ToList();
            return View(seguros);
        }

        // GET: Seguros/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID inválido.");
            }

            try
            {
                var seguros = _conexion.SegurosCollection.Find(s => s.Id == id).FirstOrDefault(); 

                if (seguros == null)
                {
                    return HttpNotFound("Cliente no encontrado.");
                }

                return View(seguros);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener detalles del cliente (ID: {id}): {ex.Message}";
                return View("Error");
            }
        }


        // GET: Seguros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Seguros/Create
        [HttpPost]
        public ActionResult Create(Seguros seguros)
        {
            if (ModelState.IsValid)
            {
                _conexion.SegurosCollection.InsertOne(seguros);
                return RedirectToAction("Index");
            }

            return View(seguros);
        }

        // GET: Seguros/Edit/5
        public ActionResult Edit(string id)
        {
            var seguros = _conexion.SegurosCollection
                .Find(s => s.Id == id)
                .FirstOrDefault();

            if (seguros == null)
            {
                return HttpNotFound();
            }

            return View(seguros);
        }

        // POST: Seguros/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Seguros seguros)
        {
            if (id != seguros.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Seguros>.Filter.Eq(s => s.Id, id);
                _conexion.SegurosCollection.ReplaceOne(filter, seguros); 

                return RedirectToAction("Index");
            }

            return View(seguros);
        }


        // GET: Seguros/Delete/5
        public ActionResult Delete(string id)
        {
            var seguros = _conexion.SegurosCollection
                .Find(s => s.Id == id)
                .FirstOrDefault();

            if (seguros == null)
            {
                return HttpNotFound();
            }

            return View(seguros);
        }

        // POST: Seguros/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Seguros seguros)
        {
            try
            {
                var filter = Builders<Seguros>.Filter.Eq(s => s.Id, id);
                _conexion.SegurosCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
