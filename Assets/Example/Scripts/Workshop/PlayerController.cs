using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	public float speed;
	[SerializeField]
	public int count;
	[SerializeField]
	public Text countText;
	private AudioSource pickSnd;
	private int totalItem;
	public bool isComplete;
	public float friction = 0.01f;
	private float moveHorizontal;
	private float moveVertical;
	private ScoreManager scoreManager;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		count= 0;

		int score = 0;
		GameObject go = GameObject.Find("CoinNumUI");
		Assert.IsNotNull( go );
		scoreManager = go.GetComponent<ScoreManager>();

		GameObject go2 = GameObject.Find("Count Text");
		Assert.IsNotNull( go2 );
		countText = go.GetComponent<Text>();
		Assert.IsNotNull( countText );

		pickSnd = GetComponent<AudioSource>();
		Assert.IsNotNull( pickSnd );

		isComplete = false;
		totalItem = GameObject.FindGameObjectsWithTag("MyItem").Length;
		SetCountText();
	}

	void Update()
	{
		// Input系はUpdateで
		moveHorizontal = Input.GetAxis("Horizontal");
		moveVertical = Input.GetAxis("Vertical");
	}

	void FixedUpdate()
	{
		// 実際に動かすのはFixedUpdateで
		Vector3 movement = new Vector3( moveHorizontal, 0.0f, moveVertical );

		Vector3 vel = rb.velocity;

		// 動いている間は摩擦力を増やす
		if ( vel.magnitude > 0.01f ) {
			vel.Normalize();
			rb.AddForce(vel * -1.0f * friction);
		}

		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		// アイテムゲット
		if ( other.gameObject.CompareTag("MyItem") ) {
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText();
			int coinNum;
			if ( PlayerPrefs.HasKey("CoinNum") ) {
				coinNum = PlayerPrefs.GetInt("CoinNum");
			} else {
				coinNum = 0;
			}
			coinNum++;
			PlayerPrefs.SetInt("CoinNum", coinNum );
			scoreManager.UpdateCoin();

			// サウンド発生
			if ( pickSnd ) pickSnd.Play();
			// アイテム数が最初に数えた数ならばコンプリート
			if ( count == totalItem ) isComplete = true;
		}
	}

	void SetCountText()
	{
		countText.text = "Item:" + count.ToString() + "/" + totalItem;
	}
}