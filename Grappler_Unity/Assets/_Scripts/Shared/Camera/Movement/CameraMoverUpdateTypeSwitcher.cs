using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameCamera))]
public class CameraMoverUpdateTypeSwitcher : MonoBehaviour {
	private GameCamera cameraMover;

	private void Awake() {
		cameraMover = GetComponent<GameCamera>();
	}

	public void UseUpdate() {
		cameraMover.SetUpdateType(WhitUpdateType.Update);
	}

	public void UseFixedUpdate() {
		cameraMover.SetUpdateType(WhitUpdateType.FixedUpdate);
	}
}
