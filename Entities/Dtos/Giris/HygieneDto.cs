using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Giris
{
    public class HygieneDto:IDto
    {
        public int Query { get; set; }

        public int Clear { get; set; }


        public int? Question { get; set; }

        public string Desc { get; set; }
    }
}
