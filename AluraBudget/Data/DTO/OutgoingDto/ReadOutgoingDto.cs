﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AluraBudget.Data.DTO.OutgoingDto
{
    public class ReadOutgoingDto
    {
        
        [Required]
        public string Description { get; set; }

        [Required]
        public Decimal Value { get; set; }

        public DateTime Date { get; set; }
    }
}
