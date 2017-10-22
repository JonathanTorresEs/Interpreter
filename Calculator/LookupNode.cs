using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class LookupNode : Node
    {
        private VariableNode varNode;
        private Node keyNode;

        public LookupNode(VariableNode varNode, Node keyNode)
        {
            this.varNode = varNode;
            this.keyNode = keyNode;
        }

        public override Object Eval()
        {
            Object var = varNode.Eval();
            int index = ((int)keyNode.Eval());
            Object ret = ((List<Object>)var)[index];
            return ret;
        }
    }
}
