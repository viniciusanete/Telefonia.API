using System;
using System.Collections.Generic;
using System.Text;

namespace Telefonia.Context.Model
{
    public interface IKey<TKey>
    {
        TKey Id { get; set; }
    }
}
