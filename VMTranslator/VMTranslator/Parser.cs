using System;
using System.Collections.Generic;
using System.Linq;
using VMTranslator;

public class ParserCallBacks
{
    public Action OnReturn;
    public Action<string, short> OnCall { get; set; }
    public Action<string,short> OnFuncDef { get; set; }
    public Action<Operator> OnArithmeticCommand { get; set; }

    public Action<MemorySegment, short> OnPush { get; set; }

    public Action<MemorySegment, short> OnPop { get; set; }

    public Action<string> OnLabelDeclaration { get; set; }
    public Action<string> OnGoTo { get; set; }
    public Action<string> OnIfGoTo { get; set; }
    
}

public class Parser
{
    public static void Parse(IEnumerable<string> lines, ParserCallBacks callbacks)
    {
        IEnumerable<string> filteredLines = from line in lines
            where !string.IsNullOrWhiteSpace(line)
            select line.Trim();
        foreach (string item in filteredLines)
        {
            string[] elems = item.Split((string[]) null, StringSplitOptions.RemoveEmptyEntries);
            if (elems.Length != 0)
            {
                string opcode = elems.First();
                if (!opcode.StartsWith("//"))
                    switch (opcode)
                    {
                        case "add":
                            callbacks.OnArithmeticCommand(Operator.Add);
                            break;
                        case "sub":
                            callbacks.OnArithmeticCommand(Operator.Sub);
                            break;
                        case "neg":
                            callbacks.OnArithmeticCommand(Operator.Neg);
                            break;
                        case "eq":
                            callbacks.OnArithmeticCommand(Operator.Eq);
                            break;
                        case "gt":
                            callbacks.OnArithmeticCommand(Operator.Gt);
                            break;
                        case "lt":
                            callbacks.OnArithmeticCommand(Operator.Lt);
                            break;
                        case "and":
                            callbacks.OnArithmeticCommand(Operator.And);
                            break;
                        case "or":
                            callbacks.OnArithmeticCommand(Operator.Or);
                            break;
                        case "not":
                            callbacks.OnArithmeticCommand(Operator.Not);
                            break;
                        case "push":
                            if (elems.Length < 3) throw new ArgumentException($"invalid line {item}");
                            callbacks.OnPush(ParseMemorySegment(elems[1]), short.Parse(elems[2]));
                            break;
                        case "pop":
                            if (elems.Length < 3) throw new ArgumentException($"invalid line {item}");
                            callbacks.OnPop(ParseMemorySegment(elems[1]), short.Parse(elems[2]));
                            break;
                        case "label":
                            if (elems.Length < 2 || string.IsNullOrWhiteSpace(elems[1])) throw new ArgumentException($"invalid line {item}");
                            callbacks.OnLabelDeclaration(elems[1].Trim());
                            break;
                        case "goto":
                            if (elems.Length < 2 || string.IsNullOrWhiteSpace(elems[1])) throw new ArgumentException($"invalid line {item}");
                            callbacks.OnGoTo(elems[1].Trim());
                            break;
                        case "if-goto":
                            if (elems.Length < 2 || string.IsNullOrWhiteSpace(elems[1])) throw new ArgumentException($"invalid line {item}");
                            callbacks.OnIfGoTo(elems[1].Trim());
                            break;
                        case "function":
                            if (elems.Length < 3 || string.IsNullOrWhiteSpace(elems[1]) 
                                                 || string.IsNullOrWhiteSpace(elems[2]))
                                throw new ArgumentException($"invalid line {item}");
                            callbacks.OnFuncDef(elems[1].Trim(),short.Parse(elems[2]));
                            break;
                        case "call":
                            if (elems.Length < 3 || string.IsNullOrWhiteSpace(elems[1])
                                                 || string.IsNullOrWhiteSpace(elems[2]))
                                throw new ArgumentException($"invalid line {item}");
                            callbacks.OnCall(elems[1].Trim(), short.Parse(elems[2]));
                            break;
                        case "return":
                            callbacks.OnReturn();
                            break;
                        default:
                            throw new NotImplementedException($"Can't parse {item}");
                    }
            }
        }
    }

    private static MemorySegment ParseMemorySegment(string s)
    {
        switch (s)
        {
            case "static":
                return MemorySegment.Static;
            case "argument":
                return MemorySegment.Arg;
            case "constant":
                return MemorySegment.Constant;
            case "local":
                return MemorySegment.LCL;
            case "pointer":
                return MemorySegment.Pointer;
            case "temp":
                return MemorySegment.Temp;
            case "that":
                return MemorySegment.That;
            case "this":
                return MemorySegment.This;
            default:
                throw new ArgumentException(string.Format("Invalid segment name:  {0}", s));
        }
    }
}