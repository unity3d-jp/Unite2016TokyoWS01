using UnityEngine;
using UnityEditor;

public class EnableSnap
{
	static bool enableSnap = false;
	[InitializeOnLoadMethod]
	static void DoEnableSnap ()
	{
		SceneView.onSceneGUIDelegate += (sceneView) => {
			Handles.BeginGUI();
			enableSnap = EditorGUILayout.ToggleLeft ( "Enable Snap", enableSnap);
			Handles.EndGUI();
			if (enableSnap)
				Event.current.command = true;
		};
	}
}