using UnityEngine;
using System.Collections;

public class FramerateSetter : MonoBehaviour {
	[SerializeField] private int targetFrameRate = 60;
	private void Awake() {
		Application.targetFrameRate = targetFrameRate;
	}
}
