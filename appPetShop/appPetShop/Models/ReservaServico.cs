using System;
using System.Collections.Generic;

namespace appPetShop.Models;

public partial class ReservaServico
{
    public int Codigo { get; set; }

    public DateTime DataHora { get; set; }

    public decimal Valor { get; set; }

    public string StatusServico { get; set; } = null!;

    public int ClienteId { get; set; }

    public int VeterinarioId { get; set; }

    public virtual Cliente? Cliente { get; set; } = null!;

    public virtual Veterinario? Veterinario { get; set; } = null!;
}
