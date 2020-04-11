using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NESS_AgendamentoExames.Models
{
    public class DataDisponivel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório!")]
        public int Id { get; set; }

        [Display(Name = "Data")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório!")]
        [DataType(DataType.Date, ErrorMessage = "Formato inválido!")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", NullDisplayText = "dd/mm/aaaa", ConvertEmptyStringToNull = true)]
        public DateTime Data { get; set; }

        public DataDisponivel()
        {
        }

        public DataDisponivel(int id, DateTime data)
        {
            Id = id;
            Data = data;
        }
    }
}
