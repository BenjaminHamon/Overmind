using Overmind.Core;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
