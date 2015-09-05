using Newtonsoft.Json;
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

		// The DLL file for the loaded assembly is locked until UnityEditor is closed.
		// I tried to load it in an AppDomain without success.
		// http://stackoverflow.com/questions/6258160/unloading-the-assembly-loaded-with-assembly-loadfrom

		// TODO: Add mod loading with already loaded assemblies when it is not possible to dynamically load them (e.g. iOS).
		// In this case, the mods would be packaged with the application.

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
