using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SCMotors.Models
{
        public class Reclamos
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }

            [BsonElement("cliente_id")]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Cliente_id { get; set; }

            [Required]
            [BsonElement("tipo_reclamo")]
            public string Tipo_reclamo { get; set; }

            [BsonElement("detalle")]
            public string Detalle { get; set; }

            [Required]
            [BsonElement("estado")]
            public string Estado { get; set; }

            [Required]
            [BsonElement("respuesta")]
            public string Respuesta { get; set; }

        }
    }