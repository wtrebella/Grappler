using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	public static ScoreManager instance;

	[SerializeField] private CollisionSignaler collisionSignaler;

	private void Awake() {
		instance = this;
		collisionSignaler.SignalCollision += OnCollision;
	}

	public void ReportJumpDistance(float jumpDistance) {

	}

	public void ReportCollision() {

	}

	private void OnCollision() {
		ReportCollision();
	}
}
