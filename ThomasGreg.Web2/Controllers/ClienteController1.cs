using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThomasGreg.Domain.Models;
using ThomasGreg.Web.Data;

namespace ThomasGreg.Web.Controllers
{
    public class ClienteController1 : Controller
    {
        private readonly ThomasGregWebContext _context;

        public ClienteController1(ThomasGregWebContext context)
        {
            _context = context;
        }

        // GET: Cliente
        public async Task<IActionResult> Index()
        {
              return _context.ClienteViewModel != null ? 
                          View(await _context.ClienteViewModel.ToListAsync()) :
                          Problem("Entity set 'ThomasGregWebContext.ClienteViewModel'  is null.");
        }

        // GET: Cliente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClienteViewModel == null)
            {
                return NotFound();
            }

            var clienteViewModel = await _context.ClienteViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteViewModel == null)
            {
                return NotFound();
            }

            return View(clienteViewModel);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Logotipo")] ClienteViewModel clienteViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clienteViewModel);
        }

        // GET: Cliente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClienteViewModel == null)
            {
                return NotFound();
            }

            var clienteViewModel = await _context.ClienteViewModel.FindAsync(id);
            if (clienteViewModel == null)
            {
                return NotFound();
            }
            return View(clienteViewModel);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Logotipo")] ClienteViewModel clienteViewModel)
        {
            if (id != clienteViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteViewModelExists(clienteViewModel.Id))
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
            return View(clienteViewModel);
        }

        // GET: Cliente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClienteViewModel == null)
            {
                return NotFound();
            }

            var clienteViewModel = await _context.ClienteViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteViewModel == null)
            {
                return NotFound();
            }

            return View(clienteViewModel);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClienteViewModel == null)
            {
                return Problem("Entity set 'ThomasGregWebContext.ClienteViewModel'  is null.");
            }
            var clienteViewModel = await _context.ClienteViewModel.FindAsync(id);
            if (clienteViewModel != null)
            {
                _context.ClienteViewModel.Remove(clienteViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteViewModelExists(int id)
        {
          return (_context.ClienteViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
