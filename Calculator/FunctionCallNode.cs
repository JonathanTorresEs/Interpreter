using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class FunctionCallNode : Node
    {
        public Node name;
        public List<Parameter> actualParameters;
        public Parser parser;

        public FunctionCallNode() { }
        public FunctionCallNode(Node name, List<Parameter> actualParameters, Parser parser)
        {
            this.name = name;
            this.actualParameters = actualParameters;
            this.parser = parser;
        }

        public override Object Eval()
        {
            // Get the function object from symbol table
            // with this name by evaluating variable node

            Function function = (Function)name.Eval();
            List<BoundParameter> boundParameters = new List<BoundParameter>();
            if (function.getParameters() != null)
            {
                // Some functions do not have parameters, check it out
                if (actualParameters != null)
                {
                    if (actualParameters.Count() < function.getParameters().Count())
                    {
                        Console.WriteLine("Too Few Parameters in Function Call: "
                        + function.getName());
                        Environment.Exit(0);
                    }
                    else if (actualParameters.Count > function.getParameters().Count)
                    {
                        Console.WriteLine("Too Many Parameters in Function Call: " + function.getName());
                        Environment.Exit(0);
                    }
                    else
                    {
                        // Bind actual parameters
                        for (int index = 0; index < actualParameters.Count; index++)
                        {
                            String name = function.getParameters()[index].getName();
                            Object value = actualParameters[index].getValue();
                            //If the parameter is a function!
                            if (value is Function)
                            {
                                value = ((Function)value).Eval();
                            }
                            boundParameters.Add(new BoundParameter(name, value));
                        }
                    }
                }
            }
            //Now, call this function
            return parser.ExecuteFunction(function, boundParameters);
        }
    }

}

