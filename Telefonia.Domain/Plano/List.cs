using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Context.Model;

namespace Telefonia.Domain.Plano
{
    public class List : Plano
    {
        public List<DDD.DDD> DDD { get; set; }
    }
}
