using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Telefonia.Domain.Plano
{
    public interface IPlanoService
    {
        /// <summary>
        /// Cadastro de um novo plano de telefonia
        /// </summary>
        /// <param name="frm">Parametros com os dados completos do plano</param>
        /// <returns>Dados do plano inserido</returns>
        Task<Form> Insert(Form frm);

        /// <summary>
        /// Atualização de um plano de telefonia
        /// </summary>
        /// <param name="frm">Parametros com os dados completos do plano</param>
        /// <returns>Dados do plano inserido</returns>
        Task<Form> Update(Form frm);

        /// <summary>
        /// Método de listagem de planos telefonicos
        /// </summary>
        /// <param name="DDD">DDD do plano, campo obrigatório para a busca</param>
        /// <param name="tipoPlanoId">Tipo do plano, campo opcional</param>
        /// <param name="operadoraId">Operadora do plano, campo opcional</param>
        /// <param name="planoId">Identificador do plano, campo opcional</param>
        /// <returns></returns>
        Task<List<List>> List(int? DDD, int? tipoPlanoId, int? operadoraId, int? planoId);

        /// <summary>
        /// Método para realizar delete lógico
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task Delete(int? Id);
    }
}
