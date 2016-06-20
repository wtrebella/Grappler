using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Gift : MonoBehaviour {
	[SerializeField] private Animator animator;

	private void Awake() {

	}
	
	private void Start() {
		animator.SetTrigger("drop");
	}
	
	private void Update() {
	
	}

	public void OnHitGround() {
		ScreenShaker.instance.ShakeMax();
	}
}
