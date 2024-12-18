using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SCMotors.Models
{
    public class Empleados
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [Required]
        [BsonElement("puesto")]
        public string Puesto { get; set; }

        [BsonElement("telefono")]
        public int Telefono { get; set; }

        [Required]
        [BsonElement("email")]
        public string Email { get; set; }

        [Required]
        [BsonElement("detallesPuesto")]
        public DetallesPuesto detallesPuesto { get; set; }

        public class DetallesPuesto
        {
            [Required]
            [BsonElement("departamento")]
            public string Departamento { get; set; }

            [Required]
            [BsonElement("responsabilidad")]
            public string Responsabilidad { get; set; }
        }
    }

}