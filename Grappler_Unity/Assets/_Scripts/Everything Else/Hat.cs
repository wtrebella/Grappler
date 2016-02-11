using UnityEngine;
using System.Collections;
using Spine;

public class Hat : MonoBehaviour {
	[SerializeField] private SkeletonAnimation stickmanTop;

	private void Awake() {
		
	}

	private void Start() {
		stickmanTop.skeleton.SetAttachment("hat", "hats/sombrero");

	}
	
	private void Update() {

	}
}
