using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoSzervizWebapp.Models;
using AutoSzervizWebapp.Data;

namespace AutoSzervizWebapp.Pages.UgyfelPages;

public class IndexModel : PageModel
{
    private readonly SzervizDbContext _context;

    public IndexModel(SzervizDbContext context)
    {
        _context = context;
    }

    public IList<Ugyfel> Ugyfel { get; set; } = new List<Ugyfel>();

    public async Task OnGetAsync()
    {
        Ugyfel = await _context.Ugyfelek
            .Include(u => u.Javitasok)
            .OrderBy(u => u.Nev)
            .ToListAsync();
    }
}
