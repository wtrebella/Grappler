using UnityEngine;
using System.Collections;

public class MountainCollisionHandler : MonoBehaviour {

	public void HandleHitMountain(Collision2D collision) {
		ScreenShaker.instance.CollisionShake(collision.relativeVelocity.magnitude);
	}
}