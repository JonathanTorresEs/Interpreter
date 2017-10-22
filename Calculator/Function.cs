using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Function : Node
    {

        private Node body;
        private List<Parameter> parameters;
        private String name;

        public Function(String name, Node body, List<Parameter> parameters)
        {
            this.body = body;
            this.parameters = parameters;
            this.name = name;
        }
        public override Object Eval()
        {
            return body.Eval();
        }

        public List<Parameter> getParameters()
        {
            return parameters;
        }

        public Node getBody()
        {
            return body;
        }

        public String getName()
        {
            return name;
        }
}
}
