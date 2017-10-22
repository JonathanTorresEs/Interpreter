using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class NegOpNode : Node
    {

        public Node node;

        public NegOpNode() { }

        public NegOpNode(Node node)
        {
            this.node = node;
        }

        public double ToDouble(Node node)
        {
            Object res = node.Eval();
            return Double.Parse(res.ToString());
        }
        public override Object Eval()
        {
            Object result = (double) -1*(ToDouble(node));
            return result;
        }

    }
}
