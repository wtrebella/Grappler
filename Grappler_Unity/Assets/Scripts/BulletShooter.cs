using UnityEngine;
using System.Collections;

public class BulletShooter : MonoBehaviour {
	[SerializeField] private Bullet bulletPrefab;
	[SerializeField] private float minInterval;
	[SerializeField] private float maxInterval;

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
		bullet.Shoot(directionType);
	}
}
