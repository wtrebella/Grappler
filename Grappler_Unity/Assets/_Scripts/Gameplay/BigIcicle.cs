using UnityEngine;
using System.Collections;

public class BigIcicle : MonoBehaviour {
	public bool hasBeenSliced {get; private set;}

	public void HandleSlice() {
		if (hasBeenSliced) return;
		hasBeenSliced = true;
		Recycle(3);
	}
	 
	private void Recycle(float delay) {
		StartCoroutine(WaitThenRecycle(delay));
	}
	
	private IEnumerator WaitThenRecycle(float delay) {
		yield return new WaitForSeconds(delay);
		this.Recycle();
	}
	
	private void Awake() {
		hasBeenSliced = false;
	}
	
	private void Start() {
		
	}
	
	private void Update() {
		
	}
}
