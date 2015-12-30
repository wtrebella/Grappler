using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Follow))]
public class FollowAxisChangerOnDeath : MonoBehaviour {
	[SerializeField] private Grappler grappler;
	[SerializeField] private FollowAxisType axisTypeToChangeTo = FollowAxisType.Y;

	private Follow follow;

	private void Awake() {
		follow = GetComponent<Follow>();
		grappler.SignalGrapplerDied += HandleGrapplerDied;
	}

	private void HandleGrapplerDied() {
		follow.SetAxisType(axisTypeToChangeTo);
	}
}
