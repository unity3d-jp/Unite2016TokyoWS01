using UnityEngine;
using UnityEditor;
using System.Collections;

public class BuildABs
{
	[MenuItem("Assets/BuildABs")]
	static void DoCreateABs() 
	{
		// Put the bundles in a folder called "AssetBundles" within the Assets folder.
		BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.Android);
	}
}
