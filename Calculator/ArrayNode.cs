using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class ArrayNode : Node
    {
        private List<Node> elements;

        public ArrayNode(List<Node> elements)
        {
            this.elements = elements;
        }

        public override Object Eval()
        {
            List<Object> items = new List<Object>(elements.Count);
            foreach (Node node in elements)
            {
                items.Add(node.Eval());
            }
            return items;
        }
    }
}
