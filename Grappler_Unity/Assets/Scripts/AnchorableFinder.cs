using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AngleRaycaster))]
public class AnchorableFinder : MonoBehaviour {
	[SerializeField] private AngleRaycaster angleRaycaster;

	public bool FindAnchorable(out Anchorable anchorable, float angle) {
		Collider2D foundCollider;
		anchorable = null;

		if (angleRaycaster.FindCollider(out foundCollider, angle)) anchorable = foundCollider.GetComponent<Anchorable>();

		return anchorable != null;
	}
}
