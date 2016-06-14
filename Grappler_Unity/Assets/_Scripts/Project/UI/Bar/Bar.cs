using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Bar : MonoBehaviour {
	public bool isShowing {get; private set;}

	public Action<Bar> SignalShown;
	public Action<Bar> SignalHidden;

	[SerializeField] protected Image backgroundImage;

	[SerializeField] private Animator animator;
	[SerializeField] private Text barText;
	[SerializeField] private float moveDelay = 0.1f;

	private void Awake() {
		isShowing = false;
	}

	public virtual void SetColor(Color color) {
		throw new NotImplementedException();
	}

	public void SetText(string text) {
		barText.text = text;
	}

	public void Show(int priority) {
		StartCoroutine(ShowRoutine(priority));
	}

	public void Hide(int priority) {
		StartCoroutine(HideRoutine(priority));
	}

	private IEnumerator ShowRoutine(int priority) {
		yield return new WaitForSeconds(priority * moveDelay);
		isShowing = true;
		animator.SetBool("isShowing", true);
	}

	private IEnumerator HideRoutine(int priority) {
		yield return new WaitForSeconds(priority * moveDelay);
		animator.SetBool("isShowing", false);
	}

	public bool IsShowing() {
		return animator.GetBool("isShowing");
	}

	public void OnShown() {
		if (SignalShown != null) SignalShown(this);
	}

	public void OnHidden() {
		isShowing = false;
		if (SignalHidden != null) SignalHidden(this);
	}
}
