using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Superpoderes
{
    public int Id { get; set; }

    public string Superpoder { get; set; } = null!;

    public string? Descricao { get; set; }
}
