using System;
using System.Collections.Generic;

namespace appPetShop.Models;

public partial class Veterinario
{
    public int Codigo { get; set; }

    public string Nome { get; set; } = null!;

    public string Crmv { get; set; } = null!;

    public string Especialidade { get; set; } = null!;

    public virtual ICollection<ReservaServico> ReservaServicos { get; set; } = new List<ReservaServico>();
}
