using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telefonia.Domain.Plano;
using Telefonia.Domain.Services;
using Telefonia.Infrastructure.Data.Config;
using Telefonia.Infrastructure.Data.Repository;
using Telefonia.Test.Config;
using Data = Telefonia.Context.Context;

namespace Telefonia.Test
{
    public class PlanoTest
    {
        private readonly string databaseName = "telefonia.db";
        private IPlanoService _planoService;
        private Data.Context _context;
        private bool _registerMap = false;
        private readonly DbSettings _dbSettings = new DbSettings();

        [SetUp]
        public void Setup()
        {

            System.IO.File.Copy(@$"..\..\..\{databaseName}", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databaseName), true);

            _dbSettings.ConnectionString = "Data Source=telefonia.db;Cache=Shared";
            _context = new Data.Context(_dbSettings);
            _planoService = new PlanoService(new PlanoRepository(_context),
                                             new DDDRepository(_context),
                                             new PlanoDDDRepository(_context),
                                             new Logger());

            if (!_registerMap)
            {
                RegisterMappings.Register();
                _registerMap = true;
            }
        }

        [Test]
        public async Task Insert()
        {
            var req = new Form();
            req.Minutos = 100;
            req.FranquiaInternet = 250;
            req.Valor = 200.0M;
            req.TipoPlanoId = 1;
            req.OperadoraId = 1;
            req.DDD = new List<int>() { 21, 11 };


            #region Minutos invalidos

            try
            {
                req.Minutos = null;
                await _planoService.Insert(req);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Necessário informar o número de minutos do plano");
            }

            req.Minutos = 100;

            #endregion

            #region FranquiaInternet invalidos

            try
            {
                req.FranquiaInternet = null;
                await _planoService.Insert(req);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Necessário informar o número da franquia de internet do plano");
            }

            req.FranquiaInternet = 320;

            #endregion

            #region Valor invalidos

            try
            {
                req.Valor = null;
                await _planoService.Insert(req);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Necessário informar o valor do plano");
            }

            req.Valor = 400.0M;

            #endregion

            #region TipoPlanoId invalidos

            try
            {
                req.TipoPlanoId = null;
                await _planoService.Insert(req);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Necessário informar o tipo do plano que está sendo cadastrado");
            }

            req.TipoPlanoId = 1;

            #endregion

            #region OperadoraId invalidos

            try
            {
                req.OperadoraId = null;
                await _planoService.Insert(req);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Necessário informar a operadora que esse plano pertence");
            }

            req.OperadoraId = 2;

            #endregion

            #region DDD vazio

            try
            {
                req.DDD = null;
                await _planoService.Insert(req);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Necessário informar a lista de DDD que o plano irá pertencer");
            }


            #endregion

            #region DDD não existe

            try
            {
                req.DDD = new List<int>() { 21, 11, 1 };
                await _planoService.Insert(req);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "DDD 1 inválido. Informar um código de área válido para cadastrar um plano");
            }

            req.DDD = new List<int>() { 21, 11 };

            #endregion

            #region Caso de Sucesso

            try
            {
                var plano = await _planoService.Insert(req);
                Assert.True(plano != null);
            }
            catch (Exception)
            {
                Assert.Fail();
            }


            #endregion
        }

        [Test]
        public async Task Update()
        {
            var req = new Form();
            req.Minutos = 100;
            req.FranquiaInternet = 250;
            req.Valor = 200.0M;
            req.TipoPlanoId = 1;
            req.OperadoraId = 1;
            req.DDD = new List<int>() { 21, 11 };


            #region Cadastrando um plano para atualizar

            try
            {
                req = await _planoService.Insert(req);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }

            req.Minutos = 100;

            #endregion

            #region FranquiaInternet invalidos

            try
            {
                req.FranquiaInternet = null;
                await _planoService.Insert(req);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Necessário informar o número da franquia de internet do plano");
            }

            req.FranquiaInternet = 320;

            #endregion

            #region Valor invalidos

            try
            {
                req.Valor = null;
                await _planoService.Insert(req);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Necessário informar o valor do plano");
            }

            req.Valor = 400.0M;

            #endregion

            #region TipoPlanoId invalidos

            try
            {
                req.TipoPlanoId = null;
                await _planoService.Insert(req);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Necessário informar o tipo do plano que está sendo cadastrado");
            }

            req.TipoPlanoId = 1;

            #endregion

            #region OperadoraId invalidos

            try
            {
                req.OperadoraId = null;
                await _planoService.Insert(req);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Necessário informar a operadora que esse plano pertence");
            }

            req.OperadoraId = 2;

            #endregion

            #region DDD vazio

            try
            {
                req.DDD = null;
                await _planoService.Insert(req);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Necessário informar a lista de DDD que o plano irá pertencer");
            }


            #endregion

            #region DDD não existe

            try
            {
                req.DDD = new List<int>() { 21, 11, 1 };
                await _planoService.Insert(req);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "DDD 1 inválido. Informar um código de área válido para cadastrar um plano");
            }

            req.DDD = new List<int>() { 21, 11 };

            #endregion

            #region Caso de Sucesso

            try
            {
                var plano = _planoService.Update(req);
                Assert.True(plano != null);
            }
            catch (Exception)
            {
                Assert.Fail();
            }


            #endregion
        }

        [Test]
        public async Task Delete()
        {
            var req = new Form();
            req.Minutos = 100;
            req.FranquiaInternet = 250;
            req.Valor = 200.0M;
            req.TipoPlanoId = 1;
            req.OperadoraId = 1;
            req.DDD = new List<int>() { 21, 11 };


            #region Cadastrando um plano para deletare

            try
            {
                req = await _planoService.Insert(req);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }

            #endregion

            #region Plano não existente

            try
            {
                await _planoService.Delete(9999);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Plano não encontrado, verifique o plano que está tentando deletar");
            }

            #endregion

            #region Caso de Sucesso

            try
            {
                await _planoService.Delete(req.Id);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            Assert.Pass();

            #endregion
        }

        [Test]
        public async Task List()
        {
            var req = new Form();
            req.Minutos = 100;
            req.FranquiaInternet = 250;
            req.Valor = 200.0M;
            req.TipoPlanoId = 1;
            req.OperadoraId = 1;
            req.DDD = new List<int>() { 21, 11 };


            #region Cadastrando um plano para listar

            try
            {
                req = await _planoService.Insert(req);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }

            #endregion

            #region Buscando por DDD null

            try
            {
                var itms = await _planoService.List(null, null, null, null);
                Assert.IsNotEmpty(itms);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "DDD é um campo obrigatório para realizar a busca");
            }

            #endregion

            #region Buscando por DDD invalido

            try
            {
                var itms = await _planoService.List(1, null, null, null);
                Assert.IsNotEmpty(itms);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "DDD 1 inválido. Informe um campo de DDD válido");
            }

            #endregion

            #region Buscando por DDD

            try
            {
                var itms = await _planoService.List(21, null, null, null);
                Assert.IsNotEmpty(itms);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Plano não encontrado, verifique o plano que está tentando deletar");
            }

            #endregion

            Assert.Pass();
        }
    }
}
