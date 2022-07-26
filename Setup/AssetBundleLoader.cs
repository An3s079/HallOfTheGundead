using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HallOfGundead
{
	class AssetBundleLoader
	{

		public static AssetBundle LoadAssetBundleFromLiterallyAnywhere(string name)
		{
				AssetBundle result = null;
				bool flag4 = File.Exists(ETGMod.FolderPath(FloorModModule.Instance) + "/" + name);
				if (flag4)
				{
					try
					{
						result = AssetBundle.LoadFromFile(Path.Combine(ETGMod.FolderPath(FloorModModule.Instance), name));
						Debug.Log("Successfully loaded assetbundle!");
					}
					catch (Exception ex)
					{
						Debug.LogError("Failed loading asset bundle from file.");
						Debug.LogError(ex.ToString());
					}
				}
				else
				{
					Debug.LogError("AssetBundle NOT FOUND!");
				}

			return result;
		}
	}
}
