using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class HeroisSuperpoderes
{
    public int? HeroiId { get; set; }

    public int? SuperpoderId { get; set; }

    public virtual Heroi? Heroi { get; set; }

    public virtual Superpoderes? Superpoder { get; set; }
}
