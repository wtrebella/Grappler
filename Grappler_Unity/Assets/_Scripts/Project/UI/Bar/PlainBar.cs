using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlainBar : Bar {

	public override void ApplyBarInfo(BarInfo barInfo) {
		base.ApplyBarInfo(barInfo);

	}

	public override void SetColor(Color color) {
		backgroundImage.color = color;
	}

	private void Awake() {

	}
	
	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
