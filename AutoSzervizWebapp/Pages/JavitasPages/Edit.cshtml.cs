using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoSzervizWebapp.Models;
using AutoSzervizWebapp.Data;
using AutoSzervizWebapp.DTOs;

namespace AutoSzervizWebapp.Pages.JavitasPages;

public class EditModel : PageModel
{
    private readonly SzervizDbContext _context;

    public EditModel(SzervizDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public JavitasEditDTO Javitas { get; set; } = new();

    public int JavitasId { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var javitas = await _context.Javitasok
            .FirstOrDefaultAsync(j => j.Id == id);

        if (javitas == null)
        {
            return NotFound();
        }

        JavitasId = javitas.Id;

        Javitas = new JavitasEditDTO
        {
            Rendszam = javitas.Rendszam,
            Leiras = javitas.Leiras,
            Koltseg = javitas.Koltseg,
            Allapot = javitas.Allapot,
            FelvetelDatuma = javitas.FelvetelDatuma
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var javitas = await _context.Javitasok
            .FirstOrDefaultAsync(j => j.Id == id);

        if (javitas == null)
        {
            return NotFound();
        }

        JavitasId = javitas.Id;

        if (!ModelState.IsValid)
        {
            Javitas.Allapot = javitas.Allapot;
            return Page();
        }

        javitas.Rendszam = Javitas.Rendszam;
        javitas.Leiras = Javitas.Leiras;
        javitas.Koltseg = Javitas.Koltseg;
        javitas.FelvetelDatuma = Javitas.FelvetelDatuma;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    public async Task<IActionResult> OnPostAllapotValtasAsync(int id)
    {
        var javitas = await _context.Javitasok
            .FirstOrDefaultAsync(j => j.Id == id);

        if (javitas == null)
        {
            return NotFound();
        }

        JavitasId = javitas.Id;

        if (javitas.Allapot == "Felvéve")
        {
            javitas.Allapot = "Folyamatban";
        }
        else if (javitas.Allapot == "Folyamatban")
        {
            if (javitas.Koltseg == null || javitas.Koltseg <= 0)
            {
                TempData["Hiba"] =
                    "A javítás csak akkor állítható Kész állapotba, ha a költség nagyobb mint 0!";
            }
            else
            {
                javitas.Allapot = "Kész";
            }
        }

        await _context.SaveChangesAsync();

        ModelState.Clear();

        Javitas = new JavitasEditDTO
        {
            Rendszam = javitas.Rendszam,
            Leiras = javitas.Leiras,
            Koltseg = javitas.Koltseg,
            Allapot = javitas.Allapot,
            FelvetelDatuma = javitas.FelvetelDatuma
        };

        return Page();
    }
}