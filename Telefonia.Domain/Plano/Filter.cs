using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Context.Model;

namespace Telefonia.Domain.Plano
{
    public class Filter
    {
        public int? PlanoId { get; set; }
        public int? DDD { get; set; }
        public int? OperadoraId { get; set; }
        public int? TipoPlanoId { get; set; }

    }
}
