using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class StringNode : Node
    {
        String text;
        public StringNode() { }

    public StringNode(String text)
    {
        this.text = text;
    }
    public override Object Eval()
    {
        return text;
    }

    }

}
