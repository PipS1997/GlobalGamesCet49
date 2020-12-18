using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GlobalGamesCet49.Dados.Entidades;


namespace GlobalGamesCet49.Controllers
{
    public class PedidoContactoController : Controller
    {
        private readonly DataContext _context;

        public PedidoContactoController(DataContext context)
        {
            _context = context;
        }

        // GET: PedidoContacto
        public async Task<IActionResult> Index()
        {
            return View(await _context.PedidoContactos.ToListAsync());
        }

        // GET: PedidoContacto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoContacto = await _context.PedidoContactos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedidoContacto == null)
            {
                return NotFound();
            }

            return View(pedidoContacto);
        }

        // GET: PedidoContacto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PedidoContacto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Apelido,Morada,Email,Mensagem")] PedidoContacto pedidoContacto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidoContacto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pedidoContacto);
        }

        // GET: PedidoContacto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoContacto = await _context.PedidoContactos.FindAsync(id);
            if (pedidoContacto == null)
            {
                return NotFound();
            }
            return View(pedidoContacto);
        }

        // POST: PedidoContacto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Apelido,Morada,Email,Mensagem")] PedidoContacto pedidoContacto)
        {
            if (id != pedidoContacto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidoContacto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoContactoExists(pedidoContacto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pedidoContacto);
        }

        // GET: PedidoContacto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoContacto = await _context.PedidoContactos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedidoContacto == null)
            {
                return NotFound();
            }

            return View(pedidoContacto);
        }

        // POST: PedidoContacto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedidoContacto = await _context.PedidoContactos.FindAsync(id);
            _context.PedidoContactos.Remove(pedidoContacto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoContactoExists(int id)
        {
            return _context.PedidoContactos.Any(e => e.Id == id);
        }
    }
}
