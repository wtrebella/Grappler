using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CameraMover))]
public class CameraMoverUpdateTypeSwitcher : MonoBehaviour {
	private CameraMover cameraMover;

	private void Awake() {
		cameraMover = GetComponent<CameraMover>();
	}

	public void UseUpdate() {
		cameraMover.SetUpdateType(WhitUpdateType.Update);
	}

	public void UseFixedUpdate() {
		cameraMover.SetUpdateType(WhitUpdateType.FixedUpdate);
	}
}
