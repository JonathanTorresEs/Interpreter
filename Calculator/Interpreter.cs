using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Interpreter
    {
        public static void Main()
        {
            Console.WriteLine("Ejecutando Interpreter");

            string[] args = { "script.vpp" };
            bool debug = true;

            if (args.Length < 1) {
                Console.WriteLine("Usage: Demo <script>"); return;
            }

            if (args.Length > 1) {
                if (args[1].Equals("debug"))
                {
                    debug = true;
                }
                    
            }

            Interpreter interpreter = new Interpreter();
            String sourceCode = System.IO.File.ReadAllText(args[0])+" ";
            Console.WriteLine(sourceCode + "\n");
            interpreter.Interpret(sourceCode, debug);
        }

        public void Interpret(String source, bool debug)
        {
            Tokenizer tokenizer = new Tokenizer();
            Parser parser = new Parser(tokenizer.Tokenize(source));

            parser.setVariable("pi", 3.14159265358979);
            parser.setVariable("euler", 2.718281828459045);

            if (debug) DumpTokens(parser);

            parser.MatchAndEat(TokenType.SCRIPT);

            Console.WriteLine("\n=============== Building block! =================\n");
            Node script = parser.Block();

            script.Eval();
          
        }

        public void DumpTokens(Parser parser)
        {
            foreach(Token token in parser.getTokens())
                Console.WriteLine("Type: " + token.type + " Text: " + token.text + " ");
                Console.WriteLine();
        }
    }
}
