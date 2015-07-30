using UnityEngine;
using System.Collections;

public enum DirectionType {Left, Right}

public class BulletShooter : MonoBehaviour {
	[SerializeField] private Bullet bulletPrefab;
	[SerializeField] private float minInterval;
	[SerializeField] private float maxInterval;
	[SerializeField] private float minImpulseForceMagnitude;
	[SerializeField] private float maxImpulseForceMagnitude;

	private void Start() {
		StartCoroutine(BulletLoop());
	}

	private IEnumerator BulletLoop() {
		while (true) {
			Shoot();
			yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
		}
	}

	private void Shoot() {
		Bullet bullet = bulletPrefab.Spawn();
		DirectionType directionType = DirectionType.Right;
		Vector2 force = GetImpulseForce(directionType);
		bullet.Shoot(directionType, force);
	}

	private Vector2 GetImpulseForce(DirectionType directionType) {
		Vector2 direction = GetDirection(directionType);
		float magnitude = Random.Range(minImpulseForceMagnitude, maxImpulseForceMagnitude);
		return direction * magnitude;
	}

	private Vector2 GetDirection(DirectionType directionType) {
		if (directionType == DirectionType.Left) return -Vector2.right;
		else if (directionType == DirectionType.Right) return Vector2.right;
		Debug.LogError("invalid direction");
		return Vector2.zero;
	}
}
