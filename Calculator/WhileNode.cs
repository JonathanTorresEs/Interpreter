using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class WhileNode : Node
    {
    public Node condition;
    public Node body;
    public WhileNode() { }
    public WhileNode(Node condition, Node body)
    {
        this.condition = condition;
        this.body = body;
    }
    public override Object Eval()
    {
        Object ret = null;
        while ( (bool) condition.Eval())
        {
            ret = body.Eval();
        }
        return ret;
    }
}
}
