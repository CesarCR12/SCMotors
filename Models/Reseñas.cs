using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SCMotors.Models
{
    public class Reseñas
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("cliente_id")]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string Cliente_id { get; set; }

        [BsonElement("vehiculo")]
        public string Vehiculo { get; set; }

        [Required]
        [BsonElement("calificacion")]
        public int Calificacion { get; set; }

        [BsonElement("comentario")]
        public string Comentario { get; set; }

    }
}