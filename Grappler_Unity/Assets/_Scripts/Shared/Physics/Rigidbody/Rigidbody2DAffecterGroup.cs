using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rigidbody2DAffecterGroup : MonoBehaviour {
	public Rigidbody2D[] rigidbodies;

	private Rigidbody2DAffecter[] affecters;

	public T GetAffecter<T>() where T : Rigidbody2DAffecter {
		if (affecters == null) affecters = GetComponentsInChildren<Rigidbody2DAffecter>();

		foreach (Rigidbody2DAffecter affecter in affecters) {
			if (affecter.GetType() == typeof(T)) return affecter as T;
		}

		Debug.LogError("no affecter of type: " + typeof(T).ToString());
		return null;
	}

	private Rigidbody2DStopper _stopper;
	public Rigidbody2DStopper stopper {
		get {
			if (_stopper == null) _stopper = GetAffecter<Rigidbody2DStopper>();
			if (_stopper == null) Debug.LogError("no Rigidbody2DStopper attached!");
			return _stopper;
		}
	}

	private Rigidbody2DVelocityReducer _velocityReducer;
	public Rigidbody2DVelocityReducer velocityReducer {
		get {
			if (_velocityReducer == null) _velocityReducer = GetAffecter<Rigidbody2DVelocityReducer>();
			if (_velocityReducer == null) Debug.LogError("no Rigidbody2DVelocityReducer attached!");
			return _velocityReducer;
		}
	}

	private Rigidbody2DForcer _forcer;
	public Rigidbody2DForcer forcer {
		get {
			if (_forcer == null) _forcer = GetAffecter<Rigidbody2DForcer>();
			if (_forcer == null) Debug.LogError("no Rigidbody2DForcer attached!");
			return _forcer;
		}
	}

	public void SetKinematic() {
		foreach (Rigidbody2D rigid in rigidbodies) rigid.isKinematic = true;
	}

	public void SetNonKinematic() {
		foreach (Rigidbody2D rigid in rigidbodies) rigid.isKinematic = false;
	}

	public void AllowMovement() {
		stopper.Cancel();
	}

	public void StopMoving() {
		stopper.StartStoppingProcess();
	}

	public void ReduceVelocity(float reductionMultiplier) {
		velocityReducer.Reduce(reductionMultiplier);
	}

	public void AddForce(Vector2 force, ForceMode2D forceMode) {
		forcer.AddForce(force, forceMode);
	}

	public void SetXVelocity(float x) {
		forcer.SetXVelocity(x);	
	}

	public void SetYVelocity(float y) {
		forcer.SetYVelocity(y);	
	}

	public void SetVelocity(Vector2 velocity) {
		forcer.SetVelocity(velocity);
	}

	public void AddVelocity(Vector2 velocity) {
		forcer.AddVelocity(velocity);	
	}

	public void AddTorque(float torque) {
		forcer.AddTorque(torque);	
	}

	public Vector2 GetAverageDirection() {
		return forcer.GetAverageDirection();
	}
}
