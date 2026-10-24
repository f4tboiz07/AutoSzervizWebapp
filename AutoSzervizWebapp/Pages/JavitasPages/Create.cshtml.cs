using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoSzervizWebapp.Models;
using AutoSzervizWebapp.Data;
using AutoSzervizWebapp.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSzervizWebapp.Pages.JavitasPages;

public class CreateModel : PageModel
{
    private readonly SzervizDbContext _context;

    public CreateModel(SzervizDbContext context)
    {
        _context = context;
    }


    [BindProperty]
    public JavitasCreateDTO Javitas { get; set; } = new();
    public SelectList UgyfelSelectList { get; set; } 

    private async Task UgyfelBetoltesAsync() 
    {
        var ugyfelek = await _context.Ugyfelek.AsNoTracking().OrderBy(u => u.Nev).ToListAsync();

        //Console.WriteLine($"Ügyfelek száma: {ugyfelek.Count}");


        UgyfelSelectList = new SelectList(
            ugyfelek,
            nameof(Ugyfel.Id),
            nameof(Ugyfel.Nev),
            Javitas.UgyfelId
            );
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.

    public async Task OnGetAsync() 
    {
        await UgyfelBetoltesAsync();
    }



    public async Task<IActionResult> OnPostAsync()
    {


        if (!ModelState.IsValid)
        {
            await UgyfelBetoltesAsync();
            return Page();
            
        }
        var ujJavitas = new Javitas
        {
            UgyfelId = Javitas.UgyfelId,
            Rendszam = Javitas.Rendszam,
            Leiras = Javitas.Leiras,
            Koltseg = Javitas.Koltseg,
            Allapot = "Felvéve",
            FelvetelDatuma = Javitas.FelvetelDatuma
        };
        _context.Javitasok.Add(ujJavitas);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
