using System.ComponentModel.DataAnnotations;

namespace AutoSzervizWebapp.Models
{
    public class Ugyfel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A név megadása kötelező.")]
        [StringLength(100, ErrorMessage = "A név maximum 100 karakter hosszú lehet.")]
        public string Nev { get; set; }
        [StringLength(20, ErrorMessage = "A telefonszám maximum 20 karakter hosszú lehet.")]
        public string? Telefon { get; set; }
        [EmailAddress(ErrorMessage = "Érvényes e-mail cím megadása kötelező.")]
        public string? Email { get; set; }

        public ICollection<Javitas> Javitasok { get; set; } = new List<Javitas>();

    }
}
