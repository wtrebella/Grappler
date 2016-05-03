using UnityEngine;
using System.Collections;

public class AnchorableFinder : MonoBehaviour {
	[SerializeField] private ArcRaycaster[] arcRaycasters;
	[SerializeField] private CircleOverlapper[] circleOverlappers;
	[SerializeField] private AreaOverlapper screenOverlapper;

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

	public bool FindAnchorableInScreenOverlap(out Anchorable anchorable) {
		anchorable = null;
		return screenOverlapper.FindAnchorable(out anchorable);
	}
}
