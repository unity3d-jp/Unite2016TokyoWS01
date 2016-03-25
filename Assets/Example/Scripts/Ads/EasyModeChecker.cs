using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EasyModeChecker : MonoBehaviour
{
	public GameObject easyModeObject;
	public Text gameModeText;

	void Start ()
	{
		if (GameModeController.currentMode == GameMode.EASY) {
			easyModeObject.SetActive (true);
			GameModeController.resetMode ();
			gameModeText.text = "EASY Mode";
		} else {
			easyModeObject.SetActive (false);
			gameModeText.text = "NORMAL Mode";
		}
	}
}
