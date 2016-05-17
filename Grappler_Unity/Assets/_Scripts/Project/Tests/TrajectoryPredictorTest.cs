using UnityEngine;
using System.Collections;

public class TrajectoryPredictorTest : MonoBehaviour {
	[SerializeField] private Rigidbody2D rigid;

	private TrajectoryPredictor tp;

	private void Start(){
		tp = GetComponent<TrajectoryPredictor>();
	}

	private void Update () {
		tp.debugLineDuration = Time.unscaledDeltaTime;
		tp.Predict2D(rigid.transform.position, rigid.velocity, Physics2D.gravity);
	}
}
