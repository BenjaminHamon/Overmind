using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Overmind.Games.Engine
{
	[DataContract]
	public class Mod
	{
		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string AssemblyName { get; set; }

		[DataMember]
		public IEnumerable<string> FileCollection { get; set; }

		[DataMember]
		public string EntryPoint { get; set; }
	}
}
