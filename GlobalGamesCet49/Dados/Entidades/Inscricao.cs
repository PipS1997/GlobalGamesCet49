using System;
using System.ComponentModel.DataAnnotations;

namespace GlobalGamesCet49.Dados.Entidades
{
    public class Inscricao 
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [EmailAddress, Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Localidade")]
        public string Localidade { get; set; }

        public string Telemovel { get; set; }

        [Display(Name = "Cartão de Cidadão")]
        public string CartaoCidadao { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        public DateTime DNasc { get; set; }

        [Display(Name = " ")]
        public string UrlImagem { get; set; }

        public User User { get; set; }

    }

}
    


