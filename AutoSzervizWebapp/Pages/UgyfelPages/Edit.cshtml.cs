using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoSzervizWebapp.Models;
using AutoSzervizWebapp.Data;

namespace AutoSzervizWebapp.Pages.UgyfelPages;

public class EditModel : PageModel
{
    private readonly SzervizDbContext _context;

    public EditModel(SzervizDbContext context)
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
        Ugyfel = ugyfel;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var ugyfel = await _context.Ugyfelek.FindAsync(Ugyfel.Id);

        if (ugyfel is null)
        {
            return NotFound();
        }

        ugyfel.Nev = Ugyfel.Nev;
        ugyfel.Telefon = Ugyfel.Telefon;
        ugyfel.Email = Ugyfel.Email;

        _context.Attach(ugyfel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UgyfelExists(Ugyfel.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool UgyfelExists(int id)
    {
        return _context.Ugyfelek.Any(e => e.Id == id);
    }
}
