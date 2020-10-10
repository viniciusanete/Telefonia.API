using System;
using System.Collections.Generic;
using System.Text;

namespace Telefonia.Domain.DDD
{
    public interface IDDDRepository : Telefonia.Context.Common.IRegular<DDD, int>
    {
        /// <summary>
        /// Buscar DDD pelo código
        /// </summary>
        /// <param name="frm">Código do DDD</param>
        /// <returns>Objeto completo de DDD</returns>
        DDD GetByDDD(DDD frm);
    }
}
