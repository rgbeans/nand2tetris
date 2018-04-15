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
                var msg = "Expected one argument. The path to a directory with *.vm files";
                throw new ArgumentException(msg);
            }

            var root = args[0];
            if (!Directory.Exists(root))
            {
                var msg = $"directory {root} does not exist";
                throw new FileNotFoundException(msg);
            }

            var paths = Directory.EnumerateFiles(root, "*.vm", SearchOption.AllDirectories);
            var dirName = Path.GetFileName(root);
            var outFile = Path.Combine(root, $"{dirName}.asm");
            using (var ctx = new Context(File.CreateText(outFile)))
            {
                ctx.Writer.WriteLine("@256");
                ctx.Writer.WriteLine("D=A");
                ctx.Writer.WriteLine("@SP");
                ctx.Writer.WriteLine("M=D");
                var sysInit = Path.Combine(root, "Sys.vm");
                if (File.Exists(sysInit))
                {
                    string EntryPoint = "Sys.Init".ToUpper();
                    ctx.CurrentFunction = new FunctionContext(EntryPoint, 0);
                    ctx.InputFilePath = "Sys.vm";
                    AssemblyWriter.EmitCall(ctx, EntryPoint, 0);
                }
                foreach (var path in paths)
                {
                    Console.WriteLine($"processing {path}");
                    var inputFile = Path.GetFileName(path);
                    ctx.InputFilePath = inputFile;


                    var callbacks = new ParserCallBacks
                    {
                        OnPop =
                            (segment, index) =>
                                AssemblyWriter.EmitPop(ctx, segment, index),
                        OnPush = (segment, index) =>
                            AssemblyWriter.EmitPush(ctx, segment, index),
                        OnArithmeticCommand = command =>
                            AssemblyWriter.EmitOperator(ctx.Writer, command),
                        OnGoTo = label =>
                            AssemblyWriter.EmitGoTo(ctx, label),
                        OnIfGoTo = label =>
                            AssemblyWriter.EmitIfGoTo(ctx, label),
                        OnLabelDeclaration = label =>
                            AssemblyWriter.EmitLabel(ctx, label),
                        OnCall = (command, n) =>
                            AssemblyWriter.EmitCall(ctx, command.ToUpper(), n),
                        OnFuncDef = (functionName, n) =>
                        {
                            var name = functionName.ToUpper();
                            ctx.CurrentFunction = new FunctionContext(name, n);
                            AssemblyWriter.EmitFunction(ctx, name, n);
                        },
                        OnReturn = () =>
                        {
                            AssemblyWriter.EmitReturn(ctx);
                        },

                    };
                    Parser.Parse(File.ReadAllLines(path), callbacks);
                    ctx.Writer.Flush();

                }
            }

        }
    }
}