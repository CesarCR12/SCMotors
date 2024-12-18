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
    public class ReclamosController : Controller
    {
        private readonly Conexion _conexion;

        public ReclamosController()
        {
            _conexion = new Conexion();
        }

        // GET: Reclamos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reclamos/List
        public ActionResult List()
        {
            var reclamos = _conexion.ReclamosCollection.Find(_ => true).ToList();
            return View(reclamos);
        }

        // GET: Reclamos/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID inválido.");
            }

            try
            {
                var reclamos = _conexion.ReclamosCollection.Find(r => r.Id == id).FirstOrDefault();

                if (reclamos == null)
                {
                    return HttpNotFound("Cliente no encontrado.");
                }

                return View(reclamos);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener detalles del cliente (ID: {id}): {ex.Message}";
                return View("Error");
            }
        }


        // GET: Reclamos/Create
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



        // POST: Reclamos/Create
        [HttpPost]
        public ActionResult Create(Reclamos reclamos)
        {
            if (ModelState.IsValid)
            {
                reclamos.Cliente_id = reclamos.Cliente_id ?? ObjectId.Empty.ToString();
                _conexion.ReclamosCollection.InsertOne(reclamos);
                return RedirectToAction("Index");
            }

            var clientes = _conexion.ClientesCollection.Find(_ => true).ToList();
            ViewBag.Clientes = new SelectList(clientes, "_id", "Nombre");
            return View(reclamos);
        }



        // GET: Reclamos/Edit/5
        public ActionResult Edit(string id)
        {
            var reclamos = _conexion.ReclamosCollection
                .Find(r => r.Id == id)
                .FirstOrDefault();

            if (reclamos == null)
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
            return View(reclamos);
        }

        // POST: Reclamos/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Reclamos reclamos)
        {
            if (id != reclamos.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Reclamos>.Filter.Eq(r => r.Id, id);
                _conexion.ReclamosCollection.ReplaceOne(filter, reclamos);

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
            return View(reclamos);
        }


        // GET: Reclamos/Delete/5
        public ActionResult Delete(string id)
        {
            var reclamos = _conexion.ReclamosCollection
                .Find(r => r.Id == id)
                .FirstOrDefault();

            if (reclamos == null)
            {
                return HttpNotFound();
            }

            return View(reclamos);
        }

        // POST: Reclamos/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Reclamos reclamos)
        {
            try
            {
                var filter = Builders<Reclamos>.Filter.Eq(r => r.Id, id);
                _conexion.ReclamosCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
