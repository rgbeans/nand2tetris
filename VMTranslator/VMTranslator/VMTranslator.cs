using System;
using System.IO;

namespace VMTranslator
{
    internal class VMTranslator
    {
        private static void Main(string[] args)
        {
            if (args == null || args.Length < 1)
            {
                var msg = "Expected one argument. The path to .vm file";
                throw new ArgumentException(msg);
            }

            var path = args[0];
            if (!File.Exists(path))
            {
                var msg = $"file {path} does not exist";
                throw new FileNotFoundException(msg);
            }
            var outFile = Path.ChangeExtension(path, ".asm");
            using (var ctx = new Context(path, File.CreateText(outFile)))
            {
                var callbacks = new ParserCallBacks
                {
                    OnPop =
                        (segment, index) =>
                            AssemblyWriter.EmitPop(ctx, segment,
                                index), 
                    OnPush = (segment, index) => AssemblyWriter.EmitPush(ctx, segment, index),
                    OnArithmeticCommand = command => AssemblyWriter.EmitOperator(ctx.Writer, command)
                };
                Parser.Parse(File.ReadAllLines(path), callbacks);
                ctx.Writer.Flush();
            }
        }
    }
}