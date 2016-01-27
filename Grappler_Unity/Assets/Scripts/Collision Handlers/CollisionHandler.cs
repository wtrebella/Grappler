using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class CollisionHandler : MonoBehaviour {
	public LayerMask layer;

	protected Player player;

	protected void BaseAwake() {
		player = GetComponent<Player>();
	}

	private void Awake() {
		BaseAwake();
	}
	
	public virtual void HandleCollision(Collision2D collision) {

	}
}
