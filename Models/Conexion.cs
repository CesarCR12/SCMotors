using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SCMotors.Models
{
    public class Conexion
    {
        private readonly IMongoDatabase _database;
        public Conexion()
        {
            // Especificacion del puerto destinado al ambiente de produccion 
            var client = new MongoClient("mongodb://localhost:27025");

            // Especificar la coleccion generada 
            _database = client.GetDatabase("SCMotors");
        }

        public IMongoCollection<Clientes> ClientesCollection
        {
            get
            {
                return _database.GetCollection<Clientes>("Clientes");
            }
        }
        public IMongoCollection<Autos> AutosCollection
        {
            get
            {
                return _database.GetCollection<Autos>("Autos");
            }
        }

        public IMongoCollection<Reservas> ReservasCollection
        {
            get
            {
                return _database.GetCollection<Reservas>("Reservas");
            }
        }

        public IMongoCollection<Empleados> EmpleadosCollection
        {
            get
            {
                return _database.GetCollection<Empleados>("Empleados");
            }
        }

        public IMongoCollection<Financiamiento> FinanciamientoCollection
        {
            get
            {
                return _database.GetCollection<Financiamiento>("Financiamiento");
            }
        }

        public IMongoCollection<Mantenimiento> MantenimientoCollection
        {
            get
            {
                return _database.GetCollection<Mantenimiento>("Mantenimiento");
            }
        }

        public IMongoCollection<Reseñas> ReseñasCollection
        {
            get
            {
                return _database.GetCollection<Reseñas>("Reseñas");
            }
        }

        public IMongoCollection<Promociones> PromocionesCollection
        {
            get
            {
                return _database.GetCollection<Promociones>("Promociones");
            }
        }

        public IMongoCollection<Proovedores> ProovedoresCollection
        {
            get
            {
                return _database.GetCollection<Proovedores>("Proovedores");
            }
        }

        public IMongoCollection<Seguros> SegurosCollection
        {
            get
            {
                return _database.GetCollection<Seguros>("Seguros");
            }
        }

        public IMongoCollection<Reclamos> ReclamosCollection
        {
            get
            {
                return _database.GetCollection<Reclamos>("Reclamos");
            }
        }

        public IMongoCollection<Ventas> VentasCollection
        {
            get
            {
                return _database.GetCollection<Ventas>("Ventas");
            }
        }

        public System.Data.Entity.DbSet<SCMotors.Models.Clientes> Clientes { get; set; }

        public System.Data.Entity.DbSet<SCMotors.Models.Clientes> Autos { get; set; }

        public System.Data.Entity.DbSet<SCMotors.Models.Clientes> Reservas { get; set; }

        public System.Data.Entity.DbSet<SCMotors.Models.Clientes> Empleados { get; set; }

        public System.Data.Entity.DbSet<SCMotors.Models.Clientes> Financiamiento { get; set; }

        public System.Data.Entity.DbSet<SCMotors.Models.Clientes> Mantenimiento { get; set; }

        public System.Data.Entity.DbSet<SCMotors.Models.Clientes> Reseñas { get; set; }

        public System.Data.Entity.DbSet<SCMotors.Models.Clientes> Promociones { get; set; }

        public System.Data.Entity.DbSet<SCMotors.Models.Clientes> Proovedores { get; set; }

        public System.Data.Entity.DbSet<SCMotors.Models.Clientes> Seguros { get; set; }

        public System.Data.Entity.DbSet<SCMotors.Models.Clientes> Reclamos { get; set; }

        public System.Data.Entity.DbSet<SCMotors.Models.Clientes> Ventas { get; set; }
    }
}
