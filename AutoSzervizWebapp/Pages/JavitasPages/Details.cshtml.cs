using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoSzervizWebapp.Models;
using AutoSzervizWebapp.Data;

namespace AutoSzervizWebapp.Pages.JavitasPages;

public class DetailsModel : PageModel
{
    private readonly SzervizDbContext _context;
    public DetailsModel(SzervizDbContext context)
    {
        _context = context;
    }

    public Javitas Javitas { get; set; } = default!;
    public string ugyfelNev { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var javitas = await _context.Javitasok
            .Where(m => m.Id == id)
            .Select(j => new { Javitas = j, ugyfelNev = j.Ugyfel.Nev })
            .FirstOrDefaultAsync();
        if (javitas is null)

        {
            return NotFound();
        }
        else
        {
            Javitas = javitas.Javitas;
            ugyfelNev = javitas.ugyfelNev;
        }

        return Page();
    }
}
