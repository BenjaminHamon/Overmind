using System;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Overmind.Games.Engine
{
	// http://stackoverflow.com/questions/137933/what-is-the-best-scripting-language-to-embed-in-a-c-sharp-desktop-application

	public class Compiler
	{
		public Compiler()
		{
			this.provider = new Microsoft.CSharp.CSharpCodeProvider();

			options = new CompilerParameters()
			{
				GenerateExecutable = false,
				GenerateInMemory = true,
				IncludeDebugInformation = true,
			};

			options.ReferencedAssemblies.Add("System.dll");
			options.ReferencedAssemblies.Add("System.Core.dll");
			
			options.ReferencedAssemblies.Add("Overmind.Core.dll");
			options.ReferencedAssemblies.Add("Rackspace.Collections.Immutable.dll");

			options.ReferencedAssemblies.Add("Overmind.Games.Engine.dll");
		}

		private readonly CodeDomProvider provider;
		private readonly CompilerParameters options;

		public void AddAssembly(string assembly)
		{
			options.ReferencedAssemblies.Add(assembly);
		}

		public Assembly Compile(params string[] filePaths)
		{
			CompilerResults result = provider.CompileAssemblyFromFile(options, filePaths);

			if (result.Errors.HasErrors)
				throw new CompilerException("Compilation error", result.Errors);

			//if (result.Errors.HasWarnings)
				//throw new CompilerException("Compilation warning", result.Errors);

			return result.CompiledAssembly;
		}
	}
}
