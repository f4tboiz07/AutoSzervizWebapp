using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoSzervizWebapp.Models;
using AutoSzervizWebapp.Data;

namespace AutoSzervizWebapp.Pages.UgyfelPages;

public class DeleteModel : PageModel
{
    private readonly SzervizDbContext _context;

    public DeleteModel(SzervizDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Ugyfel Ugyfel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var ugyfel = await _context.Ugyfelek.FirstOrDefaultAsync(m => m.Id == id);
        if (ugyfel is null)
        {
            return NotFound();
        }
        else
        {
            Ugyfel = ugyfel;
        }
        if (Ugyfel.Javitasok != null && Ugyfel.Javitasok.Any())
        {
            ModelState.AddModelError(string.Empty, "A felhasználóhoz tartozó javítások miatt nem törölhető.");
            return Page();
        }

        return Page();
    }


    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var ugyfel = await _context.Ugyfelek.FindAsync(id);
        if (ugyfel != null)
        {
            Ugyfel = ugyfel;
            _context.Ugyfelek.Remove(Ugyfel);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
