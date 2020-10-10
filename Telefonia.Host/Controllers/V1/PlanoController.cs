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

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> Insert([FromBody]Form form)
        {
            return Ok(await _planoService.Insert(form));
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] Form form)
        {
            return Ok(await _planoService.Update(form));
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List([FromQuery]int? DDD, [FromQuery] int? tipoPlanoId, [FromQuery] int? operadoraId, [FromQuery] int? planoId)
        {
            return Ok(await _planoService.List(DDD, tipoPlanoId, operadoraId, planoId));
        }
    }
}
