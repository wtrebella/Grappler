using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinsText : MonoBehaviour {
	[SerializeField] private Text coinsText;

	private void Awake() {
		UpdateCoinsText();
	}

	private void OnEnable() {
		if (GameStats.instance != null) {
			GameStats.instance.SignalCoinCountChanged += OnCoinCountChanged;
		}
	}

	private void OnDisable() {
		if (GameStats.instance != null) {
			GameStats.instance.SignalCoinCountChanged -= OnCoinCountChanged;
		}
	}

	private void OnCoinCountChanged() {
		UpdateCoinsText();
	}

	private void UpdateCoinsText() {
		coinsText.text = GetCoinsTextString();
	}

	private string GetCoinsTextString() {
		if (GameStats.instance == null) return "0";
		else return GameStats.instance.coinCount.ToString();
	}
}
