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

	public void Explode() {
		Slice();
		PlayParticles();
	}

	public void HandleCollision(Collision2D collision) {
		if (hasBeenSliced) return;

		if (autoExplode) Explode();
	}

	public void HandleTrigger(Collider2D collider) {
		if (hasBeenSliced) return;

		if (autoExplode) Explode();
	}

	private void Slice() {
		GameObject spriteObject = GetComponentInChildren<tk2dSprite>().gameObject;
		Vector2 pos = spriteObject.transform.position;
		Vector2 startPos = pos;
		Vector2 endPos = pos;
		startPos.x -= 5;
		endPos.x += 5;
		startPos.y += 10;
		endPos.y += 10;
		SpriteSlicer2D.SliceSprite(startPos, endPos, spriteObject);
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
