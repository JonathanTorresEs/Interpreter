using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class IfNode : Node
    {
        public Node condition;
        public Node thenPart;
        public Node elsePart;

        public IfNode() { }

        public IfNode(Node condition, Node thenPart, Node elsePart)
        {
            this.condition = condition;
            this.thenPart = thenPart;
            this.elsePart = elsePart;
        }

        public override Object Eval()
        {
            Object ret = null;
            if ((condition != null) && (thenPart != null))
                if ((bool)condition.Eval())
                    ret = thenPart.Eval();
            if ((condition != null) && (elsePart != null))
                if (!(Boolean)condition.Eval())
                    ret = elsePart.Eval();
            return ret;
        }
    }
}
