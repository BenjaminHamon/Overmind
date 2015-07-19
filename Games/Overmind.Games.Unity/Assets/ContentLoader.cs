using System;
using System.Collections;
using UnityEngine;

namespace Overmind.Unity
{
	public class ContentLoader
	{
		public AssetBundle LoadAssetBundle(string assetBundleName)
		{
			throw new NotImplementedException();

			using (WWW download = new WWW("file:///E:/Projects/Overmind/Games/Overmind.Games.Unity/AssetBundles/chess"))
			{
				bool forceExit = false;
				while (download.isDone == false) // download does not update/start
					if (forceExit) throw new Exception("Forced to exit");
				if (String.IsNullOrEmpty(download.error) == false)
					throw new Exception("[ContentLoader.LoadAssetBundle] Download error: " + download.error);
				return download.assetBundle;
			}
		}

		public IEnumerator LoadAssetBundleAsync(string assetBundleName, Action<AssetBundle> callback)
		{
			//using (WWW download = new WWW("file:///E:/Projects/Overmind/Games/Overmind.Games.Unity/AssetBundles/chess"))
			using (WWW download = new WWW("file:///E:/Projects/Overmind/Games/Mods/Overmind.Chess.AssetBundles/AssetBundles/chess"))
			{
				yield return download;
				if (String.IsNullOrEmpty(download.error) == false)
					throw new Exception("[ContentLoader.LoadAssetBundleAsync] Download error: " + download.error);
				callback(download.assetBundle);
			}
		}
	}
}
