using UnityEngine;
using System.Collections;

public class SlomoEffect : PickupEffect {
	[SerializeField] private float slowTimeScale = 0.5f;

	protected override void Initialize() {
		pickupType = PickupEffectType.Slomo;
	}

	protected override void ImplementEffect() {
		WhitTools.SetTimeScale(slowTimeScale);
	}

	protected override void RemoveEffect() {
		WhitTools.SetTimeScale(1.0f);
	}
}