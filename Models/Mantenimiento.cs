using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SCMotors.Models
{
    public class Mantenimiento
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

        [BsonElement("costo")]
        public int Costo { get; set; }

        [Required]
        [BsonElement("duración_días")]
        public int Duración_días { get; set; }

    }
}