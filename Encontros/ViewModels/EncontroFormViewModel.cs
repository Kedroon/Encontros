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
        [Display(Name = "Encontro")]
        public string NomeEncontro { get; set; }

        [Display(Name = "Data do Encontro")]
        [Required]
        public DateTime? DataDoEncontro { get; set; }
        
        [Required]
        public int? LocalId { get; set; }

        public string LocalNome { get; set; }

        public string Title { get { return Id != 0 ? "Editar Encontro" : "Adicionar Encontro"; } }
    }
}
