using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Context.Model;

namespace Telefonia.Domain.DDD
{
    public class DDD : IKey<int>
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
