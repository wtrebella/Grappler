using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PanelBase : MonoBehaviour {
	public Action<PanelBase> SignalPanelHidden;

	public virtual void Show() {
		gameObject.SetActive(true);
	}

	public virtual void Hide() {

	}

	public virtual void SetPanelInfo(PanelInfo panelInfo) {

	}

	protected virtual void OnHidden() {
		gameObject.SetActive(false);
		if (SignalPanelHidden != null) SignalPanelHidden(this);
	}
}
