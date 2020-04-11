using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NESS_AgendamentoExames.Models
{
    public class Consulta
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório!")]
        public int Id { get; set; }

        public Paciente Paciente { get; set; }

        public DataDisponivel DataDisponivel { get; set; }

    }
}
