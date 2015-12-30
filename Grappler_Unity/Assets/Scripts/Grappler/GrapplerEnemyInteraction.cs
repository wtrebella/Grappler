using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class GrapplerEnemyInteraction : MonoBehaviour {
	public Action<MountainEnemy> SignalHitEnemy;
	
	private Rigidbody2D rigid;

	private void Awake() {
		rigid = GetComponent<Rigidbody2D>();
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		MountainEnemy enemy = collider.GetComponent<MountainEnemy>();
		if (enemy) {
			ApplyForce(enemy.GetForceStrength());
			ApplyTorque(enemy.GetTorqueStrength());
			if (SignalHitEnemy != null) SignalHitEnemy(enemy);
		}
	}

	private void ApplyForce(float forceStrength) {
		Vector2 force = (new Vector2(UnityEngine.Random.value, -UnityEngine.Random.value)).normalized * forceStrength;
		rigid.AddForce(force, ForceMode2D.Impulse);
	}

	private void ApplyTorque(float torqueStrength) {
		EnableRotation();
		float torque = UnityEngine.Random.Range(-1.0f, 1.0f) * torqueStrength;
		rigid.AddTorque(torque);
	}

	private void EnableRotation() {
		rigid.constraints = RigidbodyConstraints2D.None;
	}
	
	private void DisableRotation() {
		rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
	}
}
