using UnityEngine;
using System.Collections;

public class PlayerBodyPart : MonoBehaviour {
	[SerializeField] private Player player;

	private void OnCollisionEnter2D(Collision2D collision) {
		player.HandleCollision(collision);
	}

	private void Awake() {

	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
