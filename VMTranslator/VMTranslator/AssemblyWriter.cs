using System;
using System.IO;

namespace VMTranslator
{
    public class AssemblyWriter
    {
        private static int emitCounter = 0;
        public static void EmitOperator(TextWriter writer, Operator op)
        {
            writer.WriteLine($"//{op}");
            var uniqueSuffix = MakeNextUniqueSuffix(op);
            switch (op)
            {
                case Operator.Add:
                    writer.WriteLine("@SP");
                    writer.WriteLine("A=M");
                    writer.WriteLine("A=A-1");
                    writer.WriteLine("D=M");
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M-1");
                    writer.WriteLine("M=M-1");
                    writer.WriteLine("A=M");
                    writer.WriteLine("M=D+M");
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M+1");
                    break;
                case Operator.Sub:
                    //writer.WriteLine("@SP");
                    //writer.WriteLine("M=M-1");
                    //writer.WriteLine("M=M-1");
                    //writer.WriteLine("A=M");
                    //writer.WriteLine("D=M");
                    //writer.WriteLine("A=A+1");
                    //writer.WriteLine("M=D-M");
                    //writer.WriteLine("D=M");
                    //writer.WriteLine("A=A-1");
                    //writer.WriteLine("M=D");
                    //writer.WriteLine("@SP");
                    //writer.WriteLine("M=M+1");
                    writer.WriteLine("@SP");
                    writer.WriteLine("A=M");
                    writer.WriteLine("A=A-1");
                    writer.WriteLine("D=M");
                    writer.WriteLine("D=-D");
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M-1");
                    writer.WriteLine("M=M-1");
                    writer.WriteLine("A=M");
                    writer.WriteLine("M=D+M");
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M+1");
                    break;
                case Operator.Neg:
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M-1");
                    writer.WriteLine("A=M");
                    writer.WriteLine("D=M");
                    writer.WriteLine("D=-D");
                    writer.WriteLine("M=D");
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M+1");
                    break;
                case Operator.Eq:
                    EmitComparisonOpCodes(writer, uniqueSuffix, "JEQ");
                    break;
                case Operator.Gt:                    
                    EmitComparisonOpCodes(writer,uniqueSuffix,"JGT");
                    break;
                case Operator.Lt:
                    EmitComparisonOpCodes(writer, uniqueSuffix, "JLT");
                    break;
                case Operator.And:
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M-1");
                    writer.WriteLine("A=M");
                    writer.WriteLine("D=M");
                    writer.WriteLine("A=A-1");
                    writer.WriteLine("M=D&M");
                    break;
                case Operator.Or:
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M-1");
                    writer.WriteLine("A=M");
                    writer.WriteLine("D=M");
                    writer.WriteLine("A=A-1");
                    writer.WriteLine("M=D|M");
                    break;
                case Operator.Not:
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M-1");
                    writer.WriteLine("A=M");
                    writer.WriteLine("D=M");
                    writer.WriteLine("D=!D");
                    writer.WriteLine("M=D");
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M+1");
                    break;
            }
        }
        private static int retNum = 0;
        public static void EmitReturn(Context ctx)
        {
            retNum++;
            ctx.Writer.WriteLine($"//return {ctx.CurrentFunction.Name}");
            ctx.Writer.WriteLine("@LCL");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("@endFrame");
            ctx.Writer.WriteLine("M=D");
            ctx.Writer.WriteLine("@5");
            ctx.Writer.WriteLine("D=A");
            ctx.Writer.WriteLine("@endFrame");
            ctx.Writer.WriteLine("M=M-D");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("A=D");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("@retAddr");
            ctx.Writer.WriteLine("M=D");
            EmitPop(ctx,MemorySegment.Arg, 0);
            ctx.Writer.WriteLine("@ARG");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("D=D+1");
            ctx.Writer.WriteLine("@SP");
            ctx.Writer.WriteLine("M=D");
            ctx.Writer.WriteLine("@endFrame");
            ctx.Writer.WriteLine("M=M+1");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("A=D");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("@LCL");
            ctx.Writer.WriteLine("M=D");
            ctx.Writer.WriteLine("@endFrame");
            ctx.Writer.WriteLine("M=M+1");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("A=D");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("@ARG");
            ctx.Writer.WriteLine("M=D");
            ctx.Writer.WriteLine("@endFrame");
            ctx.Writer.WriteLine("M=M+1");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("A=D");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("@THIS");
            ctx.Writer.WriteLine("M=D");
            ctx.Writer.WriteLine("@endFrame");
            ctx.Writer.WriteLine("M=M+1");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("A=D");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("@THAT");
            ctx.Writer.WriteLine("M=D");
            ctx.Writer.WriteLine("// returning");
            ctx.Writer.WriteLine($"@retAddr");
            ctx.Writer.WriteLine($"A=M");
            ctx.Writer.WriteLine("0;JMP");
        }
        private static void RestoreRegister(Context ctx, string register)
        {
            ctx.Writer.WriteLine($"@return_{retNum}");
            ctx.Writer.WriteLine("M=M-1");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("A=D");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine($"@{register}");
            ctx.Writer.WriteLine("M=D");
        }

        private static string MakeNextUniqueSuffix<T>(T op) => 
            $"{op.ToString().ToUpper()}_{++emitCounter}";


        private static void EmitComparisonOpCodes(TextWriter writer, string uniqueSuffix, string jumpInstruction)
        {
            writer.WriteLine("@SP");
            writer.WriteLine("A=M");
            writer.WriteLine("A=A-1");
            writer.WriteLine("D=M");
            writer.WriteLine("D=-D");
            writer.WriteLine("@SP");
            writer.WriteLine("M=M-1");
            writer.WriteLine("M=M-1");
            writer.WriteLine("A=M");
            writer.WriteLine("M=D+M");
            writer.WriteLine("D=M");
            writer.WriteLine($"@SET_TRUE_RETURN_{uniqueSuffix}");
            writer.WriteLine($"D;{jumpInstruction}");
            //
            writer.WriteLine($"(SET_FALSE_RETURN_{uniqueSuffix})");
            writer.WriteLine("@SP");
            writer.WriteLine("A=M");
            writer.WriteLine("M=0");
            writer.WriteLine($"@SP_PP_{uniqueSuffix}");
            writer.WriteLine("0;JMP");
            //
            writer.WriteLine($"(SET_TRUE_RETURN_{uniqueSuffix})");
            writer.WriteLine("@SP");
            writer.WriteLine("A=M");
            writer.WriteLine("M=-1");
            writer.WriteLine($"@SP_PP_{uniqueSuffix}");
            writer.WriteLine("0;JMP");
            //
            writer.WriteLine($"(SP_PP_{uniqueSuffix})");
            writer.WriteLine("@SP");
            writer.WriteLine("M=M+1");
        }

        /// <summary>
        /// Emits the push code.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        /// <param name="segment">The segment name.</param>
        /// <param name="index">The index inside the segment.</param>
        /// <exception cref="ArgumentOutOfRangeException">segment - Not Supported</exception>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="System.ArgumentOutOfRangeException">segment - null</exception>
        public static void EmitPush(Context ctx, MemorySegment segment, int index)
        {
            var writer = ctx.Writer;
            switch (segment)
            {
                case MemorySegment.Static:
                    writer.WriteLine($"//push static {index}");
                    writer.WriteLine($"@{Path.GetFileNameWithoutExtension(ctx.InputFilePath)}.{index}");
                    writer.WriteLine("D=M");
                    writer.WriteLine("@SP");
                    writer.WriteLine("A=M");
                    writer.WriteLine("M=D");
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M+1");
                    //PushToSegment(writer, segment, "@THAT", index);

                    break;
                case MemorySegment.This:
                    //writer.WriteLine($"//push this {index}");
                    //writer.WriteLine($"@{index}");
                    //writer.WriteLine("D=A");
                    //writer.WriteLine("@THIS");
                    //writer.WriteLine("A=M");
                    //writer.WriteLine("A=D+A");
                    //writer.WriteLine("D=M");
                    //writer.WriteLine("@SP");
                    //writer.WriteLine("A=M");
                    //writer.WriteLine("M=D");
                    //writer.WriteLine("@SP");
                    //writer.WriteLine("M=M+1");
                    PushToSegment(writer, segment, "@THIS", index);


                    break;
                case MemorySegment.LCL:
                    //writer.WriteLine($"//push local {index}");
                    //writer.WriteLine($"@{index}");
                    //writer.WriteLine("D=A");
                    //writer.WriteLine("@LCL");
                    //writer.WriteLine("A=M");
                    //writer.WriteLine("A=D+A");
                    //writer.WriteLine("D=M");
                    //writer.WriteLine("@SP");
                    //writer.WriteLine("A=M");
                    //writer.WriteLine("M=D");
                    //writer.WriteLine("@SP");
                    //writer.WriteLine("M=M+1");
                    PushToSegment(writer, segment, "@LCL", index);

                    break;
                case MemorySegment.Arg:
                    //writer.WriteLine($"//push argument {index}");
                    //writer.WriteLine($"@{index}");
                    //writer.WriteLine("D=A");
                    //writer.WriteLine("@ARG");
                    //writer.WriteLine("A=M");
                    //writer.WriteLine("A=D+A");
                    //writer.WriteLine("D=M");
                    //writer.WriteLine("@SP");
                    //writer.WriteLine("A=M");
                    //writer.WriteLine("M=D");
                    //writer.WriteLine("@SP");
                    //writer.WriteLine("M=M+1");
                    PushToSegment(writer, segment, "@ARG", index);

                    break;
                case MemorySegment.That:
                    PushToSegment(writer, segment, "@THAT", index);
                    //writer.WriteLine($"//push that {index}");
                    //writer.WriteLine($"@{index}");
                    //writer.WriteLine("D=A");
                    //writer.WriteLine("@THAT");
                    //writer.WriteLine("A=M");
                    //writer.WriteLine("A=D+A");
                    //writer.WriteLine("D=M");
                    //writer.WriteLine("@SP");
                    //writer.WriteLine("A=M");
                    //writer.WriteLine("M=D");
                    //writer.WriteLine("@SP");
                    //writer.WriteLine("M=M+1");
                    break;
                case MemorySegment.Constant:
                    writer.WriteLine($@"//push constant {index}");
                    writer.WriteLine($"@{index}");
                    writer.WriteLine("D=A");
                    writer.WriteLine("@SP");
                    writer.WriteLine("A=M");
                    writer.WriteLine("M=D");
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M+1");
                    break;
                case MemorySegment.Temp:
                    writer.WriteLine($"//push temp {index}");
                    writer.WriteLine($"@{5 + index}");
                    writer.WriteLine("D=M");
                    writer.WriteLine("@SP");
                    writer.WriteLine("A=M");
                    writer.WriteLine("M=D");
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M+1");

                    break;
                case MemorySegment.Pointer:
                    writer.WriteLine($"@{3 + index}");
                    writer.WriteLine("D=M");
                    writer.WriteLine("@SP");
                    writer.WriteLine("A=M");
                    writer.WriteLine("M=D");
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M+1");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(segment), segment, $"{segment} Not Supported");
            }
        }

        private static void PushToSegment(TextWriter writer, MemorySegment segment, string segmentVariable, int index)
        {
            writer.WriteLine($"//push {segment} {index}");
            writer.WriteLine($"@{index}");
            writer.WriteLine("D=A");
            writer.WriteLine(segmentVariable);
            writer.WriteLine("A=M");
            writer.WriteLine("A=D+A");
            writer.WriteLine("D=M");
            writer.WriteLine("@SP");
            writer.WriteLine("A=M");
            writer.WriteLine("M=D");
            writer.WriteLine("@SP");
            writer.WriteLine("M=M+1");
        }
        public static void EmitPop(Context ctx, MemorySegment segment, int index)
        {
            var writer = ctx.Writer;
            switch (segment)
            {
                case MemorySegment.Static:
                    writer.WriteLine($"//pop {segment} {index}");
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M-1");
                    writer.WriteLine("A=M");
                    writer.WriteLine("D=M");
                    writer.WriteLine($"@{Path.GetFileNameWithoutExtension(ctx.InputFilePath)}.{index}");
                    writer.WriteLine("M=D");
                    writer.WriteLine("@SP");
                    break;
                case MemorySegment.This:
                case MemorySegment.LCL:
                case MemorySegment.Arg:
                case MemorySegment.That:
                    PopASegment(segment, index, writer);
                    break;
                case MemorySegment.Constant:
                    writer.WriteLine($"//pop {segment} {index}");
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M-1");
                    writer.WriteLine("A=M");
                    writer.WriteLine("D=M");
                    writer.WriteLine($"@{256 + index}");
                    writer.WriteLine("M=D");
                    writer.WriteLine("@SP");

                    break;
                case MemorySegment.Temp:
                    writer.WriteLine($"//pop {segment} {index}");
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M-1");
                    writer.WriteLine("A=M");
                    writer.WriteLine("D=M");
                    writer.WriteLine($"@{5 + index}");
                    writer.WriteLine("M=D");
                    writer.WriteLine("@SP");

                    break;
                case MemorySegment.Pointer:
                    writer.WriteLine($"//pop {segment} {index}");
                    writer.WriteLine("@SP");
                    writer.WriteLine("M=M-1");
                    writer.WriteLine("A=M");
                    writer.WriteLine("D=M");
                    writer.WriteLine($"@{3 + index}");
                    writer.WriteLine("M=D");
                    writer.WriteLine("@SP");

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(segment), segment, "not implemented");
            }
        }

        private static void PopASegment(MemorySegment segment, int index, TextWriter writer)
        {
            var registerName = string.Empty;
            switch (segment)
            {
                case MemorySegment.This:
                case MemorySegment.LCL:
                case MemorySegment.Arg:
                case MemorySegment.That:
                    registerName = segment.ToString().ToUpper();
                    break;
                default:
                    throw new InvalidOperationException("Oink");
            }
            writer.WriteLine($"//pop {segment} {index}");
            writer.WriteLine($"@{index}");
            writer.WriteLine("D=A");
            writer.WriteLine("@SP");
            writer.WriteLine("M=M-1");
            writer.WriteLine("A=M");
            writer.WriteLine("D=M");
            writer.WriteLine("@foo");
            writer.WriteLine("M=D");
            writer.WriteLine($"@{index}");
            writer.WriteLine("D=A");
            writer.WriteLine($"@{registerName}");
            writer.WriteLine("A=D+M");
            writer.WriteLine("D=A");
            writer.WriteLine("@bar");
            writer.WriteLine("M=D");
            writer.WriteLine("@foo");
            writer.WriteLine("D=M");
            writer.WriteLine("@bar");
            writer.WriteLine("A=M");
            writer.WriteLine("M=D");
        }

        public static void EmitGoTo(Context ctx, string label)
        {
            ctx.Writer.WriteLine($"//Goto {label}");
            var prefixedLabel = $"{Path.GetFileNameWithoutExtension(ctx.InputFilePath)}";
            ctx.Writer.WriteLine($"@{prefixedLabel}_{label}");
            ctx.Writer.WriteLine("0;JMP");
        }

        public static void EmitIfGoTo(Context ctx, string label)
        {
            ctx.Writer.WriteLine($"//IfGoto {label}");
            var prefixedLabel = $"{Path.GetFileNameWithoutExtension(ctx.InputFilePath)}";
            ctx.Writer.WriteLine("@SP");
            ctx.Writer.WriteLine("M=M-1");
            ctx.Writer.WriteLine("A=M");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine($"@{prefixedLabel}_{label}");
            ctx.Writer.WriteLine("D;JNE");
        }

        public static void EmitLabel(Context ctx, string label)
        {
            ctx.Writer.WriteLine($"//Label {label}");
            var prefixedLabel = $"{Path.GetFileNameWithoutExtension(ctx.InputFilePath)}";
            ctx.Writer.WriteLine($"({prefixedLabel}_{label})");
        }
        
        public static void EmitFunction(Context ctx, string function, int localVars)
        {   
            ctx.Writer.WriteLine($"//FunctionDef for {function}");
            var prefixedLabel = $"{Path.GetFileNameWithoutExtension(ctx.InputFilePath)}".ToUpper();
            ctx.Writer.WriteLine($"({prefixedLabel.ToUpper()}_{function})");
            ctx.Writer.WriteLine("@SP");
            ctx.Writer.WriteLine("@LCL");
            ctx.Writer.WriteLine("@SP");
            for (int i = 0; i < localVars; i++)
            {
                ctx.Writer.WriteLine("@SP");
                ctx.Writer.WriteLine("D=M");
                ctx.Writer.WriteLine("A=D");
                ctx.Writer.WriteLine("M=0");
                ctx.Writer.WriteLine("@SP");
                ctx.Writer.WriteLine("M=M+1");
            }
            ctx.Writer.WriteLine("@SP");
            ctx.Writer.WriteLine("@LCL");
            ctx.Writer.WriteLine("@SP");
        }
        
        public static void EmitCall(Context ctx,string function,int args)
        {
            ctx.Writer.WriteLine($"//Call from within {ctx.CurrentFunction.Name} calling {function} {args}");
            var prefixedLabel = $"{Path.GetFileNameWithoutExtension(ctx.InputFilePath)}".ToUpper();
            ctx.Writer.WriteLine($"@RETURNADRESS_{ctx.CurrentFunction.Name}");
            ctx.Writer.WriteLine("D=A");
            ctx.Writer.WriteLine("@SP");
            ctx.Writer.WriteLine("A=M");
            ctx.Writer.WriteLine("M=D");
            ctx.Writer.WriteLine("@SP");
            ctx.Writer.WriteLine("M=M+1");
            ctx.Writer.WriteLine("@LCL");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("@SP");
            ctx.Writer.WriteLine("A=M");
            ctx.Writer.WriteLine("M=D");
            ctx.Writer.WriteLine("@SP");
            ctx.Writer.WriteLine("M=M+1");
            ctx.Writer.WriteLine("@ARG");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("@SP");
            ctx.Writer.WriteLine("A=M");
            ctx.Writer.WriteLine("M=D");
            ctx.Writer.WriteLine("@SP");
            ctx.Writer.WriteLine("M=M+1");
            ctx.Writer.WriteLine("@THIS");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("@SP");
            ctx.Writer.WriteLine("A=M");
            ctx.Writer.WriteLine("M=D");
            ctx.Writer.WriteLine("@SP");
            ctx.Writer.WriteLine("M=M+1");
            ctx.Writer.WriteLine("@THAT");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("@SP");
            ctx.Writer.WriteLine("A=M");
            ctx.Writer.WriteLine("M=D");
            ctx.Writer.WriteLine("@SP");
            ctx.Writer.WriteLine("M=M+1");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine($"@{5 + args}");
            ctx.Writer.WriteLine("D=D-A");
            ctx.Writer.WriteLine("@ARG");
            ctx.Writer.WriteLine("M=D");
            ctx.Writer.WriteLine("@SP");
            ctx.Writer.WriteLine("D=M");
            ctx.Writer.WriteLine("@LCL");
            ctx.Writer.WriteLine("M=D");
            ctx.Writer.WriteLine($"@{prefixedLabel.ToUpper()}_{function}");
            ctx.Writer.WriteLine("0;JMP");
            ctx.Writer.WriteLine($"(RETURNADRESS_{ctx.CurrentFunction.Name})");
        }



    }

}