using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SCMotors.Models
{
    public class Ventas
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("empleado")]
        public string Empleado { get; set; }

        [BsonElement("cliente")]
        public string Cliente { get; set; }

        [BsonElement("vehiculo_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Vehiculo_id { get; set; }

        [Required]
        [BsonElement("total")]
        public int Total { get; set; }

    }
}