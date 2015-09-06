using Overmind.Core;
using System.CodeDom.Compiler;

namespace Overmind.Games.Engine
{
    public class CompilerException : OvermindException
    {
        public CompilerException(string message, CompilerErrorCollection errors)
            : base(message)
        {
            this.errors = errors;
        }

        private readonly CompilerErrorCollection errors;
        public CompilerErrorCollection Errors { get { return errors; } }
    }
}
