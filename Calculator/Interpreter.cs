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
            bool debug = false;

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

            if (debug) DumpTokens(parser);

            parser.MatchAndEat(TokenType.SCRIPT);

            List<Node> script = parser.Block();

            foreach (Node statement in script)
                statement.Eval();


            /*Tokenizer tokenizer = new Tokenizer();

            Parser parser = new Parser(tokenizer.Tokenize(source));
            //parser.setVariable("PI", 3.14159265358979);
            //parser.setVariable("EULER", 2.718281828459045);

            if (debug)
                DumpTokens(parser);

            parser.MatchAndEat(TokenType.SCRIPT);

            List<Node> script = parser.Block();

            foreach(Node statement in script)
                statement.Eval(); */
        }

        public void DumpTokens(Parser parser)
        {
            foreach(Token token in parser.getTokens())
                Console.WriteLine("Type: " + token.type + " Text: " + token.text + " ");
                Console.WriteLine();
        }

        /*
        private String ReadFile(String path)
        {
            StreamReader stream = null;
            Stream input = null;

            try
            {
                stream = new StreamReader(path);

                input = new StreamReader(stream);

                StreamReader reader = new StreamReader(input);

                StringBuilder builder = new StringBuilder();

                char[] buffer = new char[8192];
                int read;

                while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    builder.Append(buffer, 0, read);
                }
                //Keep the space in the end

                builder.Append(" ");
                return builder.ToString();
            }
            catch (FileNotFoundException e)
            {
                String errMsg = "FILE NOT FOUND. ";
                String sourceInfo = "Error in Interpreter.java->"
                + "ReadFile(String path) method. ";
                Console.WriteLine(sourceInfo + errMsg);
                Environment.Exit(0);
            }
            catch (IOException e)
            {
                String errMsg = "Error while reading the script. ";
                String sourceInfo = "Error in Interpreter.java->"
                + "ReadFile(String path) method. ";
                Console.WriteLine(sourceInfo + errMsg);
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                String errMsg = "Error while reading the script. ";
                String sourceInfo = "Error in Interpreter.java->"
                + "ReadFile(String path) method. ";
                Console.WriteLine(sourceInfo + errMsg + e);
                Environment.Exit(0);
            }
            finally
            {
                try
                {
                    input.Close();
                    stream.Close();
                }
                catch (Exception e)
                {
                    String errMsg = "Error while closing a stream or a stream reader. ";
                    String sourceInfo = "Error in Interpreter.java->"
                    + "ReadFile(String path) method. ";
                    Console.WriteLine(sourceInfo + errMsg + e);
                    Environment.Exit(0);
                }
            }

        return null;
        } */

    }
}
