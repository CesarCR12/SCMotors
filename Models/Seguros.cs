using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SCMotors.Models
{
    public class Seguros
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [Required]
        [BsonElement("cobertura")]
        public string Cobertura { get; set; }

        [Required]
        [BsonElement("costo_anual")]
        public int Costo_anual { get; set; }

        [Required]
        [BsonElement("compañía")]
        public string Compañía { get; set; }

        [Required]
        [BsonElement("vigencia_meses")]
        public int Vigencia_meses { get; set; }

        [Required]
        [BsonElement("estado")]
        public string Estado { get; set; }


    }
}