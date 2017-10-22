using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Parameter
    {
        private String name;
        private Node value;

        public Parameter(Node value)
        {
            this.value = value;
        }

        public Parameter(String name, Node value)
        {
        this.name = name;
        this.value = value;
        }

        public Parameter(String name)
        {
            this.name = name;
        }

        public Object Eval()
        {
            return value.Eval();
        }

        public String getName()
        {
            return name;
        }

        public Object getValue()
        {
            return value.Eval();
        }
    }
}
