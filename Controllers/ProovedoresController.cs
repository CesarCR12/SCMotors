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
    public class ProovedoresController : Controller
    {
        private readonly Conexion _conexion;

        public ProovedoresController()
        {
            _conexion = new Conexion();
        }

        // GET: Proovedores
        public ActionResult Index()
        {
            return View();
        }

        // GET: Proovedores/List
        public ActionResult List()
        {
            var proovedores = _conexion.ProovedoresCollection.Find(_ => true).ToList();
            return View(proovedores);
        }

        // GET: Proovedores/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID inválido.");
            }

            try
            {
                var proovedores = _conexion.ProovedoresCollection.Find(p => p.Id == id).FirstOrDefault(); 

                if (proovedores == null)
                {
                    return HttpNotFound("Cliente no encontrado.");
                }

                return View(proovedores);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener detalles del cliente (ID: {id}): {ex.Message}";
                return View("Error");
            }
        }


        // GET: Proovedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proovedores/Create
        [HttpPost]
        public ActionResult Create(Proovedores proovedores)
        {
            if (ModelState.IsValid)
            {
                _conexion.ProovedoresCollection.InsertOne(proovedores);
                return RedirectToAction("Index");
            }

            return View(proovedores);
        }

        // GET: Proovedores/Edit/5
        public ActionResult Edit(string id)
        {
            var proovedores = _conexion.ProovedoresCollection
                .Find(p => p.Id == id)
                .FirstOrDefault();

            if (proovedores == null)
            {
                return HttpNotFound();
            }

            return View(proovedores);
        }

        // POST: Proovedores/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Proovedores proovedores)
        {
            if (id != proovedores.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Proovedores>.Filter.Eq(p => p.Id, id);
                _conexion.ProovedoresCollection.ReplaceOne(filter, proovedores); 

                return RedirectToAction("Index");
            }

            return View(proovedores);
        }


        // GET: Proovedores/Delete/5
        public ActionResult Delete(string id)
        {
            var proovedores = _conexion.ProovedoresCollection
                .Find(p => p.Id == id)
                .FirstOrDefault();

            if (proovedores == null)
            {
                return HttpNotFound();
            }

            return View(proovedores);
        }

        // POST: Proovedores/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Proovedores proovedores)
        {
            try
            {
                var filter = Builders<Proovedores>.Filter.Eq(p => p.Id, id);
                _conexion.ProovedoresCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
