using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoSzervizWebapp.Models;
using AutoSzervizWebapp.Data;

namespace AutoSzervizWebapp.Pages.UgyfelPages;

public class CreateModel : PageModel
{
    private readonly SzervizDbContext _context;

    public CreateModel(SzervizDbContext context)
    {
        _context = context;
    }
    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Ugyfel Ugyfel { get; set; } = new();

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var ujUgyfel = new Ugyfel
        {
            Nev = Ugyfel.Nev,
            Telefon = Ugyfel.Telefon,
            Email = Ugyfel.Email
        };

        _context.Ugyfelek.Add(ujUgyfel);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
