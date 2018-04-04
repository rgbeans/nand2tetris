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
        [Test]
        public void TestParser()
        {
            const string root = @"C:\Users\Dean.Pavlovsky\Projects\nand2tetris\07";
            var paths = new[]
            {
                $@"{root}\MemoryAccess\BasicTest\BasicTest.vm",
                $@"{root}\MemoryAccess\PointerTest\PointerTest.vm",
                $@"{root}\MemoryAccess\StaticTest\StaticTest.vm",
                $@"{root}\StackArithmetic\SimpleAdd\SimpleAdd.vm",
                $@"{root}\StackArithmetic\StackTest\StackTest.vm"
            };
            foreach (var path in paths)
            {
                Console.WriteLine($"processing {path}");

                var outFile = Path.ChangeExtension(path, ".asm");
                using (var ctx = new Context(path, File.CreateText(outFile)))
                {
                    var callbacks = new ParserCallBacks
                    {
                        OnPop =
                            (segment, index) =>
                                AssemblyWriter.EmitPop(ctx, segment,
                                    index), // Console.WriteLine($"Emit Pop({segment},{index})"),
                        OnPush = (segment, index) => AssemblyWriter.EmitPush(ctx, segment, index),
                        OnArithmeticCommand = command => AssemblyWriter.EmitOperator(ctx.Writer, command)
                    };
                    Parser.Parse(File.ReadAllLines(path), callbacks);
                    ctx.Writer.Flush();
                }
            }
        }
        [Test]
        public void TestParser2()
        {
        
            const string root = @"C:\Users\Dean.Pavlovsky\Projects\nand2tetris\08";
            var paths = Directory.EnumerateFiles(root, "*.vm", SearchOption.AllDirectories);
            //var paths = new[]
            //{
            //    $@"{root}\MemoryAccess\BasicTest\BasicTest.vm",
            //    $@"{root}\MemoryAccess\PointerTest\PointerTest.vm",
            //    $@"{root}\MemoryAccess\StaticTest\StaticTest.vm",
            //    $@"{root}\StackArithmetic\SimpleAdd\SimpleAdd.vm",
            //    $@"{root}\StackArithmetic\StackTest\StackTest.vm"
            //};
            foreach (var path in paths)
            {
                Console.WriteLine($"processing {path}");

                var outFile = Path.ChangeExtension(path, ".asm");
                var sb = new StringBuilder();
                using (var ctx = new Context(path, File.CreateText(outFile)/*new StringWriter(sb)*/))
                {
                    var callbacks = new ParserCallBacks
                    {
                        OnPop =
                            (segment, index) =>
                                AssemblyWriter.EmitPop(ctx,segment,index),
                        OnPush = (segment, index) =>
                            AssemblyWriter.EmitPush(ctx,segment,index),
                        OnArithmeticCommand = command =>
                            AssemblyWriter.EmitOperator(ctx.Writer,command),
                        OnGoTo = label =>
                            AssemblyWriter.EmitGoTo(ctx,label),
                        OnIfGoTo = label =>
                            AssemblyWriter.EmitIfGoTo(ctx,label),
                        OnLabelDeclaration = label =>
                            AssemblyWriter.EmitLabel(ctx, label),
                        OnCall = (command,n) =>
                            Console.WriteLine($">> call {command},{n}"),
                        OnFuncDef = (command, n) =>
                            Console.WriteLine($">> function {command},{n}"),
                        OnReturn = () =>
                            Console.WriteLine($">> return"),

                    };
                    Parser.Parse(File.ReadAllLines(path), callbacks);
                    ctx.Writer.Flush();
                }

                Console.WriteLine(sb);
            }
        }
    }
}