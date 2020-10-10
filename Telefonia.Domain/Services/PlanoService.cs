using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telefonia.Domain.DDD;
using Telefonia.Domain.Plano;
using Telefonia.Domain.PlanoDDD;

namespace Telefonia.Domain.Services
{
    public class PlanoService : IPlanoService
    {
        private readonly IPlanoRepository _planoRepository;
        private readonly IDDDRepository _dDDRepository;
        private readonly IPlanoDDDRepository _planoDDDRepository;
        private Telefonia.Context.Context.IContext _context;
        private readonly ILogger _logger;
        public PlanoService(IPlanoRepository planoRepository, IDDDRepository dDDRepository, IPlanoDDDRepository planoDDDRepository, ILogger logger)
        {
            _planoRepository = planoRepository ?? throw new ArgumentNullException(nameof(planoRepository));
            _dDDRepository = dDDRepository ?? throw new ArgumentNullException(nameof(dDDRepository));
            _planoDDDRepository = planoDDDRepository ?? throw new ArgumentNullException(nameof(planoDDDRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _context = _planoRepository.Context = _dDDRepository.Context = _planoDDDRepository.Context;
        }

        public async Task<Form> Insert(Form frm)
        {
            try
            {
                _context.BeginTransaction();


                if (frm.Minutos == null)
                    throw new Exception("Necessário informar o número de minutos do plano");

                if (frm.FranquiaInternet == null)
                    throw new Exception("Necessário informar o número da franquia de internet do plano");

                if (frm.Valor == null)
                    throw new Exception("Necessário informar o valor do plano");

                if (frm.TipoPlanoId == null)
                    throw new Exception("Necessário informar o tipo do plano que está sendo cadastrado");

                if (frm.OperadoraId == null)
                    throw new Exception("Necessário informar a operadora que esse plano pertence");

                if (frm.DDD == null || frm.DDD.Count == 0)
                    throw new Exception("Necessário informar a lista de DDD que o plano irá pertencer");


                var dddValido = new List<DDD.DDD>();

                foreach (var ddd in frm.DDD)
                {
                    var item = _dDDRepository.GetByDDD(new DDD.DDD() { Codigo = ddd});

                    if (item == null)
                        throw new Exception($"DDD {ddd} inválido. Informar um código de área válido para cadastrar um plano");

                    dddValido.Add(item);
                }

                frm.StatusRegistro = Context.Model.StatusRegister.Active;

                var plano = _planoRepository.Insert(frm);

                foreach (var item in dddValido)
                {
                    _planoDDDRepository.Insert(new PlanoDDD.PlanoDDD()
                    {
                        DDDId = item.Id,
                        PlanoId = plano.Id
                    });
                }

                _context.Commit();

                return frm;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PlanoService - Post");
                _context.RollBack();
                throw ex;
            }

        }
    }
}
