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

	public static int coinCount {get {return instance._coinCount;}}

	[SerializeField, HideInInspector] private int _coinCount = 0;

	public static void OnCoinCollected() {
		instance._coinCount++;
	}
}
