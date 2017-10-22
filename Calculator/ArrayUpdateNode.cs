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

        public double ToDouble(Node node)
        {
            Object res = node.Eval();
            return Double.Parse(res.ToString());
        }

        public override Object Eval()
        {
            Object arrayVariable = array.Eval();
            double index = ToDouble(indexExpression);
            int intIndex = Int32.Parse(index.ToString());
            Object newValue = rightSideExpression.Eval();
            ((List<Object>)arrayVariable)[intIndex] = newValue;
            Object ret = arrayVariable;
            return ret;
    }
}
}
