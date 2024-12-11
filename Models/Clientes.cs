using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SCMotors.Models
{
        public class Clientes
        {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [Required]
        [BsonElement("identificacion")]
        public int Identificacion { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [Required]
        [BsonElement("telefono")]
        public int Telefono { get; set; }

        [Required]
        [BsonElement("direccion")]
        public Direccion direccion { get; set; }

        public class Direccion
        {
            [Required]
            [BsonElement("provincia")]
            public string Provincia { get; set; }

            [Required]
            [BsonElement("canton")]
            public string Canton { get; set; }
        }
    }

 }
