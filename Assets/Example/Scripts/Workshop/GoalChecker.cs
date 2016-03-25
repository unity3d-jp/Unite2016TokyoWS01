using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
// TODO:
// namespace を追加

public class GoalChecker : MonoBehaviour {

	public Text goalText;
	private Rigidbody playerRb;
	public Text timerText;

	bool isGoaled = false;
	public AudioSource goalJingle;
	public AudioSource bgm;

	DateTime startTime;
	TimeSpan nowTime;
	public GameObject nextButton;
	public GameObject twitterButton;
	string displayTime;
	int goalTimeSec;
	public bool isStarted = false;

	void Start()
	{
		goalText.enabled = false;
		startTime = DateTime.Now;
		nextButton.SetActive(false);
		isStarted = false;
		//twitterButton.SetActive(false);
		displayTime = "00:00:000";
	}

	public void GameStart()
	{
		startTime = DateTime.Now;
		isStarted = true;
	}

	void Update()
	{
		if ( isGoaled == false && isStarted ) {
			nowTime = DateTime.Now - startTime;

			displayTime = String.Format("{0:00}:{1:00}:{2:000}", 
				nowTime.Minutes, 
				nowTime.Seconds, 
				nowTime.Milliseconds);
			if ( nowTime.Minutes >= 1 ) {
				goalTimeSec = 60;
			} else {
				goalTimeSec = nowTime.Seconds;
			}
			timerText.text = displayTime;
		}
	}

	private PlayerController ball;

	void OnTriggerEnter(Collider other) {
		if ( other.CompareTag("Player") ) {
			if ( isGoaled == false ) {
				isGoaled = true;
				goalText.enabled = true;

				// ゴールした瞬間は無重量に
				playerRb = other.GetComponent<Rigidbody>();
				playerRb.useGravity = false;

				// 操作もできなくしよう
				ball = other.GetComponent<PlayerController>();
				ball.enabled = false;

				// 時間差で減速
				StartCoroutine( SlowPlayer() );
				// 時間差でインフォ表示
				StartCoroutine( GoNextStage() );

				bgm.Stop();
				goalJingle.Play();

				// CustomEvent を作る
				// TODO:
			}
		}
	}

	IEnumerator SlowPlayer()
	{
		while( playerRb.velocity.magnitude > 0.1f ) {
			Vector3 vel = playerRb.velocity;
			vel.Normalize();
			playerRb.AddForce(vel * -1.0f * 600.0f * Time.deltaTime);
			yield return null;
		}
		playerRb.velocity = Vector3.zero;
	}


	IEnumerator GoNextStage()
	{
		yield return new WaitForSeconds(3.0f);
		nextButton.SetActive(true);
		//twitterButton.SetActive(true);
	}

	public void Tweet()
	{
		Application.OpenURL("http://twitter.com/intent/tweet?text=" + 
			WWW.EscapeURL("ユニティちゃんボウルロウルで Time:" + displayTime + 
				" " + ball.countText.text + " 出しました https://build.cloud.unity3d.com/share/-kP0o8Piy-/webplayer/"));
	}

	public void NextStage()
	{
		int nowSceneId = SceneManager.GetActiveScene().buildIndex;
		//SceneManager.UnloadScene( nowSceneId );
		nowSceneId = nowSceneId + 1;
		if ( nowSceneId >= SceneManager.sceneCountInBuildSettings ) {
			nowSceneId = 0;
		}
		SceneManager.LoadScene( nowSceneId );
	}
}
