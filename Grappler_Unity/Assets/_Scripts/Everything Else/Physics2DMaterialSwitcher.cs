using UnityEngine;
using System.Collections;

public class Physics2DMaterialSwitcher : MonoBehaviour {
	[SerializeField] private Collider2D[] colliders;
	[SerializeField] private PhysicsMaterial2D material1;
	[SerializeField] private PhysicsMaterial2D material2;

	public void EnableMaterial1() {
		EnableMaterial(material1);
	}

	public void EnableMaterial2() {
		EnableMaterial(material2);
	}

	private void EnableMaterial(PhysicsMaterial2D material) {
		foreach (Collider2D collider in colliders) collider.sharedMaterial = material;
	}

	private void Awake() {

	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
