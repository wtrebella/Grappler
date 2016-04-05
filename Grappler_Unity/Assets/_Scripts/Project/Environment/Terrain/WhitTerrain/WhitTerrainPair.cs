using UnityEngine;
using System.Collections;

public class WhitTerrainPair : MonoBehaviour {
	[SerializeField] private WhitTerrain topTerrain;
	[SerializeField] private WhitTerrain bottomTerrain;

	[SerializeField] private float straightLength = 30;
	[SerializeField] private float minRadius = 20;

	public bool shouldContinue = false;
	public bool shouldTurnRight = false;
	public bool shouldTurnLeft = false;
	public bool shouldWiden = false;
	public bool shouldNarrow = false;

	private float currentSlope = 0.2f;

	public Vector2 GetAveragePointAtDist(float dist) {
		return WhitTools.GetAveragePoint(
			topTerrain.GetPointAtDist(dist),
			bottomTerrain.GetPointAtDist(dist)
		);
	}

	public bool IsValid() {
		return topTerrain.IsValid() && bottomTerrain.IsValid();
	}

	public float GetWidth() {
		return (topTerrain.GetLastPoint() - bottomTerrain.GetLastPoint()).magnitude;
	}

	public void Continue() {
		float slopeVariation = WhitTerrainAttributes.instance.slopeVariationRange.GetRandom();
		topTerrain.AddStraight(currentSlope + slopeVariation, straightLength);
		bottomTerrain.AddStraight(currentSlope + slopeVariation, straightLength);
	}

	public void Widen() {
		float slope = 0.5f;
		float radius = 30;

		topTerrain.AddCurve(currentSlope + slope, radius);
		bottomTerrain.AddCurve(currentSlope - slope, radius);

		topTerrain.AddCurve(currentSlope, radius);
		bottomTerrain.AddCurve(currentSlope, radius);
	}

	public void Narrow() {
		float slope = 0.5f;
		float radius = 30;

		topTerrain.AddCurve(currentSlope - slope, radius);
		bottomTerrain.AddCurve(currentSlope + slope, radius);

		topTerrain.AddCurve(currentSlope, radius);
		bottomTerrain.AddCurve(currentSlope, radius);
	}

	public void TurnToSlope(float targetSlope) {
		bool clockwise = targetSlope < currentSlope;
		float maxRadius = minRadius + GetWidth();

		if (clockwise) {
			topTerrain.AddCurve(targetSlope, maxRadius);
			bottomTerrain.AddCurve(targetSlope, minRadius);
		}
		else {
			topTerrain.AddCurve(targetSlope, minRadius);
			bottomTerrain.AddCurve(targetSlope, maxRadius);
		}

		currentSlope = targetSlope;
	}

	public void TurnBySlope(float deltaSlope) {
		TurnToSlope(currentSlope + deltaSlope);
	}

	private void Update() {
		if (shouldWiden) {
			shouldWiden = false;
			Widen();
		}
		if (shouldNarrow) {
			shouldNarrow = false;
			Narrow();
		}
		if (shouldContinue) {
			shouldContinue = false;
			Continue();
		}
		if (shouldTurnRight) {
			shouldTurnRight = false;
			TurnBySlope(-0.3f);
		}
		if (shouldTurnLeft) {
			shouldTurnLeft = false;
			TurnBySlope(0.3f);
		}
	}
}