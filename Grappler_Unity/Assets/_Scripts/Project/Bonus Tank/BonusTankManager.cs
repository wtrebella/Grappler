using UnityEngine;
using System.Collections;

public class BonusTankManager : MonoBehaviour {
	public static BonusTankManager instance;

	[SerializeField] private CollisionSignaler collisionSignaler;
	[SerializeField] private FloatRange jumpDistanceRange = new FloatRange(0, 100);
	[SerializeField] private BonusTank bonusTank;

	private void Awake() {
		instance = this;
		collisionSignaler.SignalCollision += OnCollision;
	}

	public void ReportJumpDistance(float jumpDistance) {
		float jumpPercent = jumpDistanceRange.GetPercent(jumpDistance);
		bonusTank.Add((int)(0.25f * jumpPercent * 100.0f));
	}

	private void OnCollision() {
		bonusTank.Empty();
	}
}
