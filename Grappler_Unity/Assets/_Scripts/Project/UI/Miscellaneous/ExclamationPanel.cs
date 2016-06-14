using UnityEngine;
using System.Collections;

public class ExclamationPanel : PanelBase {
	[SerializeField] private Player player;
	[SerializeField] private RectTransform exclamationTextHolder;
	[SerializeField] private ExclamationText exclamationTextPrefab;
	[SerializeField] private GrapplingStateController grapplingStateController;

	private void Awake() {
		grapplingStateController.SignalGrappleEndedWithSlopeDeviation += OnGrappleEndedWithSlopeDeviation;
	}

	private void ShowExclamation(string text) {
		UpdatePanelPosition();
		ExclamationText et = Instantiate(exclamationTextPrefab);
		et.transform.SetParent(exclamationTextHolder);
		et.SetText(text);
	}

	private void UpdatePanelPosition() {
		RectTransform rectTransform = (RectTransform)transform;
		Vector3 bodyPos = player.body.transform.position;
		Vector2 viewPos = ScreenUtility.instance.cam.WorldToViewportPoint(bodyPos);
		rectTransform.SetViewportPosition(viewPos);
	}

	private void OnGrappleEndedWithSlopeDeviation(float deviation) {
		if (deviation < 0.3f) ShowExclamation("Great!");
		else ShowExclamation("You suck!");
	}
}
