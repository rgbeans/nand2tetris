using System;
using System.IO;

namespace VMTranslator
{
    public class FunctionContext
    {
        public string Name { get; }
        public int ArgNum { get; }
        public FunctionContext(string functionName,int numOfArgs)
        {
            Name = functionName;
            ArgNum = numOfArgs;
        }
    }
    public class Context : IDisposable
    {
        public Context() : this(new StringWriter())
        {
        }

        public Context(TextWriter output)
        {
            
            Writer = output;
        }
        public FunctionContext CurrentFunction { get; set; }
        public string InputFilePath { get; set; }
        public TextWriter Writer { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            Writer.Flush();
            Writer?.Dispose();
        }
    }
}