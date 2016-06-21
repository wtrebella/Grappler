using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class GameStats : MonoBehaviour {
	public static GameStats instance;

	public Action SignalCoinCollected;
	public Action SignalCoinCountChanged;

	[SerializeField] private int giftCost = 1000;

	private void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy(gameObject);
		}
	}

	private string coinCountKey = "GameStats_CoinCount";
	private int _coinCount = 0;
	public int coinCount {
		get {
			_coinCount = WhitPrefs.GetInt(coinCountKey, 0);
			return _coinCount;
		}
		private set {
			if (_coinCount == value) return;

			_coinCount = value;
			WhitPrefs.SetInt(coinCountKey, _coinCount);
			if (SignalCoinCountChanged != null) SignalCoinCountChanged();
		}
	}

	public void OnCoinCollected() {
		instance.coinCount++;
		if (SignalCoinCollected != null) SignalCoinCollected();
	}

	public int CoinsToGoUntilFreeGift() {
		return Mathf.Max(0, giftCost - coinCount);
	}

	public bool CanAffordGift() {
		return coinCount >= giftCost;
	}

	public void HitTree() {

	}

	public void HitIcicle() {

	}
}