using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Follow))]
public class FollowAxisChangerOnDeath : MonoBehaviour {
	[SerializeField] private WhitAxisType axisTypeToChangeTo = WhitAxisType.Y;

	private Follow follow;

	private void Awake() {
		follow = GetComponent<Follow>();
	}

	private void HandleGrapplerDied() {
		follow.SetAxisType(axisTypeToChangeTo);
	}
}
