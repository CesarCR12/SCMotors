using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SCMotors.Models;

namespace SCMotors.Controllers
{
    public class ClientesController : Controller
    {
        private readonly Conexion _conexion;

        public ClientesController()
        {
            _conexion = new Conexion();
        }

        // GET: Clientes
        public ActionResult Index()
        {
            return View();
        }

        // GET: Clientes/List
        public ActionResult List()
        {
            var clientes = _conexion.ClientesCollection.Find(_ => true).ToList();
            return View(clientes);
        }

        // GET: Clientes/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID inválido.");
            }

            try
            {
                var clientes = _conexion.ClientesCollection.Find(c => c.Id == id).FirstOrDefault(); // Buscar grupo por ID

                if (clientes == null)
                {
                    return HttpNotFound("Cliente no encontrado.");
                }

                return View(clientes);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener detalles del cliente (ID: {id}): {ex.Message}";
                return View("Error");
            }
        }


        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        public ActionResult Create(Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                _conexion.ClientesCollection.InsertOne(clientes);
                return RedirectToAction("Index");
            }

            return View(clientes);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(string id)
        {
            var clientes = _conexion.ClientesCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (clientes == null)
            {
                return HttpNotFound();
            }

            return View(clientes);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Clientes clientes)
        {
            if (id != clientes.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Clientes>.Filter.Eq(e => e.Id, id);
                _conexion.ClientesCollection.ReplaceOne(filter, clientes); // Actualiza el documento

                return RedirectToAction("Index");
            }

            return View(clientes);
        }


        // GET: Clientes/Delete/5
        public ActionResult Delete(string id)
        {
            var clientes = _conexion.ClientesCollection
                .Find(e => e.Id == id)
                .FirstOrDefault();

            if (clientes == null)
            {
                return HttpNotFound();
            }

            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Clientes clientes)
        {
            try
            {
                var filter = Builders<Clientes>.Filter.Eq(e => e.Id, id);
                _conexion.ClientesCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
