using UnityEngine;
using System.Collections;

public class AnchorableFinder : MonoBehaviour {
	[SerializeField] private ArcRaycaster[] raycasters;

	public bool FindAnchorable(out Anchorable anchorable) {
		Collider2D foundCollider;
		anchorable = null;

		foreach (ArcRaycaster raycaster in raycasters) {
			if (raycaster.FindCollider(out foundCollider)) {
				anchorable = foundCollider.GetComponent<Anchorable>();
				break;
			}
		}
		
		return anchorable != null;
	}
}
