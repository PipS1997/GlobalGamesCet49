using GlobalGamesCet49.Dados.Entidades;
using GlobalGamesCet49.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalGamesCet49.Dados
{
    public class SeedDB
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
  


        public SeedDB(DataContext context, IUserHelper userHelper)
        {

            this.context = context;
            this.userHelper = userHelper;
           

        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.userHelper.GetUserByEmailAsync("admin@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Filipe",
                    LastName = "Afonso",
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456789");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Não foi possível criar o usuário no semeador");
                }
            }

            if (!this.context.Inscricoes.Any())
            {
                this.AddSubscribe("Filipe Afonso", user);
                this.AddSubscribe("Raquel Filipa", user);
                this.AddSubscribe("Edir Amorim", user);
                this.AddSubscribe("Sofia Correia", user);
                this.AddSubscribe("Carolina Abreu", user);
                this.AddSubscribe("Rodrigo Vieira",  user);
                this.AddSubscribe("Pedro Santos",  user);
                this.AddSubscribe("Diogo Macedo", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddSubscribe(string name, User user)
        {
            this.context.Inscricoes.Add(new Inscricao
            {

                Nome = name,
                User = user,
            });

        }
    }
}