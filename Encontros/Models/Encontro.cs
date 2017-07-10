using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Encontros.Models
{
    public class Encontro
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string NomeEncontro { get; set; }
        
        public DateTime DataDoEncontro { get; set; }

        [LocalNotZero]
        public int LocalId { get; set; }

        public Local Local { get; set; }

        public List<int?> ImageId { get; set; }

        public List<FotoEncontro> FotoEncontro { get; set; }


    }
}