using GlobalGamesCet49.Dados.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalGamesCet49.Dados
{
    public class SeedDB
    {
        private readonly DataContext context;
        private readonly Inscricao inscricao;

        private Random random;


        public SeedDB(DataContext context)
        {
            this.context = context;


            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            if (!this.context.Inscricoes.Any())
            {
                this.AddInscricao("Filipe","Afonso");
                this.AddInscricao("Raquel","Filipa");
                this.AddInscricao("Edir","Amorim");
                this.AddInscricao("Talita","Borges");
                this.AddInscricao("Eduardo","Santos");
                this.AddInscricao("Rodrigo","Vieira");
                this.AddInscricao("Pedro","Macedo");
                this.AddInscricao("Diogo","Silva");
                await this.context.SaveChangesAsync();
            }
        }

        private void AddInscricao(string nome, string apelido)
        {
            this.context.Inscricoes.Add(new Inscricao
            {
                Nome = nome,
                Apelido = apelido,
                Morada = "",
                Telemovel = "",
                CartaoCidadao = ""
                
            });
        }
    }
}
