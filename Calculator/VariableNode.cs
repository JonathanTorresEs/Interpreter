using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator 
{
    class VariableNode : Node
    {
        public String varName;
        public Parser parser;

        public VariableNode() { }

        public VariableNode(String varName, Parser parser)
        {
            this.varName = varName;
            this.parser = parser;
        }
        public override Object Eval()
        {
            Object varValue = parser.getVariable(varName);
            if (varValue == null)
            {
                Console.WriteLine("Undefined Variable...Var Name: " + varName);
                Environment.Exit(1);
            }
            return varValue;

        }
    }

}
