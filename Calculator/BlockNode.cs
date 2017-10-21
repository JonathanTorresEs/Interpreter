using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class BlockNode : Node
    {
        private List<Node> statements;

        public BlockNode(List<Node> statements)
        {
            this.statements = statements;
        }

        public override Object Eval()
        {
            Object ret = null;
            foreach(Node statement in statements)
            {
                ret = statement.Eval();
            }
            return ret;
        }

        public Node get(int index)
        {
            return statements[index];
        }
        
        protected List<Node> getStatements()
        {
            return statements;
        }

        public String toString()
        {
            String str = "";
            foreach (Node statement in statements)
                str = str + statement + "\n";
            return str;
        }
    }
}
