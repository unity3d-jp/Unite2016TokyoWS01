using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Assertions;

namespace CompleteProject
{
	public class CharaSelectController : MonoBehaviour 
	{
		public GameObject characterSelectionPanel;
		public GameObject newOneUI;
		public GameObject defaultChara;
		public GameObject newOneChara;
		public GameObject playerObj;
		private GoalChecker goalChecker;

		private Button newOneButton;
		// Use this for initialization
		void Awake () 
		{
			newOneButton = newOneUI.GetComponent<Button>();
			playerObj.SetActive(false);
			defaultChara.SetActive(false);
			newOneChara.SetActive(false);

			// 非消費アイテムを買ったか否かで表示非表示の判定
			if (PlayerPrefs.GetInt("NewCharaUnlocked") == 0)
			{
				newOneButton.interactable = false;
			}
			else
			{
				newOneButton.interactable = true;
			}
		}

		void Start()
		{
			GameObject go = GameObject.Find("GoalObj");
			Assert.IsNotNull(go);
			goalChecker = go.GetComponent<GoalChecker>();
			Assert.IsNotNull(goalChecker);
		}
		
		public void DefaultClicked()
		{
			//GameParameters.gameStarted = true;
			Vector3 pos = playerObj.transform.position;
			pos.y -= 0.5f;
			defaultChara.transform.position = pos;
			newOneChara.SetActive(false);
			defaultChara.SetActive(true);
			//Everyplay.StartRecording();
			CommonStart();
		}

		public void NewOneClicked()
		{
			//GameParameters.gameStarted = true;
			Vector3 pos = playerObj.transform.position;
			pos.y -= 0.5f;
			newOneChara.transform.position = pos;
			defaultChara.SetActive(false);
			newOneChara.SetActive(true);
			//Everyplay.StartRecording();
			CommonStart();
		}

		void CommonStart()
		{
			int score = 0;
			if (PlayerPrefs.HasKey("CoinNum"))
			{
				score = PlayerPrefs.GetInt("CoinNum");
			}
			score -= 10;
			if ( score < 0 ) score = 0;
			PlayerPrefs.SetInt("CoinNum", score);
			GameObject go = GameObject.Find("CoinNumUI");
			Assert.IsNotNull(go);
			ScoreManager sm = go.GetComponent<ScoreManager>();
			Assert.IsNotNull(sm);
			sm.UpdateCoin();

			GameObject bgmObj = GameObject.Find("BGM");
			Assert.IsNotNull(bgmObj);
			AudioSource audio = bgmObj.GetComponent<AudioSource>();
			Assert.IsNotNull(audio);
			audio.Play();

			characterSelectionPanel.SetActive(false);
			playerObj.SetActive(true);
			goalChecker.GameStart();
		}
	}
}
