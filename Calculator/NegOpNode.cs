﻿using System;
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

        public int ToInt(Node node)
        {
            Object res = node.Eval();
            return (int) res;
        }
        public override Object Eval()
        {
            Object result = (int) -1*(ToInt(node));
            return result;
        }

    }
}
