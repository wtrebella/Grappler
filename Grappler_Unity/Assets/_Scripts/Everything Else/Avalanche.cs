using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Follow))]
[RequireComponent(typeof(FollowOffsetChanger))]
public class Avalanche : MonoBehaviour {
	private Follow follow;
	private FollowOffsetChanger followOffsetChanger;

	private void Awake() {
		follow = GetComponent<Follow>();
		followOffsetChanger = GetComponent<FollowOffsetChanger>();
	}
}
