using System;
using System.Collections.Generic;
using System.Text;

namespace Telefonia.Context.Model
{
    public class Key<TKey> : IKey<TKey>
    {
        public TKey Id { get; set; }
    }
}
