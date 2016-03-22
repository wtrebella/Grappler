using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum MoveType {
	Relative,
	Absolute
}

public class RectTransformMover : MonoBehaviour {
	[SerializeField] private RectTransform rectTransform;
	[SerializeField] private MoveType moveType;
	[SerializeField] private Vector3 position;

	private Vector3 originalPosition;
	private bool isMoved = false;

	public void Move() {
		if (isMoved) return;

		if (moveType == MoveType.Relative) rectTransform.localPosition += position;
		else if (moveType == MoveType.Absolute) rectTransform.localPosition = position;
		isMoved = true;
	}

	public void Revert() {
		if (!isMoved) return;

		rectTransform.localPosition = originalPosition;
		isMoved = false;
	}

	private void Awake() {
		originalPosition = rectTransform.localPosition;
	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
