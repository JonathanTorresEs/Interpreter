using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class PrintNode : Node
    {
        public Node expression;
        public String type;

        public PrintNode() { }

        public PrintNode(Node expression, String type)
        {
            this.expression = expression;
            this.type = type;
        }

        public override Object Eval()
        {
            Object writee = expression.Eval();

        if (type.Equals("sameline"))
                Console.WriteLine(writee);
        else 
            if (type.Equals("newline"))
                Console.WriteLine(writee);
            return writee;
        }
    }
}
