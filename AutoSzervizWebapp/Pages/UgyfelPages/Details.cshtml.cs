using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoSzervizWebapp.Models;
using AutoSzervizWebapp.Data;

namespace AutoSzervizWebapp.Pages.UgyfelPages;

public class DetailsModel : PageModel
{
    private readonly SzervizDbContext _context;
    public DetailsModel(SzervizDbContext context)
    {
        _context = context;
    }

    public Ugyfel Ugyfel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var ugyfel = await _context.Ugyfelek.Include(u => u.Javitasok).FirstOrDefaultAsync(m => m.Id == id);
        if (ugyfel is null)
        {
            return NotFound();
        }
        else
        {
            Ugyfel = ugyfel;
        }

        return Page();
    }
}
