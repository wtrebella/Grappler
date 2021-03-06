﻿using UnityEngine;
using System.Collections;
using WhitDataTypes;

public class FirTree : GeneratableItem {
	[SerializeField] private FloatRange xScaleRange = new FloatRange(0.7f, 1.0f);
	[SerializeField] private FloatRange yScaleRange = new FloatRange(0.7f, 1.0f);

	private void Awake() {
		transform.localScale = new Vector3(xScaleRange.GetRandom(), yScaleRange.GetRandom(), 1.0f);
	}
}
