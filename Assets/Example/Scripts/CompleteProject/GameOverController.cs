using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityAnalyticsHeatmap;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace CompleteProject
{
	[RequireComponent(typeof(AudioSource))]
	public class GameOverController : MonoBehaviour {
		public GameObject gameOverPanel;
		public CameraController camCon;
		public GameObject iapPanel;

		void Start()
		{
			gameOverPanel.SetActive(false);
		}

		void OnTriggerEnter(Collider other) 
		{
			Assert.AreEqual<string>( other.gameObject.name, "Player");

			camCon.isLookAt = true;

			// ここでPositionTrackerをオフにする
			ExecuteEvents.Execute<IAnalyticsDispatcher>(
				target: other.gameObject, // 呼び出す対象のオブジェクト
				eventData: null,  // イベントデータ（モジュール等の情報）
				functor: (x,y)=>x.DisableAnalytics()); // 操作

			GameObject bgmObj = GameObject.Find("BGM");
			Assert.IsNotNull(bgmObj);
			AudioSource audio = bgmObj.GetComponent<AudioSource>();
			Assert.IsNotNull(audio);
			audio.Stop();

			this.GetComponent<AudioSource>().Play();

			StartCoroutine(LateOpenRetry());
		}

		IEnumerator LateOpenRetry()
		{
			yield return new WaitForSeconds(2f);
			gameOverPanel.SetActive(true);
		}

		public void ShowIAP()
		{
			iapPanel.SetActive(true);
			gameOverPanel.SetActive(false);
		}

		public void Retry()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		public void RetryWithEasyMode ()
		{
			GameModeController.setEasyMode ();
			Retry ();
		}
	}
}
