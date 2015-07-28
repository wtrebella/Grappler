using UnityEngine;
using System.Collections;

public class CityPointMaker : MonoBehaviour {
	[SerializeField] private float minY = 0;
	[SerializeField] private float maxY = 83;
	[SerializeField] private float minStepDistanceX = 2;
	[SerializeField] private float maxStepDistanceX = 15;
	[SerializeField] private float minStepDistanceY = 2;
	[SerializeField] private float maxStepDistanceY = 8;

	private enum StepType {None, Vertical, Horizontal};

	private Vector2 currentPoint = Vector2.zero;
	private StepType previousStepType = StepType.None;
	private Vector2 previousPoint;
	private int currentPointNum = 0;

	public Vector2 GetCurrentPoint() {
		return currentPoint;
	}

	public void HandleCurrentPointUsed() {
		previousPoint = GetCurrentPoint();
		previousStepType = GetCurrentStepType();
		currentPointNum++;
		GenerateNewCurrentPoint();
	}

	private void GenerateNewCurrentPoint() {
		if (currentPointNum == 0) {
			currentPoint = GetFirstPoint();
		}
		else if (currentPointNum == 1) {
			currentPoint = GetSecondPoint();
		}
		else {
			if (CurrentStepTypeIsHorizontal()) currentPoint = GetHorizontalStepPoint();
			else if (CurrentStepTypeIsVertical()) currentPoint = GetVerticalStepPoint();
		}
	}

	private StepType GetCurrentStepType() {
		if (currentPointNum == 0) return StepType.None;
		else if (currentPointNum == 1) return StepType.Vertical;
		else {
			if (previousStepType == StepType.Horizontal) return StepType.Vertical;
			else if (previousStepType == StepType.Vertical) return StepType.Horizontal;
		}

		return StepType.None;
	}

	private bool CurrentStepTypeIsVertical() {
		return GetCurrentStepType() == StepType.Vertical;
	}

	private bool CurrentStepTypeIsHorizontal() {
		return GetCurrentStepType() == StepType.Horizontal;
	}

	private Vector2 GetFirstPoint() {
		return Vector2.zero;
	}

	private Vector2 GetSecondPoint() {
		return new Vector2(0, GetRandomNumberWithinRange());
	}

	private Vector2 GetHorizontalStepPoint() {
		if (currentPointNum < 2) Debug.LogError("you haven't created the first two points yet!");

		Vector2 point;
		point.x = previousPoint.x + Random.Range(minStepDistanceX, maxStepDistanceX);
		point.y = previousPoint.y;
		return point;
	}

	private Vector2 GetVerticalStepPoint() {
		if (currentPointNum < 2) Debug.LogError("you haven't created the first two points yet!");

		Vector2 point;
		point.x = previousPoint.x;
		float stepDist = Random.Range(minStepDistanceY, maxStepDistanceY);
		float positiveStepY = previousPoint.y + stepDist;
		float negativeStepY = previousPoint.y - stepDist;
		if (positiveStepY > maxY) point.y = negativeStepY;
		else if (negativeStepY < minY) point.y = positiveStepY;
		else point.y = Random.value < 0.5f ? positiveStepY : negativeStepY;

		return point;
	}

	private float GetRandomNumberWithinRange() {
		return Random.Range(minY, maxY);
	}
}
