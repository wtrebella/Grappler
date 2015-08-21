using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ArcRaycaster))]
public class AnchorableFinder : MonoBehaviour {
	[SerializeField] private ArcRaycaster arcRaycaster;

	public bool FindAnchorable(out Anchorable anchorable, float angle) {
		Collider2D foundCollider;
		anchorable = null;

		if (arcRaycaster.FindCollider(out foundCollider, angle)) anchorable = foundCollider.GetComponent<Anchorable>();

		return anchorable != null;
	}
}
