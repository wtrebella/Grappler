using UnityEngine;
using System.Collections;

public class BigIcicle : MonoBehaviour {
	public bool hasBeenSliced {get; private set;}
	
	[SerializeField] private ParticleSystem sliceParticles;
	
	public void HandleSlice() {
		if (hasBeenSliced) return;
		hasBeenSliced = true;
		PlayParticles();
		Recycle(3);
	}
	
	private void Recycle(float delay) {
		StartCoroutine(WaitThenRecycle(delay));
	}
	
	private IEnumerator WaitThenRecycle(float delay) {
		yield return new WaitForSeconds(delay);
		this.Recycle();
	}
	
	public void HandleCollision(Collision2D collision) {
		if (hasBeenSliced) return;
		PlayParticles();
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
