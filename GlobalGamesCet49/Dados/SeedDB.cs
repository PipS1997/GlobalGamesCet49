using GlobalGamesCet49.Dados.Entidades;
using GlobalGamesCet49.Helpers;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUserHelper userHelper;
        private Random random;


        public SeedDB(DataContext context, IUserHelper userHelper)
        {

            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();

        }

        public Inscricao Inscricao => inscricao;

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.userHelper.GetUserByEmailAsync("filipeafonso@gmail.com");
            if(user == null)
            {
                user = new User
                {
                    FirstName = "Filipe",
                    LastName = "Afonso",
                    Email = "filipeafonso@gmail.com",
                    UserName = "filipeafonso@gmail.com"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456789");
                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Não foi possível criar o usuário no semeador");
                }
            }

            if (!this.context.Inscricoes.Any())
            {
                this.AddSubscriber("Filipe",user);
                this.AddSubscriber("Raquel",user);
                this.AddSubscriber("Edir",user);
                this.AddSubscriber("Talita",user);
                this.AddSubscriber("Eduardo",user);
                this.AddSubscriber("Rodrigo",user);
                this.AddSubscriber("Pedro",user);
                this.AddSubscriber("Diogo",user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddSubscriber(string name, User user)
        {
            this.context.Inscricoes.Add(new Inscricao
            {
                Nome = name,
                Apelido = "",
                Morada = "",
                Telemovel = "",
                CartaoCidadao = "",
               
                
            });
        }
    }
}
