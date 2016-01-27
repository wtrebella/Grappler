using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class PlayerStateController : MonoBehaviour {
	public float timeInState {get; private set;}
	public float fixedTimeInState {get; private set;}

	protected Player player;

	public void BaseAwake() {
		player = GetComponent<Player>();
		timeInState = 0;
		fixedTimeInState = 0;
	}

	private void Awake() {
		BaseAwake();
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
	
	public virtual void HandleLeftSwipe() {
		
	}
	
	public virtual void HandleRightSwipe() {
		
	}
	
	public virtual void HandleUpSwipe() {
		
	}
	
	public virtual void HandleDownSwipe() {
		
	}
	
	public virtual void HandleTap() {
		
	}

	public virtual void HandleTouchUp() {
		
	}

	public virtual void HandleTouchDown() {
		
	}

	public virtual void HandleLeftTouchDown() {
		
	}
	
	public virtual void HandleLeftTouchUp() {
		
	}
	
	public virtual void HandleRightTouchDown() {
		
	}
	
	public virtual void HandleRightTouchUp() {
		
	}
}
