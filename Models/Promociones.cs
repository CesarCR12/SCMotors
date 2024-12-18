using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SCMotors.Models
{
    public class Promociones
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [Required]
        [BsonElement("descripción")]
        public string Descripción { get; set; }

        [Required]
        [BsonElement("tipo")]
        public string Tipo { get; set; }

        [Required]
        [BsonElement("porcentaje_descuento")]
        public int Porcentaje_descuento { get; set; }

        [Required]
        [BsonElement("estado")]
        public string Estado { get; set; }

    }
}