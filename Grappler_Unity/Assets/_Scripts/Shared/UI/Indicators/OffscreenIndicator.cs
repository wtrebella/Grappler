using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using WhitDataTypes;

public class OffscreenIndicator : MonoBehaviour {
	[SerializeField] private RectTransform root;
	[SerializeField] private Transform focus;
	[SerializeField] private Camera cam;
	[SerializeField] private CanvasScaler scaler;
	[SerializeField] private FloatRange distanceRange = new FloatRange(0, 1500);

	private void Awake() {

	}

	private void Start() {
	
	}
	
	private void Update() {
		Vector3 focusPosition = focus.transform.position;
		Vector3 screenPosition = cam.WorldToScreenPoint(focusPosition);
		Rect rootRect = root.rect;

		RectTransform rectTransform = (RectTransform)transform;
		Vector3 newPosition = screenPosition;
		// TODO figure out why you have to multiply by 2 here
		float xMax = rootRect.xMax * 2 - rectTransform.sizeDelta.x * scaler.scaleFactor / 2f;
		newPosition.x = Mathf.Min(screenPosition.x, xMax);
		float xDistance = Mathf.Max(0, screenPosition.x - xMax);
		float scale = 1 - distanceRange.GetPercent(xDistance);
		transform.position = newPosition;
		transform.localScale = new Vector3(scale, scale, 1.0f);
	}
}
