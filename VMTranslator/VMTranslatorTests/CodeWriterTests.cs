using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using VMTranslator;

namespace VMTranslatorTests
{
    [TestFixture]
    public class CodeWriterTests
    {
        //[Test]
        //public void TestParser()
        //{
        //    const string root = @"C:\Users\Dean.Pavlovsky\Projects\nand2tetris\07";
        //    var paths = new[]
        //    {
        //        $@"{root}\MemoryAccess\BasicTest\BasicTest.vm",
        //        $@"{root}\MemoryAccess\PointerTest\PointerTest.vm",
        //        $@"{root}\MemoryAccess\StaticTest\StaticTest.vm",
        //        $@"{root}\StackArithmetic\SimpleAdd\SimpleAdd.vm",
        //        $@"{root}\StackArithmetic\StackTest\StackTest.vm"
        //    };
        //    foreach (var path in paths)
        //    {
        //        Console.WriteLine($"processing {path}");

        //        var outFile = Path.ChangeExtension(path, ".asm");
        //        using (var ctx = new Context(path, File.CreateText(outFile)))
        //        {
        //            var callbacks = new ParserCallBacks
        //            {
        //                OnPop =
        //                    (segment, index) =>
        //                        AssemblyWriter.EmitPop(ctx, segment,
        //                            index), // Console.WriteLine($"Emit Pop({segment},{index})"),
        //                OnPush = (segment, index) => AssemblyWriter.EmitPush(ctx, segment, index),
        //                OnArithmeticCommand = command => AssemblyWriter.EmitOperator(ctx.Writer, command)
        //            };
        //            Parser.Parse(File.ReadAllLines(path), callbacks);
        //            ctx.Writer.Flush();
        //        }
        //    }
        //}
        [Test]
        public void TestParser2()
        {

            //const string root = @"C:\Users\Dean.Pavlovsky\Projects\nand2tetris\08\FunctionCalls\NestedCall";
            //const string root = @"C:\Users\Dean.Pavlovsky\Projects\nand2tetris\08\ProgramFlow\BasicLoop";
            const string root = @"C:\Users\Dean.Pavlovsky\Projects\nand2tetris\08\ProgramFlow\FibonacciSeries";
            //const string root = @"C:\Users\Dean.Pavlovsky\Projects\nand2tetris\08\FunctionCalls\SimpleFunction";
            //const string root = @"C:\Users\Dean.Pavlovsky\Projects\nand2tetris\08\FunctionCalls\FibonacciElement";
            //const string root = @"C:\Users\Dean.Pavlovsky\Projects\nand2tetris\08\FunctionCalls\StaticsTest";
            var paths = Directory.EnumerateFiles(root, "*.vm", SearchOption.AllDirectories);
            //var paths = new[]
            //{
            //    $@"{root}\MemoryAccess\BasicTest\BasicTest.vm",
            //    $@"{root}\MemoryAccess\PointerTest\PointerTest.vm",
            //    $@"{root}\MemoryAccess\StaticTest\StaticTest.vm",
            //    $@"{root}\StackArithmetic\SimpleAdd\SimpleAdd.vm",
            //    $@"{root}\StackArithmetic\StackTest\StackTest.vm"
            //};
            var dirName = Path.GetFileName(root);
            var outFile = Path.Combine(root, $"{dirName}.asm");
            using (var ctx = new Context(File.CreateText(outFile)/*new StringWriter(sb)*/))
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
                    AssemblyWriter.EmitCall(ctx,EntryPoint,0);
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
                        //Console.WriteLine($">> call {command},{n}"),
                        OnFuncDef = (functionName, n) =>
                            {
                                var name = functionName.ToUpper();
                                ctx.CurrentFunction = new FunctionContext(name, n);
                                AssemblyWriter.EmitFunction(ctx, name, n);
                            },
                        OnReturn = () =>
                            {
                                AssemblyWriter.EmitReturn(ctx);
                                //ctx.CurrentFunction = null;
                            },

                    };
                    Parser.Parse(File.ReadAllLines(path), callbacks);
                    ctx.Writer.Flush();

                }
            }
        }
    }
}