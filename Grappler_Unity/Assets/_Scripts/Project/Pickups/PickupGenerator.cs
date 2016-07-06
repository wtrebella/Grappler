using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using WhitDataTypes;

public class PickupGenerator : ItemBetweenTerrainPairGenerator {
	[SerializeField] private float probability = 0.1f;

	protected override void OnPatternAdded(FloatRange distRange) {
		if (UnityEngine.Random.value > probability) return;

		float distPercent = UnityEngine.Random.Range(0.05f, 0.95f);
		float dist = distRange.Lerp(distPercent);
		float verticalPercent = UnityEngine.Random.Range(0.1f, 0.9f);
		GenerateItem(dist, verticalPercent);
	}
}