using GlobalGamesCet49.Dados.Entidades;
using GlobalGamesCet49.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using GlobalGamesCet49.Helpers;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace GlobalGamesCet49.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper userHelper;

        public HomeController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            this.userHelper = userHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sobre()
        {

            return View();
        }

        public IActionResult Servicos()
        {

            return View();
        }

       [HttpPost]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> Servicos([Bind("Id,Nome,Apelido,Morada,Email,Mensagem")]PedidoContacto pedidoContacto)

        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidoContacto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(pedidoContacto);
        }

        public IActionResult Inscricoes()
        {

            return View();
        }
        // POST: Criação do registo em Inscricoes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inscricoes([Bind("Id,Nome,Email,Localidade,CartaoCidadao,DNasc,FicheiroImagem")] InscricaoViewModel view)
        {
            if (ModelState.IsValid)
            {
                
                var path = string.Empty;

                if (view.FicheiroImagem != null && view.FicheiroImagem.Length > 0)
              {


                    path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot\\images\\Fotos",
                    view.FicheiroImagem.FileName);

                  using (var stream = new FileStream(path, FileMode.Create))
                   {
                      await view.FicheiroImagem.CopyToAsync(stream);
                   }

                    path = $"~/images/Fotos/{view.FicheiroImagem.FileName}";
              }

                var inscricao = this.ToInscricao(view, path);


                _context.Add(inscricao);
                await this._context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        private object ToInscricao(InscricaoViewModel view, string path)
        {
            return new Inscricao
            {
                Id = view.Id,
                UrlImagem = path,
                Nome = view.Nome,
                Email = view.Email,
                Localidade = view.Localidade,
                CartaoCidadao = view.CartaoCidadao,
                DNasc = view.DNasc
            };
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return this.RedirectToAction("Index", "Inscricoes");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to login");
            return this.View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await this.userHelper.LogoutAsync();
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModal model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.Username);
                if (user == null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username
                    };

                    var result = await this.userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, " Ups ainda não tens um Registo..");
                        return this.View(model);
                    }

                    var loginViewModel = new LoginViewModel
                    {
                        Password = model.Password,
                        RememberMe = false,
                        Username = model.Username
                    };



                    var result1 = await this.userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);
                    }

                    var loginViewmodel = new LoginViewModel
                    {
                        Password = model.Password,
                        RememberMe = false,
                        Username = model.Username
                    };

                    var result2 = await this.userHelper.LoginAsync(loginViewModel);

                    if (result2.Succeeded)
                    {
                        return this.RedirectToAction("Index", "Home");
                    }

                    this.ModelState.AddModelError(string.Empty, "Não tem idade suficiente.");
                }

                this.ModelState.AddModelError(string.Empty, "Utilizador já existente.");
            }

            return this.View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    var response = await this.userHelper.UpdateUserAsync(user);
                    if (response.Succeeded)
                    {
                        this.ViewBag.UserMessage = "User Update";
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }

                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Utilizador não encontrado.");
                }
            }

            return this.View(model);
        }

        public IActionResult ChangePassword()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await this.userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Utilizador não encontrado.");
                }
            }

            return this.View(model);
        }
            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
