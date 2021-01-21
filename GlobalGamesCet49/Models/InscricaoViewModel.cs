namespace GlobalGamesCet49.Models
{

    using GlobalGamesCet49.Dados.Entidades;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;


    public class InscricaoViewModel : Inscricao
    {
        [Display(Name = "Fotografia")]
        public IFormFile FicheiroImagem { get; set; }
    }
}
