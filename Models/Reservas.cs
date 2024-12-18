using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SCMotors.Models
{
        public class Reservas
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }

            [BsonElement("cliente_id")]
            [BsonRepresentation(BsonType.ObjectId)] 
            public string Cliente_id { get; set; }

            [BsonElement("modelo_auto")]
            public string Modelo_auto { get; set; }

            [Required]
            [BsonElement("detalles_reserva")]
            public string Detalles_reserva { get; set; }

            [BsonElement("estado")]
            public string Estado { get; set; }

    }
 }