using jatekGyar.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jatekGyar.Entities
{
    public class PresentFactory: IToyFactory
    {
        public Toy CreateNew()
        {
            return new Present();
        }
    }
}
