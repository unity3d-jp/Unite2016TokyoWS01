using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
	public GameObject scoreObject;

	private Text scoreText;

	// Use this for initialization
	void Start () 
	{
		scoreText = scoreObject.GetComponent<Text>();
		UpdateCoin();
	}

	public void UpdateCoin()
	{
		int score = 0;
		if (PlayerPrefs.HasKey("CoinNum"))
		{
			score = PlayerPrefs.GetInt("CoinNum");
		}
		scoreText.text = score.ToString();
	}
}
