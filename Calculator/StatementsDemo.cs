using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class StatementsDemo
    {
        public static void main(string[] args)
            {
                Node firstMsg = new PrintNode(new NumberNode(1), "newline");
                Node secondMsg = new PrintNode(new NumberNode(2), "newline");

                Node wait = new WaitNode(new NumberNode(2000));

                List<Node> script = new List<Node>();

                script.Add(firstMsg);
                script.Add(wait);
                script.Add(secondMsg);

                foreach (Node statement in script)
                    statement.Eval();
            }
    }
    
}
