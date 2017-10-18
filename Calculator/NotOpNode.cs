using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class NotOpNode : Node
    {
        public Node node;
        public NotOpNode() { }
        public NotOpNode(Node node)
        {
            this.node = node;
        }
        public bool ToBoolean(Node node)
        {
            Object res = node.Eval();
            return (bool) res;
        }
        public override Object Eval()
        {
            Object result = (bool) (!ToBoolean(node));
            return result;
        }

    }
}
