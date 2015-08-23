using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ArcRaycaster))]
public class AnchorableFinder : MonoBehaviour {
	[SerializeField] private ArcRaycaster[] arcRaycasters;

	public bool FindAnchorable(out Anchorable anchorable) {
		Collider2D foundCollider;
		anchorable = null;

		foreach (ArcRaycaster arcRaycaster in arcRaycasters) {
			if (arcRaycaster.FindCollider(out foundCollider)) {
				anchorable = foundCollider.GetComponent<Anchorable>();
				break;
			}
		}

		return anchorable != null;
	}
}
