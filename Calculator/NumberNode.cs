using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class NumberNode : Node
    {
        int value;
    public NumberNode() { }

    public NumberNode(Int32 value)
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
