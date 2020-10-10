using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Context.Model;

namespace Telefonia.Domain.Plano
{
    public class Plano : IKey<int>, IActive
    {
        public int Id { get; set; }
        public int Minutos { get; set; }
        public int FranquiaInternet { get; set; }
        public decimal Valor { get; set; }
        public int TipoPlanoId { get; set; }
        public int OperadoraId { get; set; }
        public StatusRegister StatusRegistro { get; set; }
    }
}
