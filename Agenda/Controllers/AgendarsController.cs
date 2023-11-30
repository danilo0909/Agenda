using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agenda.Data;
using Agenda.Models;

namespace Agenda.Controllers
{
    public class AgendarsController : Controller
    {
        private readonly DBContext _context;

        public AgendarsController(DBContext context)
        {
            _context = context;
        }

        // GET: Agendars
        public async Task<IActionResult> Index()
        {
              return _context.Agendar != null ? 
                          View(await _context.Agendar.ToListAsync()) :
                          Problem("Entity set 'DBContext.Agendar'  is null.");
        }

        // GET: Agendars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Agendar == null)
            {
                return NotFound();
            }

            var agendar = await _context.Agendar
                .FirstOrDefaultAsync(m => m.IdAgendamento == id);
            if (agendar == null)
            {
                return NotFound();
            }

            return View(agendar);
        }

        // GET: Agendars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agendars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAgendamento,Nome,Endereco,Data_Agendamento,Hora")] Agendar agendar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agendar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agendar);
        }

        // GET: Agendars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Agendar == null)
            {
                return NotFound();
            }

            var agendar = await _context.Agendar.FindAsync(id);
            if (agendar == null)
            {
                return NotFound();
            }
            return View(agendar);
        }

        // POST: Agendars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAgendamento,Nome,Endereco,Data_Agendamento,Hora")] Agendar agendar)
        {
            if (id != agendar.IdAgendamento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendarExists(agendar.IdAgendamento))
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
            return View(agendar);
        }

        // GET: Agendars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Agendar == null)
            {
                return NotFound();
            }

            var agendar = await _context.Agendar
                .FirstOrDefaultAsync(m => m.IdAgendamento == id);
            if (agendar == null)
            {
                return NotFound();
            }

            return View(agendar);
        }

        // POST: Agendars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Agendar == null)
            {
                return Problem("Entity set 'DBContext.Agendar'  is null.");
            }
            var agendar = await _context.Agendar.FindAsync(id);
            if (agendar != null)
            {
                _context.Agendar.Remove(agendar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendarExists(int id)
        {
          return (_context.Agendar?.Any(e => e.IdAgendamento == id)).GetValueOrDefault();
        }
    }
}
