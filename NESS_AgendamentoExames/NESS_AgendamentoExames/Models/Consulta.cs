using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
