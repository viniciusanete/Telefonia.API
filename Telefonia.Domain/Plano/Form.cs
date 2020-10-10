using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Context.Model;

namespace Telefonia.Domain.Plano
{
    public class Form : Plano
    {
        public List<int> DDD { get; set; }
    }
}
