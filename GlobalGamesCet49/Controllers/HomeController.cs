using GlobalGamesCet49.Dados.Entidades;
using GlobalGamesCet49.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;

namespace GlobalGamesCet49.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
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
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Inscricoes()
        {

            return View();
        }
        // POST: Criação do registo em Inscricoes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inscricoes([Bind("Id,Nome,Apelido,Morada,Telemovel,CartaoCidadao,DNasc,FicheiroImagem")] InscricaoViewModel view)
        {
            if (ModelState.IsValid)
            {
                ViewBag.TheResult = true;
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
                Apelido = view.Apelido,
                Morada = view.Morada,
                CartaoCidadao = view.CartaoCidadao,
                DNasc = view.DNasc
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
