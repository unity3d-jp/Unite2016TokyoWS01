using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

// IStoreListener を継承うることでUnity Purchasing からメッセージを取得できる
public class Purchaser : MonoBehaviour, IStoreListener
{
	private static IStoreController m_StoreController;              // Purchasing システムの参照
	private static IExtensionProvider m_StoreExtensionProvider;     // 拡張した場合のPurchasing サブシステムの参照

	//@Angelo: Here are some details on how you create your identifiers. The idea is that you create a general identifier and purchase THAT identifier, which Unity's IAP will convert to
	// the identifier of the relevant store (e.g.: if I purchase a "consumable" on iOS, Unity's IAP will convert it to com.unity3d.test.services.purchasing.consumable before sending the data to
	// the app store. So a developer could use this class as it is being used in the game and simply switch the Product ID's below.
	// ここではどうやってIDを生成するか書いてある。
	// StoreのIDがUnity IAPでいうところのIDで何にあたるかということを決めている
	// つまり、IOS で"consumable"を購入するとした際、Unity IAPではちゃんと"test.services.purchasing.consumabl"と変換して送ってくれる
	// なので開発者はどのストアであっても気にせず同一の製品IDでやり取りできる

	// Product identifiers for all products capable of being purchased: "convenience" general identifiers for use with Purchasing, and their store-specific identifier counterparts 
	// for use with and outside of Unity Purchasing. Define store-specific identifiers also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)​
	// 製品IDは全ての購入で利用可能な、便利な汎用IDであり、各ストア特有IDは反対にUnity Purchaseing以外のIDである
	// 各ストア特有IDは各プラットフォームのダッシュボードで設定すること
	private static string kProductIDConsumable =    "consumable";                                                     // 消費型製品の汎用ID
	private static string kProductIDNonConsumable = "nonconsumable";                                                  // 非消費型製品の汎用ID
	private static string kProductIDSubscription =  "subscription";                                                   // 定期購読製品の汎用ID

	private static string kProductNameAppleConsumable =    "com.unity3d.test.services.purchasing.consumable";         // Apple App Store identifier for the consumable product.
	private static string kProductNameAppleNonConsumable = "com.unity3d.test.services.purchasing.nonconsumable";      // Apple App Store identifier for the non-consumable product.
	private static string kProductNameAppleSubscription =  "com.unity3d.test.services.purchasing.subscription";       // Apple App Store identifier for the subscription product.

	private static string kProductNameGooglePlayConsumable =    "com.unity3d.test.services.purchasing.consumable";    // Google Play Store identifier for the consumable product.
	private static string kProductNameGooglePlayNonConsumable = "com.unity3d.test.services.purchasing.nonconsumable"; // Google Play Store identifier for the non-consumable product.
	private static string kProductNameGooglePlaySubscription =  "com.unity3d.test.services.purchasing.subscription";  // Google Play Store identifier for the subscription product.

	void Awake()
	{
		// If we haven't set up the Unity Purchasing reference
		// Unity Purchasing 参照が設定されてなければ...
		if (m_StoreController == null)
		{
			// Begin to configure our connection to Purchasing
			// Purchasing へつなげる初期設定
			InitializePurchasing();
		}
	}

	public void InitializePurchasing() 
	{
		// If we have already connected to Purchasing ...
		// Purchasing へ既に繋がっているんだったら...
		if (IsInitialized())
		{
			// ... we are done here.
			// ここは何もしない
			return;
		}

		var module = StandardPurchasingModule.Instance();
		module.useMockBillingSystem = true; // Microsoft
		// The FakeStore supports: no-ui (always succeeding), basic ui (purchase pass/fail), and 
		// developer ui (initialization, purchase, failure code setting). These correspond to 
		// the FakeStoreUIMode Enum values passed into StandardPurchasingModule.useFakeStoreUIMode.
		// フェイクStoreは以下のことをサポートしている： UIなし（いつも成功する）、基本的なUI（購入が成功/失敗）
		// 開発者向けUI（初期化、購入、失敗時の設定）。　これらは FakeStoreUIMode の Enum値と対応している
		module.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;

		// Create a builder, first passing in a suite of Unity provided stores.
		// builder を生成し、Unity謹製ストアの最初の部分をパスする
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		// Add a product to sell / restore by way of its identifier, associating the general identifier with its store-specific identifiers.
		// 販売・リストアのために製品IDを追加。汎用IDとストア特有IDとを結びつけるために必要
		builder.AddProduct(kProductIDConsumable, ProductType.Consumable, new IDs()
			{
				{ kProductNameAppleConsumable,       AppleAppStore.Name },
				{ kProductNameGooglePlayConsumable,  GooglePlay.Name }
			});
		// Continue adding the non-consumable product.
		builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable, new IDs()
			{
				{ kProductNameAppleNonConsumable,       AppleAppStore.Name },
				{ kProductNameGooglePlayNonConsumable,  GooglePlay.Name }
			});
		// And finish adding the subscription product.
		builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs()
			{
				{ kProductNameAppleSubscription,       AppleAppStore.Name },
				{ kProductNameGooglePlaySubscription,  GooglePlay.Name }
			});
		// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
		// ここまでで設定したものとこのクラスのインスタンスを引数として、非同期呼び出しで設定して開始。
		// その後、OnInitialized か OnInitializeFailedが呼び出されるはず。
		UnityPurchasing.Initialize(this, builder);
	}


	private bool IsInitialized()
	{
		// Only say we are initialized if both the Purchasing references are set.
		// 二つのPurchasing の参照が設定されていれば、初期設定されていると言える
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

	//@Angelo: See that regardless of what you're purchasing, the process is the same, calling the BuyProductID function and passing the product ID as parameter.
	// 何を購入したかに関わらず、プロセスは同じということがわかるだろう。BuyProductID関数を製品IDを引数にいれて呼んでいるだけである。
	public void BuyConsumable()
	{
		// Buy the consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		// 汎用IDを使って消費型アイテムを買っている。非同期でProcessPurchase か OnPurchaseFailedが呼ばれるはずである。
		BuyProductID(kProductIDConsumable);
	}


	public void BuyNonConsumable()
	{
		// Buy the non-consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		// 汎用IDを使って非消費型アイテムを買っている。非同期でProcessPurchase か OnPurchaseFailedが呼ばれるはずである。
		BuyProductID(kProductIDNonConsumable);
	}


	public void BuySubscription()
	{
		// Buy the subscription product using its the general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDSubscription);
	}


	public void BuyProductID(string productId)
	{
		// If the stores throw an unexpected exception, use try..catch to protect my logic here.
		// ストアの中で例外が出れば、try catch を使って、ここのロジックでキャッチできる
		try
		{
			// If Purchasing has been initialized ...
			// 購入が初期化されていれば
			if (IsInitialized())
			{
				// ... look up the Product reference with the general product identifier and the Purchasing system's products collection.
				// ... 汎用製品IDと、購入システムの製品群からProduct参照を取得する。
				Product product = m_StoreController.products.WithID(productId);

				// If the look up found a product for this device's store and that product is ready to be sold ... 
				// もしデバイスのストアで製品が見つかったら、販売される用意が出来たということになる
				if (product != null && product.availableToPurchase)
				{
					// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
					// ... 製品を買う。非同期で ProcessPurchase か OnPurchaseFailed の呼び出しの反応がある
					Debug.Log (string.Format("Purchasing product asychronously: '{0}' - '{1}'", product.definition.id, product.definition.storeSpecificId));
					m_StoreController.InitiatePurchase(product);
				}
				// Otherwise ...
				else
				{
					// ... report the product look-up failure situation  
					// ... さもなければ、失敗シチュエーションのレポート
					Debug.Log ("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				}
			}
			// Otherwise ...
			else
			{
				// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or retrying initiailization.
				// ... Purchasing で初期化が成功していない事実をレポートする。本来はもう少し待つか、初期化リトライを考慮するべき
				Debug.Log("BuyProductID FAIL. Not initialized.");
			}
		}
		// Complete the unexpected exception handling ...
		// 例外ハンドリング
		catch (Exception e)
		{
			// ... by reporting any unexpected exception for later diagnosis.
			// ... あとで分析で例外をレポート
			Debug.Log ("BuyProductID: FAIL. Exception during purchase. " + e);
		}
	}


	// Restore purchases previously made by this customer. Some platforms automatically restore purchases. Apple currently requires explicit purchase restoration for IAP.
	// 以前買ったことがある商品のリストア処理。いくつかのプラットフォームは自動的にリストアする。AppleはIAPの中で明示的に購入リストアを要求している
	public void RestorePurchases()
	{
		// If Purchasing has not yet been set up ...
		if (!IsInitialized())
		{
			// ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}

		// If we are running on an Apple device ... 
		// Apple なら
		if (Application.platform == RuntimePlatform.IPhonePlayer || 
			Application.platform == RuntimePlatform.OSXPlayer)
		{
			// ... begin restoring purchases
			// リストア開始
			Debug.Log("RestorePurchases started ...");

			// Fetch the Apple store-specific subsystem.
			// Apple特有のサブシステムを取ってくる
			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			// Begin the asynchronous process of restoring purchases. Expect a confirmation response in the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
			// 購入リストアの非同期処理を開始。下記のAction<bool> の中で、確認プロセスが呼び出される。もし以前買っているのならProcessPurchaseが呼び出される
			apple.RestoreTransactions((result) => {
				// The first phase of restoration. If no more responses are received on ProcessPurchase then no purchases are available to be restored.
				// リストア処理の最初のフェーズ。もし、ProcessPurchaseで反応がないのなら、リストアするべき購入はなかったということ
				Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
		// Otherwise ...
		else
		{
			// We are not running on an Apple device. No work is necessary to restore purchases.
			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}


	//  
	// --- IStoreListener
	//

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		// Purchasing は初期化に成功した。Purchasing 参照を取っておく
		Debug.Log("OnInitialized: PASS");

		// Overall Purchasing system, configured with products for this application.
		// 全体のPurchasingシステム。このアプリケーションの製品を構成している。
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		// ストア特有のサブシステム。デバイス特有のストアへのアクセス等ができる。
		m_StoreExtensionProvider = extensions;
	}


	public void OnInitializeFailed(InitializationFailureReason error)
	{
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		// 初期化失敗。
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}


	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{
		//@Angelo: here is the result of a successful purchase of a consumable (100 coins, in our case), in the Editor once you press the "Buy" button you'll immediately see the
		//"ProcessPurchase: PASS" message on the Console. On a device you would see the OS pop-up confirming the purchase
		// A consumable product has been purchased by this user.
		// 消費型の購入が成功した結果でここにくる。
		// エディタでは、「Buy」と押すと、すぐにコンソールに「ProcessPurchase: PASS」と表示される。
		// 本来デバイスでは、ポップアップで購入確認ウィンドウが出る。
		if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 100 coins to the player's in-game score.ScoreManager.score += 100;
			// ここに消費アイテムを買った時の処理を入れる
			// TODO: 
			
		}

		//@Angelo: Same here for a non-consumable (in our chase, the old lady character)
		// Or ... a non-consumable product has been purchased by this user.
		// ここも非消費型での同じ感じ
		else if (String.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			// ここに非消費アイテムを買った時の処理を入れる
			// TODO: 
		}// Or ... a subscription product has been purchased by this user.
		else if (String.Equals(args.purchasedProduct.definition.id, kProductIDSubscription, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));}// Or ... an unknown product has been purchased by this user. Fill in additional products here.
		else 
		{
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));}// Return a flag indicating wither this product has completely been received, or if the application needs to be reminded of this purchase at next app launch. Is useful when saving purchased products to the cloud, and when that save is delayed.
		return PurchaseProcessingResult.Complete;
	}


	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing this reason with the user.
		// 製品購入が成功しなかった。詳しい情報はfailureReasonをチェック。ユーザーに失敗の理由はシェアした方がいい
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}",product.definition.storeSpecificId, failureReason));
	}
}