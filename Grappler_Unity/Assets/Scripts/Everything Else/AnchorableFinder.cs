using UnityEngine;
using System.Collections;

public class AnchorableFinder : MonoBehaviour {
	[SerializeField] private ArcRaycaster[] arcRaycasters;
	[SerializeField] private CircleOverlapper[] circleOverlappers;
	[SerializeField] private BoxCaster boxCaster;

	public bool FindAnchorableInDirection(out Anchorable anchorable, Vector2 direction) {
		anchorable = null;

		foreach (ArcRaycaster arcRaycaster in arcRaycasters) {
			if (arcRaycaster.FindAnchorable(out anchorable, direction)) break;
		}

		return anchorable != null;
	}

	public bool FindAnchorableInCircle(out Anchorable anchorable) {
		anchorable = null;
		
		foreach (CircleOverlapper circleOverlapper in circleOverlappers) {
			if (circleOverlapper.FindAnchorable(out anchorable)) break;
		}
		
		return anchorable != null;
	}

	public bool FindAnchorableInBoxCast(out Anchorable anchorable) {
		anchorable = null;
		
		return boxCaster.FindAnchorable(out anchorable);
	}
}
