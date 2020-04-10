using System.ComponentModel.DataAnnotations;

namespace Primetest.Models
{
    public class Accounts
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo � obrigat�rio")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string name { get; set; }

        [Required(ErrorMessage = "Este campo � obrigat�rio")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string email { get; set; }

        [Required(ErrorMessage = "Este campo � obrigat�rio")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo � obrigat�rio")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}