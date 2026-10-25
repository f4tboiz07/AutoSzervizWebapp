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

    public async Task OnGetAsync()
    {
        Javitas = await _context.Javitasok
            .Include(j => j.Ugyfel)
            .OrderBy(j => j.Ugyfel.Nev)
            .ToListAsync();
    }


}
