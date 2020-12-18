using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalGamesCet49.Dados.Entidades
{
    public class Inscricao
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Apelido { get; set; }

        public string Morada { get; set; }

        public string Telemovel{ get; set; }

        [Display(Name = "Cartão de Cidadão")]
        public string CartaoCidadao { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DNasc { get; set; }

    }
}