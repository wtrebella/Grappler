using UnityEngine;
using System.Collections;

public class FirTree : MonoBehaviour {
	public bool hasBeenSliced {get; private set;}

	[SerializeField] private ParticleSystem sliceParticles;
	
	public void HandleSlice() {
		if (hasBeenSliced) return;

		hasBeenSliced = true;

		sliceParticles.Play();

		StartCoroutine(WaitThenRecycle());
	}

	private IEnumerator WaitThenRecycle() {
		yield return new WaitForSeconds(3);
		this.Recycle();
	}

	public void HandleCollision(Collision2D collision) {
		if (hasBeenSliced) return;


	}

	private void Awake() {
		hasBeenSliced = false;
	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
