using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Gift : MonoBehaviour {
	[SerializeField] private ParticleSystem particles;
	[SerializeField] private Animator animator;
	[SerializeField] private Animator itemAnimator;
	[SerializeField] private CollectionItemUISprite itemUISprite;

	public void Pop() {
		animator.SetTrigger("pop");
	}

	public void Drop() {
		animator.SetTrigger("drop");
	}

	public void SetCollectionItem(CollectionItem item) {
		itemUISprite.SetCollectionItem(item);
	}

	public void OnLidPoppedOff() {
		particles.Play();
		itemAnimator.SetTrigger("pop");
	}

	private void Awake() {

	}
	
	private void Start() {
		Drop();
	}
	
	private void Update() {
		if (Input.GetKeyDown(KeyCode.P)) Pop();
	}

	public void OnHitGround() {
		ScreenShaker.instance.ShakeMax();
	}
}
