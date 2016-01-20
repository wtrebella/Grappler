using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class PlayerStateController : MonoBehaviour {
	protected Player player;

	private void Awake() {
		player = GetComponent<Player>();
	}
	
	public virtual void EnterState() {
		
	}
	
	public virtual void ExitState() {
		
	}
	
	public virtual void UpdateState() {
		
	}
	
	public virtual void FixedUpdateState() {
		
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
}
