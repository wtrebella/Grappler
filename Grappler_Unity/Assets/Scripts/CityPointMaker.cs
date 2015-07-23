using UnityEngine;
using System.Collections;

public class CityPointMaker : MonoBehaviour {
	[SerializeField] private float minY = 0;
	[SerializeField] private float maxY = 83;
	[SerializeField] private float minStepDistanceX = 2;
	[SerializeField] private float maxStepDistanceX = 15;
	[SerializeField] private float minStepDistanceY = 2;
	[SerializeField] private float maxStepDistanceY = 8;

	private enum StepType {Vertical, Horizontal};

	private StepType previousStepType;
	private Vector2 previousPoint;
	private int numPointsMade = 0;

	public Vector2 GetNextPoint() {
		Vector2 nextPoint = Vector2.zero;

		if (numPointsMade == 0) nextPoint = GetFirstPoint();
		else if (numPointsMade == 1) nextPoint = GetSecondPoint();
		else {
			if (previousStepType == StepType.Vertical) nextPoint = GetHoriztonalStepPoint();
			else if (previousStepType == StepType.Horizontal) nextPoint = GetVerticalStepPoint();
		}

		return nextPoint;
	}

	public void HandleNextPointUsed() {
		if (numPointsMade == 1) previousStepType = StepType.Vertical;
		else if (numPointsMade > 1) {
			if (previousStepType == StepType.Vertical) previousStepType = StepType.Horizontal;
			else if (previousStepType == StepType.Horizontal) previousStepType = StepType.Vertical;
		}

		numPointsMade++;
		previousPoint = GetNextPoint();
	}

	private Vector2 GetFirstPoint() {
		return Vector2.zero;
	}

	private Vector2 GetSecondPoint() {
		return new Vector2(0, GetRandomNumberWithinRange());
	}

	private Vector2 GetHoriztonalStepPoint() {
		if (numPointsMade < 2) Debug.LogError("you haven't created the first two points yet!");

		Vector2 point;
		point.x = previousPoint.x + Random.Range(minStepDistanceX, maxStepDistanceX);
		point.y = previousPoint.y;
		return point;
	}

	private Vector2 GetVerticalStepPoint() {
		if (numPointsMade < 2) Debug.LogError("you haven't created the first two points yet!");

		Vector2 point;
		point.x = previousPoint.x;
		point.y = Mathf.Clamp(previousPoint.y + Random.Range(minStepDistanceY, maxStepDistanceY) * (Random.value < 0.5f ? 1 : -1), minY, maxY);
		return point;
	}

	private float GetRandomNumberWithinRange() {
		return Random.Range(minY, maxY);
	}
}
