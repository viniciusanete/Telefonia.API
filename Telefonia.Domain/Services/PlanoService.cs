using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Domain.Plano;

namespace Telefonia.Domain.Services
{
    public class PlanoService : IPlanoService
    {
        private readonly IPlanoRepository _planoRepository;
        public PlanoService(IPlanoRepository planoRepository)
        {
            _planoRepository = planoRepository ?? throw new ArgumentNullException(nameof(planoRepository));
        }
    }
}
