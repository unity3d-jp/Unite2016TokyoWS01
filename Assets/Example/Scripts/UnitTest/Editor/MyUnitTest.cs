using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class MyUnitTest {

    [Test]
    public void EditorTest()
    {
        //Arrange
		GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>
			("Assets/Example/Prefabs/Player.prefab");
		Assert.NotNull(go);

        //Act
		CompleteProject.PlayerController player = 
			go.GetComponent<CompleteProject.PlayerController>();
		player.SetCountText();

        //Assert
        //The object has a new name
		Assert.NotNull(player);
    }
}
