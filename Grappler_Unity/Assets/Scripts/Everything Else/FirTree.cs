using UnityEngine;
using System.Collections;

public class FirTree : MonoBehaviour {
	public bool hasBeenSliced {get; private set;}

	[SerializeField] private ParticleSystem sliceParticles;

	private void Recycle(float delay) {
		StartCoroutine(WaitThenRecycle(delay));
	}

	private IEnumerator WaitThenRecycle(float delay) {
		yield return new WaitForSeconds(delay);
		this.Recycle();
	}

	public void HandleCollision(Collision2D collision) {
		if (hasBeenSliced) return;

		Slice(collision);
		PlayParticles();
	}

	private void Slice(Collision2D collision) {
		Vector2 contact = collision.contacts[0].point;
		Vector2 direction = collision.relativeVelocity.normalized;
		GameObject spriteObject = GetComponentInChildren<tk2dSprite>().gameObject;
		Vector2 startPoint = contact - direction * 10;
		Vector2 endPoint = contact + direction * 10;
		SpriteSlicer2D.ExplodeSprite(spriteObject, 2, 100);//startPoint.ToVector3(), endPoint.ToVector3(), spriteObject);
	}

	private void PlayParticles() {
		sliceParticles.Play();
	}

	private void Awake() {
		hasBeenSliced = false;
	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
