using UnityEngine;
using System.Collections;
using System;

public class WhitStateController : MonoBehaviour {
	public float timeInState {get; private set;}
	public float fixedTimeInState {get; private set;}

	[HideInInspector] public Enum state;

	private void Awake() {
		BaseAwake();
	}

	protected virtual void BaseAwake() {
		timeInState = 0;
		fixedTimeInState = 0;
	}
	
	public virtual void EnterState() {
		timeInState = 0;
		fixedTimeInState = 0;
	}
	
	public virtual void ExitState() {
		
	}
	
	public virtual void UpdateState() {
		timeInState += Time.deltaTime;
	}
	
	public virtual void FixedUpdateState() {
		fixedTimeInState += Time.fixedDeltaTime;
	}
	
	public virtual void LeftSwipe() {
		
	}
	
	public virtual void RightSwipe() {
		
	}
	
	public virtual void UpSwipe() {
		
	}
	
	public virtual void DownSwipe() {
		
	}
	
	public virtual void Tap() {
		
	}

	public virtual void TouchUp() {
		
	}

	public virtual void TouchDown() {
		
	}

	public virtual void LeftTouchDown() {
		
	}
	
	public virtual void LeftTouchUp() {
		
	}
	
	public virtual void RightTouchDown() {
		
	}
	
	public virtual void RightTouchUp() {
		
	}
}
