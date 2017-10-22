using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class ArrayUpdateNode : Node
    {
        private Node array;
        private Node indexExpression;
        private Node rightSideExpression;
        public ArrayUpdateNode(Node array, Node indexExpression, Node rightSideExpression)
        {
            this.array = array;
            this.indexExpression = indexExpression;
            this.rightSideExpression = rightSideExpression;
        }

        public int ToInt(Node node)
        {
            Object res = node.Eval();
            return ((int)res);
        }

        public override Object Eval()
        {
            Object arrayVariable = array.Eval();
            int index = ToInt(indexExpression);
            Object newValue = rightSideExpression.Eval();
            ((List<Object>)arrayVariable)[index] = newValue;
            Object ret = arrayVariable;
            return ret;
    }
}
}
