using UnityEngine;
using System.Collections;
using WhitDataTypes;

public class BonusTankManager : MonoBehaviour {
	public static BonusTankManager instance;

	[SerializeField] private CollisionSignaler collisionSignaler;
	[SerializeField] private FloatRange jumpDistanceRange = new FloatRange(0, 100);
	[SerializeField] private float multiplier = 0.5f;
	[SerializeField] private BonusTank bonusTank;

	private void Awake() {
		instance = this;
		collisionSignaler.SignalCollision += OnCollision;
	}

	public void ReportJumpDistance(float jumpDistance) {
		float jumpPercent = jumpDistanceRange.GetPercent(jumpDistance);
		bonusTank.Add((int)(multiplier * jumpPercent * 100.0f));
	}

	private void OnCollision() {
		bonusTank.Empty();
	}
}
