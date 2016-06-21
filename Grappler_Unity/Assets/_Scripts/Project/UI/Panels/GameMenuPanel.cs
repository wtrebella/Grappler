using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameMenuPanel : ModularPanel {
	[SerializeField] private Animator[] animators;

	private void Awake() {

	}
	
	private void Start() {
	
	}
	
	private void Update() {
	
	}

	protected override IEnumerator ShowSubroutine() {
		foreach (Animator animator in animators) {
			animator.SetBool("isShowing", true);
			yield return new WaitForSeconds(0.03f);
		}
		yield return new WaitForSeconds(0.5f);
	}

	protected override IEnumerator HideSubroutine() {
		foreach (Animator animator in animators) {
			animator.SetBool("isShowing", false);
			yield return new WaitForSeconds(0.03f);
		}
		yield return new WaitForSeconds(0.5f);
	}

	public void OnPlayButtonClicked() {
		GameSceneManager.instance.GoToGameplayState();
	}
}
