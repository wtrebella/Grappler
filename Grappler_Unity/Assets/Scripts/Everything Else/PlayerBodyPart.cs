using UnityEngine;
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
		player.HandleCollision(collision);
	}

	private void Awake() {
		rigid = GetComponent<Rigidbody2D>();
	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
