using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class ForNode : Node
    {

    public Node variable;
    public Node condition;
    public Node action;
    public Node body;

    public ForNode() { }

    public ForNode(Node variable, Node condition, Node action, Node body)
    {
        this.variable = variable;
        this.condition = condition;
        this.action = action;
        this.body = body;
    }

    public override Object Eval()
    {
        Object res = null;
        variable.Eval();
        while ((bool)condition.Eval())
        {
            res = body.Eval();
            action.Eval();
        }
        return res;
    }
}
}
