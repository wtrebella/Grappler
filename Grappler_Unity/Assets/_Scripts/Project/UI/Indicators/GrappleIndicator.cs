using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GrappleIndicator : MonoBehaviour {
	[SerializeField] private GrapplingManager grapplingManager;
	[SerializeField] private Image bgImage;
	[SerializeField] private Image iconImage;

	[SerializeField] private Sprite readySprite;
	[SerializeField] private Sprite notReadySprite;

	[SerializeField] private Color readyColor = new Color(50f/255f, 251f/255f, 165f/255f, 1f);
	[SerializeField] private Color notReadyColor = new Color(233f/255f, 77f/255f, 67f/255f, 1f);

	private void Awake() {
		grapplingManager.SignalGrapplingEnabledChanged += OnGrapplingEnabledChanged;
	}

	private void OnGrapplingEnabledChanged(bool grapplingEnabled) {
		if (grapplingEnabled) SetReady();
		else SetNotReady();
	}

	private void SetReady() {
		bgImage.color = readyColor;
		iconImage.sprite = readySprite;
	}

	private void SetNotReady() {
		bgImage.color = notReadyColor;
		iconImage.sprite = notReadySprite;
	}
}
