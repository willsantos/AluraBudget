using AluraBudget.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace AluraBudget.Data.DTO.OutgoingDto
{
    public class ReadOutgoingDto
    {
        
        [Required]
        public string Description { get; set; }

        [Required]
        public Decimal Value { get; set; }

        [Required]
        public OutgoingCategory Category { get; set; }

        public DateTime Date { get; set; }
    }
}
