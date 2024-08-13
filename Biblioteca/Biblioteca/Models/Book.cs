using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public string ISBN { get; set; } = string.Empty;

        public DateTime PublishedDate { get; set; }

        // Constructor opcional si necesitas lógica adicional
        public Book()
        {
            // Inicialización en línea se hace en las propiedades
        }
    }
}
