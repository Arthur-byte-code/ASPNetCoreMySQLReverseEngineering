using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebReverseEngineering.Models; // Namespace correto

namespace WebReverseEngineering.Controllers
{
    public class pessoasController : Controller
    {
        private readonly usuariosContext _context;

        public pessoasController(usuariosContext context)
        {
            _context = context;
        }

        // GET: pessoas
        public async Task<IActionResult> Index()
        {
            return View(await _context.pessoas.ToListAsync());
        }

        // GET: pessoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.pessoas
                .FirstOrDefaultAsync(m => m.id_pessoa == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: pessoas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: pessoas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_pessoa,nome_pessoa,idade_pessoa")] pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pessoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        // GET: pessoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        // POST: pessoas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_pessoa,nome_pessoa,idade_pessoa")] pessoa pessoa)
        {
            if (id != pessoa.id_pessoa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!pessoaExists(pessoa.id_pessoa))
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
            return View(pessoa);
        }

        // GET: pessoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.pessoas
                .FirstOrDefaultAsync(m => m.id_pessoa == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoa = await _context.pessoas.FindAsync(id);
            if (pessoa != null)
            {
                _context.pessoas.Remove(pessoa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool pessoaExists(int id)
        {
            return _context.pessoas.Any(e => e.id_pessoa == id);
        }
    }
}
