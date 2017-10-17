using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class BooleanNode : Node
    {
    bool value;

    public BooleanNode() { }

    public BooleanNode(Boolean value)
    {
        this.value = value;
    }
    public override Object Eval()
    {
        return value;
    }
    public String toString()
    {
        return value + "";
    }
}
}
