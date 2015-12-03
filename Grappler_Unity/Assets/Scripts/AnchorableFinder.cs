using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ArcRaycaster))]
public class AnchorableFinder : MonoBehaviour {
	private ArcRaycaster[] arcRaycasters;

	private void Awake() {
		arcRaycasters = GetComponentsInChildren<ArcRaycaster>();
	}

	public bool FindAnchorable(out Anchorable anchorable) {
		anchorable = null;

		foreach (ArcRaycaster arcRaycaster in arcRaycasters) {
			if (arcRaycaster.FindAnchorable(out anchorable)) break;
		}

		return anchorable != null;
	}
}
