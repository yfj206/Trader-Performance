using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trader;
using Trader.Models;

namespace Trader.Controllers;

public class TradersController : Controller
{
    private readonly ApplicationDbContext _context;

    public TradersController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Traders.ToListAsync());
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var traders = await _context.Traders
            .FirstOrDefaultAsync(m => m.Id == id);
        if (traders == null)
        {
            return NotFound();
        }

        return View(traders);
    }

    public IActionResult Create()
    {
        return View();
    }
        
    [HttpPost]
    public async Task<IActionResult> Create([Bind("Id,Name,Email")] Traders traders)
    {
        if (ModelState.IsValid)
        {
            traders.Id = Guid.NewGuid();
            _context.Add(traders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(traders);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var traders = await _context.Traders.FindAsync(id);
        if (traders == null)
        {
            return NotFound();
        }
        return View(traders);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Email")] Traders traders)
    {
        if (id != traders.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(traders);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TradersExists(traders.Id))
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
        return View(traders);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var traders = await _context.Traders
            .FirstOrDefaultAsync(m => m.Id == id);
        if (traders == null)
        {
            return NotFound();
        }

        return View(traders);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var traders = await _context.Traders.FindAsync(id);
        if (traders != null)
        {
            _context.Traders.Remove(traders);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TradersExists(Guid id)
    {
        return _context.Traders.Any(e => e.Id == id);
    }
}
