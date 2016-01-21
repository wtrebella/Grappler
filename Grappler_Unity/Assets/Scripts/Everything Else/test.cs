using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
	private void Awake() {
		StartCoroutine(WaitThenExplode());
	}

	private IEnumerator WaitThenExplode() {
		yield return new WaitForSeconds(2);
		SpriteSlicer2D.ExplodeSprite(gameObject, 50, 10);
	}

	private void Start() {

	}
	
	private void Update() {
	
	}
}
