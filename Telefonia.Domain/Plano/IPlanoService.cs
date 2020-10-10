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
    }
}
