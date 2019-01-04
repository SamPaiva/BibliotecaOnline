using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BibliotecaOnline.Modelo.ViewModels
{
    public class LivroViewModel
    {
        public long LivroId { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        public string Sinopse { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        public string Autor { get; set; }

        [Display(Name = "Gênero")]
        public string Genero { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name ="Imagem")]
        public HttpPostedFileBase ImageUpload { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name ="Arquivo PDF")]
        public HttpPostedFileBase Pdf { get; set; }

        [Display(Name = "Data de Lançamento")]
        public DateTime DataLancamento { get; set; }
    }
}