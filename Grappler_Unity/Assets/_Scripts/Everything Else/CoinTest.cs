using UnityEngine;
using System.Collections;

public class CoinTest : MonoBehaviour {
	public void OnCoinCollected() {
		Debug.Log("coin collected! total coins: " + GameStats.coinCount);
	}
}
