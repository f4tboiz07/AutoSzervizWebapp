using AutoSzervizWebapp.Models;
using System.ComponentModel.DataAnnotations;

namespace AutoSzervizWebapp.DTOs
{
    public class JavitasCreateDTO
    {
        [Required(ErrorMessage = "Az ügyfél megadása kötelező.")]
        public int UgyfelId { get; set; }
        [Required(ErrorMessage = "A rendszám megadása kötelező.")]
        [StringLength(10, ErrorMessage = "A rendszám maximum 10 karakter hosszú lehet.")]
        [RegularExpression(@"^[A-Z]{3}-[0-9]{3}$",ErrorMessage = "A rendszám formátuma például ABC-123 legyen.")]

        public string Rendszam { get; set; }
        [StringLength(300, ErrorMessage = "A leírás maximum 300 karakter hosszú lehet.")]
        public string? Leiras { get; set; }
        [Range(typeof(decimal), "0", "9999999999", ErrorMessage = "A költség 0 és 9999999999 közötti lehet.")]
        public decimal Koltseg { get; set; }
        public string Allapot { get; set; } = "Felvéve";
        public DateTime FelvetelDatuma { get; set; } = DateTime.Now;
    }
}
