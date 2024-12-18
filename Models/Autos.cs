using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SCMotors.Models
{
        public class Autos
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }

            [Required]
            [BsonElement("marca")]
            public string Marca { get; set; }

            [Required]
            [BsonElement("modelo")]
            public string Modelo { get; set; }

            [BsonElement("año")]
            public int Año { get; set; }

            [Required]
            [BsonElement("precio")]
            public int Precio { get; set; }

            [Required]
            [BsonElement("color")]
            public string Color { get; set; }

            [Required]
            [BsonElement("kilometraje")]
            public int Kilometraje { get; set; }

            [Required]
            [BsonElement("estado")]
            public string Estado { get; set; }


    }
}