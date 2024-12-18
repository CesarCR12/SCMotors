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
    public class ReseñasController : Controller
    {
        private readonly Conexion _conexion;

        public ReseñasController()
        {
            _conexion = new Conexion();
        }

        // GET: Reseñas
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reseñas/List
        public ActionResult List()
        {
            var reseñas = _conexion.ReseñasCollection.Find(_ => true).ToList();
            return View(reseñas);
        }

        // GET: Reseñas/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID inválido.");
            }

            try
            {
                var reseñas = _conexion.ReseñasCollection.Find(r => r.Id == id).FirstOrDefault();

                if (reseñas == null)
                {
                    return HttpNotFound("Cliente no encontrado.");
                }

                return View(reseñas);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener detalles del cliente (ID: {id}): {ex.Message}";
                return View("Error");
            }
        }


        // GET: Reseñas/Create
        public ActionResult Create()
        {
            var clientes = _conexion.ClientesCollection
                            .Find(_ => true)
                            .Project(c => new SelectListItem
                            {
                                Value = c.Id,
                                Text = c.Nombre
                            })
                            .ToList();

            ViewBag.Clientes = clientes;

            return View();
        }



        // POST: Reseñas/Create
        [HttpPost]
        public ActionResult Create(Reseñas reseñas)
        {
            if (ModelState.IsValid)
            {
                reseñas.Cliente_id = reseñas.Cliente_id ?? ObjectId.Empty.ToString();
                _conexion.ReseñasCollection.InsertOne(reseñas);
                return RedirectToAction("Index");
            }

            var clientes = _conexion.ClientesCollection.Find(_ => true).ToList();
            ViewBag.Clientes = new SelectList(clientes, "_id", "Nombre");
            return View(reseñas);
        }

        // GET: Reseñas/Edit/5
        public ActionResult Edit(string id)
        {
            var reseñas = _conexion.ReseñasCollection
                .Find(r => r.Id == id)
                .FirstOrDefault();

            if (reseñas == null)
            {
                return HttpNotFound();
            }

            // Obtener la lista de clientes
            var clientes = _conexion.ClientesCollection
                .Find(_ => true)
                .Project(c => new SelectListItem
                {
                    Value = c.Id,        
                    Text = c.Nombre      
                })
                .ToList();

            ViewBag.Clientes = clientes; 
            return View(reseñas);
        }

        // POST: Reseñas/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Reseñas reseñas)
        {
            if (id != reseñas.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Reseñas>.Filter.Eq(r => r.Id, id);
                _conexion.ReseñasCollection.ReplaceOne(filter, reseñas);

                return RedirectToAction("Index");
            }

            var clientes = _conexion.ClientesCollection
                .Find(_ => true)
                .Project(c => new SelectListItem
                {
                    Value = c.Id,
                    Text = c.Nombre
                })
                .ToList();

            ViewBag.Clientes = clientes;
            return View(reseñas);
        }



        // GET: Reseñas/Delete/5
        public ActionResult Delete(string id)
        {
            var reseñas = _conexion.ReseñasCollection
                .Find(r => r.Id == id)
                .FirstOrDefault();

            if (reseñas == null)
            {
                return HttpNotFound();
            }

            return View(reseñas);
        }

        // POST: Reseñas/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Reseñas reseñas)
        {
            try
            {
                var filter = Builders<Reseñas>.Filter.Eq(r => r.Id, id);
                _conexion.ReseñasCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
