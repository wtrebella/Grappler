using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	public static ScoreManager instance;

	private void Awake() {
		instance = this;
	}

	public void ReportJumpDistance(float jumpDistance) {

	}

	public void ReportCollision() {

	}
}
