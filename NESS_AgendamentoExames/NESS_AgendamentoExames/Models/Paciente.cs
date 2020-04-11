using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NESS_AgendamentoExames.Models
{
    public class Paciente
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório!")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório!")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O {0} possui de {2} até {1} caracteres!")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$", ErrorMessage =
            "Números e caracteres especiais não são permitidos!")]
        public string Nome { get; set; }

        public Paciente()
        {
        }

        public Paciente(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
