using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TempSpeedText : MonoBehaviour {
	public Text text;
	public Rigidbody2D rigid;

	private void Awake() {

	}
	
	private void Start() {
	
	}
	
	private void FixedUpdate() {
		text.text = ((int)rigid.velocity.magnitude).ToString();
	}
}
