using Encontros.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Encontros.ViewModels
{
    public class EncontroFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Descrição do encontro")]
        public string Descricao { get; set; }

        [Display(Name = "Data do Encontro")]
        [Required]
        public DateTime? DataDoEncontro { get; set; }
        
        [Required]
        public int? LocalId { get; set; }
    }
}