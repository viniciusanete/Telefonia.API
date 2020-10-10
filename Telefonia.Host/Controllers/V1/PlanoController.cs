using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telefonia.Domain.Plano;

namespace Telefonia.Host.Controllers.V1
{
    [Route("v1/plano")]
    public class PlanoController : ControllerBase
    {
        private readonly IPlanoService _planoService;

        public PlanoController(IPlanoService planoService)
        {
            _planoService = planoService ?? throw new ArgumentNullException(nameof(planoService));
        }
    }
}
