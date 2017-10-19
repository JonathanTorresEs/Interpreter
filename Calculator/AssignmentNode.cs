using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class AssignmentNode : Node
    {
        public String name;
        public Node value;
        public Parser parser;
        public String scope;
        public AssignmentNode() { }
        public AssignmentNode(String name, Node value, Parser parser)
        {
            this.name = name;
            this.value = value;
            this.parser = parser;
        }
        public override Object Eval()
        {
            return parser.setVariable(name, value.Eval());
        }
    }
}
