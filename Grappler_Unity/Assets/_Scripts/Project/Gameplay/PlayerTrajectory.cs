using UnityEngine;
using System.Collections;

public class PlayerTrajectory : MonoBehaviour {
	[SerializeField] private Rigidbody2D rigid;

	private TrajectoryPredictor tp;

	public void Show() {
		tp.drawDebugOnPrediction = true;
	}

	public void Hide() {
		tp.drawDebugOnPrediction = false;
	}

	private void Start(){
		tp = GetComponent<TrajectoryPredictor>();
		Hide();
	}

	private bool IsShowing() {
		return tp.drawDebugOnPrediction;
	}

	private void Update () {
		if (!IsShowing()) return;

		tp.debugLineDuration = Time.unscaledDeltaTime;
		tp.Predict2D(rigid.transform.position, rigid.velocity, Physics2D.gravity);
	}
}
