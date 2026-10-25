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

        Javitas = new JavitasEditDTO
        {
            Rendszam = javitas.Rendszam,
            Leiras = javitas.Leiras,
            Koltseg = javitas.Koltseg
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

        javitas.Rendszam = Javitas.Rendszam;
        javitas.Leiras = Javitas.Leiras;
        javitas.Koltseg = Javitas.Koltseg;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}