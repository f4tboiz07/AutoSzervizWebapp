using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoSzervizWebapp.Models;
using AutoSzervizWebapp.Data;
using AutoSzervizWebapp.DTOs;
using System.ComponentModel.DataAnnotations;

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

    [BindProperty]
    [Range(0,2)]
    public int AllapotIndex { get; set; }


    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var javitas = await _context.Javitasok
            .FirstOrDefaultAsync(j => j.Id == id);

        Javitas = new JavitasEditDTO
        {
            Rendszam = javitas.Rendszam,
            Leiras = javitas.Leiras,
            Koltseg = javitas.Koltseg,
        };

        AllapotIndex = javitas.Allapot switch
        {
            "Felvéve" => 0,
            "Folyamatban" => 1,
            "Kész" => 2,
            _ => 0
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var javitas = await _context.Javitasok
            .FirstOrDefaultAsync(j => j.Id == id);

        if (javitas == null)
        {
            return NotFound();
        }

        var ujAllapot = AllapotIndex switch
        {
            0 => "Felvéve",
            1 => "Folyamatban",
            2 => "Kész",
            _ => null
        };

        if (ujAllapot == null) 
        {
            return BadRequest();
        }

        javitas.Rendszam = Javitas.Rendszam;
        javitas.Leiras = Javitas.Leiras;
        javitas.Koltseg = Javitas.Koltseg;
        javitas.Allapot = ujAllapot;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}