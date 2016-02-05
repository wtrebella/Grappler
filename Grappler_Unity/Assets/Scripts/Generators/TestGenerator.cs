using UnityEngine;
using System.Collections;

public class TestGenerator : Generator {

	private void Awake() {
		BaseAwake();
	}

	private void Start() {
		GenerateItems(5);
	}
}
