using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class WaitNode : Node
    {
        public Node interval;
        public WaitNode() { }

        public WaitNode(Node interval)
        {
            this.interval = interval;
        }
        public override Object Eval()
        {
            int waitAmount =  (int) interval.Eval();
            try
            {
                System.Threading.Thread.Sleep(waitAmount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in WaitNode.eval() method" + e);
            }
            return waitAmount;
        }

    }
}
