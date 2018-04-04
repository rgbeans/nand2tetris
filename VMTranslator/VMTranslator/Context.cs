using System;
using System.IO;

namespace VMTranslator
{
    public class Context : IDisposable
    {
        public Context(string input) : this(input, new StringWriter())
        {
        }

        public Context(string input, TextWriter output)
        {
            if (!File.Exists(input)) throw new FileNotFoundException($"file {input} was not found");
            InputFilePath = input;
            Writer = output;
        }

        public string InputFilePath { get; }
        public TextWriter Writer { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            Writer.Flush();
            Writer?.Dispose();
        }
    }
}