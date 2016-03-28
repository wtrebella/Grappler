using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class GameStats : ScriptableObjectSingleton<GameStats> {
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/GameStatsAsset", false, 103)]
	public static void CreateGameStatsAsset() {
		ScriptableObjectUtility.CreateAsset<GameStats>("GameStats");
	}
	#endif

	private string coinCountKey = "GameStats_CoinCount";
	private int _coinCount = 0;
	public int coinCount {
		get {
			_coinCount = WhitPrefs.GetInt(coinCountKey, 0);
			return _coinCount;
		}
		set {
			_coinCount = value;
			WhitPrefs.SetInt(coinCountKey, _coinCount);
		}
	}

	public static void OnCoinCollected() {
		instance.coinCount++;
	}
}
