﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<Form> Insert(Form frm)
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
                    var item = _dDDRepository.GetByDDD(new DDD.DDD() { Codigo = ddd });

                    if (item == null)
                        throw new Exception($"DDD {ddd} inválido. Informar um código de área válido para cadastrar um plano");

                    dddValido.Add(item);
                }

                frm.StatusRegistro = Context.Model.StatusRegister.Active;

                var plano = _planoRepository.Insert(frm);
                frm.Id = plano.Id;

                foreach (var item in dddValido)
                {
                    _planoDDDRepository.Insert(new PlanoDDD.PlanoDDD()
                    {
                        DDDId = item.Id,
                        PlanoId = frm.Id
                    });
                }

                _context.Commit();

                return Task.FromResult(frm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PlanoService - Insert");
                _context.RollBack();
                throw ex;
            }
        }

        public Task<Form> Update(Form frm)
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

                var plano = _planoRepository.Get(frm);

                if (plano == null)
                    throw new Exception("Plano não encontrado, verifique o plano que está tentando atualizar");

                plano.Minutos = frm.Minutos;
                plano.FranquiaInternet = frm.FranquiaInternet;
                plano.Valor = frm.Valor;
                plano.TipoPlanoId = frm.TipoPlanoId;
                plano.OperadoraId = frm.OperadoraId;

                var planosDDD = _planoDDDRepository.ListByPlano(new PlanoDDD.PlanoDDD() { PlanoId = plano.Id }).ToList();

                foreach (var item in planosDDD)
                {
                    _planoDDDRepository.Delete(item);
                }

                var dddValido = new List<DDD.DDD>();

                foreach (var ddd in frm.DDD)
                {
                    var item = _dDDRepository.GetByDDD(new DDD.DDD() { Codigo = ddd });

                    if (item == null)
                        throw new Exception($"DDD {ddd} inválido. Informar um código de área válido para cadastrar um plano");

                    dddValido.Add(item);
                }

                _planoRepository.Update(plano);

                foreach (var item in dddValido)
                {
                    _planoDDDRepository.Insert(new PlanoDDD.PlanoDDD()
                    {
                        DDDId = item.Id,
                        PlanoId = frm.Id
                    });
                }

                _context.Commit();

                return Task.FromResult(frm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PlanoService - Update");
                _context.RollBack();
                throw ex;
            }
        }

        public Task<List<Plano.List>> List(int? DDD, int? tipoPlanoId, int? operadoraId, int? planoId)
        {
            try
            {
                if (DDD == null)
                    throw new Exception("DDD é um campo obrigatório para realizar a busca");

                var dddValido = _dDDRepository.GetByDDD(new DDD.DDD() { Codigo = (int)DDD });

                if (dddValido == null)
                    throw new Exception($"DDD {DDD} inválido. Informe um campo de DDD válido");

                var planos = _planoRepository.List(new Plano.Filter()
                {
                    DDD = dddValido.Id,
                    TipoPlanoId = tipoPlanoId,
                    OperadoraId = operadoraId,
                    PlanoId = planoId
                });

                var result = new List<Plano.List>();

                foreach (var plano in planos)
                {
                    var item = new Plano.List();
                    item.Id = plano.Id;
                    item.Minutos = plano.Minutos;
                    item.FranquiaInternet = plano.FranquiaInternet;
                    item.Valor = plano.Valor;
                    item.TipoPlanoId = plano.TipoPlanoId;
                    item.OperadoraId = plano.OperadoraId;
                    item.StatusRegistro = plano.StatusRegistro;

                    item.DDD = _dDDRepository.ListByPlano(new Domain.DDD.Filter()
                    {
                        PlanoId = plano.Id
                    }).ToList();

                    result.Add(item);
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PlanoService - List");
                throw ex;
            }
        }

        public Task Delete(int? Id)
        {
            try
            {
                if (Id == null)
                    throw new Exception("Identificador obrigatório");

                var plano = _planoRepository.Get(new Plano.Plano() { Id = (int)Id});

                if (plano == null)
                    throw new Exception("Plano não encontrado, verifique o plano que está tentando deletar");

                _planoRepository.Delete(plano);

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PlanoService - Delete");
                throw ex;
            }
        }
    }
}
