using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Context.Model;

namespace Telefonia.Domain.TipoPlano
{
    public class TipoPlano : IKey<int>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
