using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoSzervizWebapp.Models;
using AutoSzervizWebapp.Data;

namespace AutoSzervizWebapp.Pages.JavitasPages;

public class EditModel : PageModel
{
    private readonly SzervizDbContext _context;

    public EditModel(SzervizDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Javitas Javitas { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var javitas = await _context.Javitasok.FirstOrDefaultAsync(m => m.Id == id);
        if (javitas is null)
        {
            return NotFound();
        }
        Javitas = javitas;
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

        _context.Attach(Javitas).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!JavitasExists(Javitas.Id))
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

    private bool JavitasExists(int id)
    {
        return _context.Javitasok.Any(e => e.Id == id);
    }
}
