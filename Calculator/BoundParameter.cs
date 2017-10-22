using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class BoundParameter
    {
        private String name;
        private Object value;

        public BoundParameter(String name, Object value)
        {
            this.name = name;
            this.value = value;
        }

        public String getName()
        {
            return name;
        }

        public Object getValue()
        {
            return value;
        }
    }
}
