using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour {
	public bool hasBeenSliced {get; private set;}

	public bool autoExplode = true;

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

		if (autoExplode) Slice(collision.collider.transform.position, collision.relativeVelocity.normalized);
	}

	public void HandleTrigger(Collider2D collider) {
		if (hasBeenSliced) return;

		if (autoExplode) Slice(collider.transform.position, collider.attachedRigidbody.velocity.normalized);
	}

	private void Slice(Vector2 hitPoint, Vector2 hitDirection) {
		GameObject spriteObject = GetComponentInChildren<tk2dSprite>().gameObject;
		Vector2 pos = hitPoint - hitDirection * 20;
		Vector2 startPos = pos;
		Vector2 endPos = pos + hitDirection * 60;
		Debug.DrawLine(startPos, endPos, Color.blue, 1);
		hasBeenSliced = true;
		SpriteSlicer2D.SliceSprite(startPos, endPos, spriteObject);
		sliceParticles.Play();
	}

	private void PlayParticles() {
		
	}

	private void Awake() {
		hasBeenSliced = false;
	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
