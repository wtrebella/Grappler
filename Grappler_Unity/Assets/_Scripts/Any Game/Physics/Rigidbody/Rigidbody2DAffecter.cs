using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rigidbody2DAffecter : MonoBehaviour {
	public Rigidbody2D[] rigidbodies {
		get {
			if (affecterGroup == null) {
				affecterGroup = GetComponentInParent<Rigidbody2DAffecterGroup>();
				if (affecterGroup == null) {
					Debug.LogError("rigidbody affecters need to be children of a Rigidbody2DAffecterGroup");
					return null;
				}
			}

			return affecterGroup.rigidbodies;
		}
	}

	private Rigidbody2DAffecterGroup affecterGroup;
}
