using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.SceneManagement;

public class BuildLightMap 
{
	[MenuItem("Window/Bake Lightmap All")]
	static void CreateLightmapAllScene() 
	{
		string nowSceneName = EditorSceneManager.GetActiveScene().path;
		EditorSceneManager.OpenScene("Assets/Example/Scenes/CompleteScene.unity");
		Lightmapping.Bake();
		EditorSceneManager.OpenScene("Assets/Example/Scenes/Workshop01.unity");
		Lightmapping.Bake();
		EditorSceneManager.OpenScene(nowSceneName);
	}

}
