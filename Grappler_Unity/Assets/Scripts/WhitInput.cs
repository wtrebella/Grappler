using UnityEngine;
using System.Collections;

public class WhitInput : MonoBehaviour {
	public static WhitInput instance;

	private void Awake() {
		instance = this;
	}

	public static bool AtLeastOneTouchDown() {
		if (SystemInfo.deviceType == DeviceType.Handheld) {
			foreach (Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) return true;
			}
			return false;
		}
		else return Input.GetMouseButton(0);
	}
}
