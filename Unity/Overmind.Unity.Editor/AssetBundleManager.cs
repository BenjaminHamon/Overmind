using System.IO;
using UnityEditor;
using UnityEngine;

namespace Overmind.Unity.Editor
{
	public static class AssetBundleManager
	{
		[MenuItem("Overmind/Asset Bundles/Build All")]
		public static void BuildAllAssetBundles()
		{
			string outputDirectory = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "AssetBundles");
			Directory.CreateDirectory(outputDirectory);
			BuildPipeline.BuildAssetBundles("AssetBundles");
		}
	}
}
