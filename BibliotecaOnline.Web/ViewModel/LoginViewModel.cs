using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BibliotecaOnline.Web.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}