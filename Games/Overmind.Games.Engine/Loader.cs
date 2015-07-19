using Newtonsoft.Json;
using Overmind.Core;
using Overmind.Core.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Overmind.Games.Engine
{
	public class Loader
	{
		public Loader(string modPath)
		{
			this.modPath = modPath;
		}

		private readonly string modPath;

		public IEnumerable<string> GetModList()
		{
			return Directory.GetFiles(modPath, "*.json").Select(file => Path.GetFileNameWithoutExtension(file));
		}

		// Consider .NET AddIn for compiled assembly load. https://msdn.microsoft.com/en-us/library/bb384200(v=vs.110).aspx
		// http://stackoverflow.com/questions/835182/choosing-between-mef-and-maf-system-addin

		// Assembly unload when game ends? http://stackoverflow.com/questions/6258160/unloading-the-assembly-loaded-with-assembly-loadfrom
		// DLL is locked if not unloaded (in Unity).

		public Game Load(string modName)
		{
			string modFilePath = Path.Combine(modPath, modName + ".json");
			if (File.Exists(modFilePath) == false)
				throw new Exception(String.Format("[Loader.Load] Mod file not found (Mod={0}, Path={1})", modName, modFilePath));
			string json = File.ReadAllText(modFilePath);
			Mod mod = JsonConvert.DeserializeObject<Mod>(json);

			string assemblyFilePath = Path.Combine(modPath, modName + ".dll");
			if (File.Exists(assemblyFilePath) == false)
				throw new Exception(String.Format("[Loader.Load] Mod assembly file not found (Mod={0}, Path={1})", modName, assemblyFilePath));
			Assembly assembly = Assembly.LoadFrom(assemblyFilePath);

			// Switched to precompiled assembly because compilation made Unity crash.
			// TODO: handle engine targeting different framework version
			//Compiler compiler = new Compiler();
			//Assembly assembly = compiler.Compile(mod.FileCollection.Select(file => PathExtensions.Combine(modPath, modName, file)).ToArray());
			
			IGameBuilder builder = (IGameBuilder)assembly.CreateInstance(mod.EntryPoint);
			return builder.Create();
		}
	}
}
