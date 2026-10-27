using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoSzervizWebapp.Models;
using AutoSzervizWebapp.Data;

namespace AutoSzervizWebapp.Pages.JavitasPages;

public class IndexModel : PageModel
{
    private readonly SzervizDbContext _context;

    public IndexModel(SzervizDbContext context)
    {
        _context = context;
    }

    public IList<Javitas> Javitas { get; set; } = default!;


    public async Task OnGetAsync(string? allapot)
    {
        var query = _context.Javitasok
            .Include(j => j.Ugyfel)
            .AsQueryable();

        if (!string.IsNullOrEmpty(allapot))
        {
            query = query.Where(j => j.Allapot == allapot);
        }

        Javitas = await query 
            .OrderBy(j => j.Ugyfel.Nev)
            .ToListAsync();
    }


}
