using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class CoinGenerator : ItemBetweenTerrainPairGenerator {
	[SerializeField] private float probability = 0.5f;
//	public Action OnCoinCollected;

	protected override void OnPatternAdded(FloatRange distRange) {
		if (UnityEngine.Random.value > probability) return;

		float distPercent = UnityEngine.Random.Range(0.05f, 0.95f);
		float dist = distRange.Lerp(distPercent);
		float verticalPercent = UnityEngine.Random.Range(0.1f, 0.9f);
		GenerateItem(dist, verticalPercent);
	}

	public void OnChildCoinCollected() {
//		WhitTools.Invoke(OnCoinCollected);
		GameStats.OnCoinCollected();
	}
}
