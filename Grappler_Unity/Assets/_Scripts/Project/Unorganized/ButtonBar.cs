using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonBar : Bar {
	[SerializeField] private Image icon;
	[SerializeField] private Image buttonImage;

	private void Awake() {

	}
	
	private void Start() {
	
	}
	
	private void Update() {
	
	}

	public override void SetColor(Color color) {
		buttonImage.color = color;
	}

	public void SetIconSprite(Sprite sprite) {
		icon.sprite = sprite;
	}
}
