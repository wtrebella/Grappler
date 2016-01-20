using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UILabel))]
public class DeadLabel : MonoBehaviour {
	[SerializeField] private Player player;

	private UILabel label;

	private void Awake() {
		player.SignalEnteredDeadState += HandleEnteredDeadState;
		label = GetComponent<UILabel>();
	}

	private void HandleEnteredDeadState() {
		Show();
	}

	public void Show() {
		label.enabled = true;
	}

	public void Hide() {
		label.enabled = false;
	}
}
