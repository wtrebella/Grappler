using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	public void Collect() {
		Recycle();
	}

	private void Recycle() {
		gameObject.Recycle();
	}
}
