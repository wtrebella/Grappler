using UnityEngine;
using System.Collections;

public class TrailBurster : MonoBehaviour {
	[SerializeField] private TrailRenderer trail;

	public void OnPushBack() {
		trail.material.color = Color.red;
		trail.startWidth = 2;
	}

	private void Awake() {

	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
