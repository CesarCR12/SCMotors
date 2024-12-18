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
    public class ReservasController : Controller
    {
        private readonly Conexion _conexion;

        public ReservasController()
        {
            _conexion = new Conexion();
        }

        // GET: Reservas
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reservas/List
        public ActionResult List()
        {
            var reservas = _conexion.ReservasCollection.Find(_ => true).ToList();
            return View(reservas);
        }

        // GET: Reservas/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID inválido.");
            }

            try
            {
                var reservas = _conexion.ReservasCollection.Find(r => r.Id == id).FirstOrDefault(); 

                if (reservas == null)
                {
                    return HttpNotFound("Cliente no encontrado.");
                }

                return View(reservas);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener detalles del cliente (ID: {id}): {ex.Message}";
                return View("Error");
            }
        }


        // GET: Reservas/Create
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



        // POST: Reservas/Create
        [HttpPost]
        public ActionResult Create(Reservas reservas)
        {
            if (ModelState.IsValid)
            {
                reservas.Cliente_id = reservas.Cliente_id ?? ObjectId.Empty.ToString(); 
                _conexion.ReservasCollection.InsertOne(reservas);
                return RedirectToAction("Index");
            }

            var clientes = _conexion.ClientesCollection.Find(_ => true).ToList();
            ViewBag.Clientes = new SelectList(clientes, "_id", "Nombre"); 
            return View(reservas);
        }



        // GET: Reservas/Edit/5
        public ActionResult Edit(string id)
        {
            var reservas = _conexion.ReservasCollection
                .Find(r => r.Id == id)
                .FirstOrDefault();

            if (reservas == null)
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
            return View(reservas);
        }

        // POST: Reservas/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Reservas reservas)
        {
            if (id != reservas.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Reservas>.Filter.Eq(r => r.Id, id);
                _conexion.ReservasCollection.ReplaceOne(filter, reservas);

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
            return View(reservas);
        }


        // GET: Reservas/Delete/5
        public ActionResult Delete(string id)
        {
            var reservas = _conexion.ReservasCollection
                .Find(r => r.Id == id)
                .FirstOrDefault();

            if (reservas == null)
            {
                return HttpNotFound();
            }

            return View(reservas);
        }

        // POST: Reservas/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Reservas reservas)
        {
            try
            {
                var filter = Builders<Reservas>.Filter.Eq(r => r.Id, id);
                _conexion.ReservasCollection.DeleteOne(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
