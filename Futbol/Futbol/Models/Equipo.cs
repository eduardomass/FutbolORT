﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Futbol.Models
{
    //[Table("EQUIPO")]
    public class Equipo
    {
        [Key]
        public int Id { get; set; }

        

        //[ForeignKey(nameof(Clase))]
    }
}
