﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactList.Data;
using ContactList.Models;
using Microsoft.AspNetCore.Authorization;

namespace ContactList.Controllers
{
    public class ContatosController : Controller
    {
        private readonly ContactListContext _context;

        public ContatosController(ContactListContext context)
        {
            _context = context;
        }

        // GET: Contatos
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            ViewBag.Categorias = _context.Categorias.Where(c => c.UsuarioNome == User.Identity.Name).ToList();

            var contactListContext = _context.Contato.Include(c => c.Categoria)
                                                        .Where(c => c.UsuarioNome == User.Identity.Name)
                                                        .OrderBy(o => o.Nome);

            return View(await contactListContext.ToListAsync());
        }

        // GET: Contatos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return NotFound();
            }

            var contato = await _context.Contato
                .Include(c => c.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contato == null)
            {
                return NotFound();
            }

            if (contato.UsuarioNome != User.Identity.Name) return NotFound();

            return View(contato);
        }

        // GET: Contatos/Create
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            ViewData["Categoria"] = new SelectList(_context.Categorias.Where(c => c.UsuarioNome == User.Identity.Name), "Id", "CategoriaNome");
            return View();
        }

        // POST: Contatos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Telefone,CategoriaId")] Contato contato)
        {
            if (ModelState.IsValid)
            {
                contato.UsuarioNome = User.Identity.Name;
                _context.Add(contato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias.Where(c => c.UsuarioNome == User.Identity.Name), "Id", "Id", contato.CategoriaId);
            return View(contato);
        }

        // GET: Contatos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return NotFound();
            }

            var contato = await _context.Contato.FindAsync(id);
            if (contato == null)
            {
                return NotFound();
            }
            ViewData["Categoria"] = new SelectList(_context.Categorias.Where(c => c.UsuarioNome == User.Identity.Name), "Id", "CategoriaNome", contato.CategoriaId);

            if (contato.UsuarioNome != User.Identity.Name) return NotFound();
            return View(contato);
        }

        // POST: Contatos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Telefone,CategoriaId,UsuarioNome")] Contato contato)
        {
            if (id != contato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContatoExists(contato.Id))
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
            ViewData["Categoria"] = new SelectList(_context.Categorias.Where(c => c.UsuarioNome == User.Identity.Name), "Id", "CategoriaNome", contato.CategoriaId);
            return View(contato);
        }

        // GET: Contatos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return NotFound();
            }

            var contato = await _context.Contato
                .Include(c => c.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contato == null)
            {
                return NotFound();
            }

            if (contato.UsuarioNome != User.Identity.Name) return NotFound();

            return View(contato);
        }

        // POST: Contatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contato = await _context.Contato.FindAsync(id);
            _context.Contato.Remove(contato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> BuscaCategoria(int idCategoria)
        {
            ViewBag.Categorias = _context.Categorias.Where(c => c.UsuarioNome == User.Identity.Name).ToList();
            var categoriaAltual = _context.Categorias.FirstOrDefault(c => c.Id == idCategoria);
            ViewData["title"] = categoriaAltual.CategoriaNome;
            var result = await _context.Contato.Include(c => c.Categoria).Where(c => c.CategoriaId == idCategoria).
                                                Where(c => c.UsuarioNome == User.Identity.Name)
                                               .OrderBy(o => o.Nome).ToListAsync();

            return View(result);
        }

        private bool ContatoExists(int id)
        {
            return _context.Contato.Any(e => e.Id == id);
        }


    }
}
