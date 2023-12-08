using ApiRotinaMvc.Models.Enuns;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiRotinaMvc.Models
{
    public class TreinoViewModel
    {
        [Key]
        [Column("IdTreino")]
        public int IdTreino { get; set; }
        public string Exercicios { get; set; }
        public TreinoEnum TreinoClass { get; set; }
    }
}