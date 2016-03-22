using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour {
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

		Slice();
		PlayParticles();
	}

	public void HandleTrigger(Collider2D collider) {
		if (hasBeenSliced) return;

		Slice();
		PlayParticles();
	}

	private void Slice() {
		GameObject spriteObject = GetComponentInChildren<tk2dSprite>().gameObject;
		SpriteSlicer2D.ExplodeSprite(spriteObject, 2, 100);
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
