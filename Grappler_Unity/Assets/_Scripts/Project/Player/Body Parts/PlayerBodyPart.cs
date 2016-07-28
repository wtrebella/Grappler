using UnityEngine;
using System.Collections;

public class PlayerBodyPart : MonoBehaviour {
	public Rigidbody2D rigid {get; private set;}

	private Player player;
	
	public bool IsBelowScreen() {
		float margin = -5;
		float minY = ScreenUtility.instance.lowerLeft.y + margin;
		return transform.position.y < minY;
	}

	public void SetKinematic() {
		rigid.isKinematic = true;
	}

	public void SetNonKinematic() {
		rigid.isKinematic = false;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		player.collisionHandler.HandleCollisionEnter(this, collision);
	}

	private void OnCollisionStay2D(Collision2D collision) {
		player.collisionHandler.HandleCollisionStay(this, collision);
	}

	private void OnCollisionExit2D(Collision2D collision) {
		player.collisionHandler.HandleCollisionExit(this, collision);
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		player.collisionHandler.HandleTriggerEnter(this, collider);
	}

	private void OnTriggerStay2D(Collider2D collider) {
		player.collisionHandler.HandleTriggerStay(this, collider);
	}

	private void OnTriggerExit2D(Collider2D collider) {
		player.collisionHandler.HandleTriggerExit(this, collider);
	}

	private void Awake() {
		rigid = GetComponent<Rigidbody2D>();
		player = GetComponentInParent<Player>();
	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
