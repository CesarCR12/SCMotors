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

        public System.Data.Entity.DbSet<SCMotors.Models.Clientes> Clientes { get; set; }
    }
}
