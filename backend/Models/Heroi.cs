using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Heroi
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string NomeHeroi { get; set; } = null!;

    public DateTime? DataNascimento { get; set; }

    public float Altura { get; set; }

    public float Peso { get; set; }
}
