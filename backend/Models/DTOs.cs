
namespace backend.Models;

public class HeroiDTO
{

    public string Nome { get; set; } = null!;

    public string NomeHeroi { get; set; } = null!;

    public DateTime? DataNascimento { get; set; }

    public float Altura { get; set; }

    public float Peso { get; set; }
    public List<string> Poderes { get; set; } = new List<string>();
}