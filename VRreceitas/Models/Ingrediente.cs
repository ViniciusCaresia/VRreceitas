using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VRreceitas.Models;

[Table("Ingrediente")]
public class Ingrediente
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Nome { get; set; }

    public List<ReceitaIngrediente> Receitas { get; set; }
}
