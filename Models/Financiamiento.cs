using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SCMotors.Models
{
    public class Financiamiento
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [Required]
        [BsonElement("interés")]
        public double Interés { get; set; }

        [Required]
        [BsonElement("cuotas")]
        public int Cuotas { get; set; }

        [BsonElement("banco")]
        public string Banco { get; set; }

        [BsonElement("monto_minimo")]
        public int Monto_minimo { get; set; }

        [BsonElement("monto_maximo")]
        public int Monto_maximo { get; set; }

        [BsonElement("estado")]
        public String Estado { get; set; }

    }

}