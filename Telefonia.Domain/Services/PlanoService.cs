using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telefonia.Domain.Plano;

namespace Telefonia.Domain.Services
{
    public class PlanoService : IPlanoService
    {
        private readonly IPlanoRepository _planoRepository;
        private readonly ILogger _logger;
        public PlanoService(IPlanoRepository planoRepository, ILogger logger)
        {
            _planoRepository = planoRepository ?? throw new ArgumentNullException(nameof(planoRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Form> Post(Form frm)
        {
            try
            {
                return frm;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PlanoService - Post");
                throw ex;
            }

        }
    }
}
