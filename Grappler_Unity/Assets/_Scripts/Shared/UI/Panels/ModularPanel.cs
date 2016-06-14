using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class ModularPanel : MonoBehaviour {
	public bool isShowing {get; private set;}

	public Action<ModularPanel> SignalShown;
	public Action<ModularPanel> SignalHidden;

	private void Awake() {
		isShowing = false;
	}

	public void Show() {
		if (isShowing) return;
		StartCoroutine(ShowRoutine());
	}
		
	public void Hide() {
		if (!isShowing) return;
		StartCoroutine(HideRoutine());
	}

	private IEnumerator ShowRoutine() {
		isShowing = true;
		gameObject.SetActive(true);
		yield return StartCoroutine(ShowSubroutine());
		OnShown();
	}

	private IEnumerator HideRoutine() {
		yield return StartCoroutine(HideSubroutine());
		OnHidden();
	}

	private void OnShown() {
		if (SignalShown != null) SignalShown(this);
	}

	private void OnHidden() {
		isShowing = false;
		gameObject.SetActive(false);
		if (SignalHidden != null) SignalHidden(this);
	}

	protected virtual IEnumerator ShowSubroutine() {

	}

	protected virtual IEnumerator HideSubroutine() {

	}

	public virtual void SetPanelInfo(PanelInfo panelInfo) {

	}
}