using System;
using System.Collections.Generic;

namespace appPetShop.Models;

public partial class Cliente
{
    public int Codigo { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public DateOnly DataNascimento { get; set; }

    public virtual ICollection<ReservaServico> ReservaServicos { get; set; } = new List<ReservaServico>();
}
