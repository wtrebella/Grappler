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
	
	public virtual void HandleCollision(Rigidbody2D rigid, Collision2D collision) {

	}

	public virtual void HandleTrigger(Rigidbody2D rigid, Collider2D collider) {

	}

	protected float GetCollisionAngle(Rigidbody2D rigid, Collision2D collision) {
		Vector3 normal = collision.contacts[0].normal;
		Vector3 vel = rigid.velocity;
		float angle = 90 - Vector3.Angle(vel, -normal);
		return angle;
	}

	protected float GetCollisionSpeed(Collision2D collision) {
		return collision.relativeVelocity.magnitude;
	}

	protected void ShakeScreen(Rigidbody2D rigid, Collision2D collision) {
		float speed = GetCollisionSpeed(collision);
		float angle = GetCollisionAngle(rigid, collision);
		ScreenShaker.instance.CollisionShake(speed, angle);
	}
}
