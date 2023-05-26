using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Uretim
{
    public class IsemriBilgi : IEntity
    {
        public string Keys { get; set; }
        public string Value { get; set; }
    }
}
