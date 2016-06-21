using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonBar : Bar {
	[SerializeField] private Image icon;
	[SerializeField] private Image buttonImage;

	public override void ApplyBarInfo(BarInfo barInfo) {
		base.ApplyBarInfo(barInfo);

	}

	private void Awake() {

	}
	
	private void Start() {
	
	}
	
	private void Update() {
	
	}

	public void OnClickButton() {
		if (barInfo == null) {
			Debug.LogError("no bar info applied!");
			return;
		}

		if (barInfo.buttonDelegate == null) {
			Debug.LogError("no button delegate set!");
			return;
		}

		barInfo.buttonDelegate(this);
	}

	public override void SetColor(Color color) {
		buttonImage.color = color;
	}

	public void SetIconSprite(Sprite sprite) {
		icon.sprite = sprite;
	}
}
