using System;
using System.Collections.Generic;

namespace backend.Models;

public class HeroisSuperpoderes
{
    public int HeroiId { get; set; }
    public int SuperpoderId { get; set; }

    public Heroi Heroi { get; set; }
    public Superpoderes Superpoder { get; set; }
}
