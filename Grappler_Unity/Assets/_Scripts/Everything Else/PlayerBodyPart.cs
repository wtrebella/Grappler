﻿using UnityEngine;
using System.Collections;

public class PlayerBodyPart : MonoBehaviour {
	public Rigidbody2D rigid {get; private set;}

	[SerializeField] private Player player;
	
	public bool IsBelowScreen() {
		float margin = -5;
		float minY = GameScreen.instance.lowerLeft.y + margin;
		return transform.position.y < minY;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		player.HandleCollisionEnter(this, collision);
	}

	private void OnCollisionStay2D(Collision2D collision) {
		player.HandleCollisionStay(this, collision);
	}

	private void OnCollisionExit2D(Collision2D collision) {
		player.HandleCollisionExit(this, collision);
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		player.HandleTriggerEnter(this, collider);
	}

	private void OnTriggerStay2D(Collider2D collider) {
		player.HandleTriggerEnter(this, collider);
	}

	private void OnTriggerExit2D(Collider2D collider) {
		player.HandleTriggerEnter(this, collider);
	}

	private void Awake() {
		rigid = GetComponent<Rigidbody2D>();
	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}