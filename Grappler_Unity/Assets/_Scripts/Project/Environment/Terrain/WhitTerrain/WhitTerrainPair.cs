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

	private float currentSlope = 0.1f;

	public Vector2 GetAveragePointAtDist(float dist) {
		Vector2 topPoint = topTerrain.GetPointAtDist(dist);
		Vector2 bottomPoint = bottomTerrain.GetPointAtDist(dist);
		Vector2 averagePoint = WhitTools.GetAveragePoint(topPoint, bottomPoint);
		return averagePoint;
	}

	public bool NeedsNewPart() {
		return topTerrain.LastSectionIsPastScreenMargin() || bottomTerrain.LastSectionIsPastScreenMargin();
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

//	public void Bump(float slope, float radius

	public void Widen(float slope, float radius) {
		topTerrain.AddCurve(currentSlope + slope, radius);
		bottomTerrain.AddCurve(currentSlope - slope, radius);

		topTerrain.AddCurve(currentSlope, radius);
		bottomTerrain.AddCurve(currentSlope, radius);
	}

	public void Narrow(float slope, float radius) {
		topTerrain.AddCurve(currentSlope - slope, radius);
		bottomTerrain.AddCurve(currentSlope + slope, radius);

		topTerrain.AddCurve(currentSlope, radius);
		bottomTerrain.AddCurve(currentSlope, radius);
	}

	public void TurnToSlope(float slope) {
		bool clockwise = slope < currentSlope;
		float maxRadius = minRadius + GetWidth();

		if (clockwise) {
			topTerrain.AddCurve(slope, maxRadius);
			bottomTerrain.AddCurve(slope, minRadius);
		}
		else {
			topTerrain.AddCurve(slope, minRadius);
			bottomTerrain.AddCurve(slope, maxRadius);
		}

		currentSlope = slope;
	}

	public void TurnBySlope(float deltaSlope) {
		TurnToSlope(currentSlope + deltaSlope);
	}

	private void Update() {
		if (NeedsNewPart()) Continue();

		if (shouldWiden) {
			shouldWiden = false;
			Widen(0.5f, 30.0f);
		}
		if (shouldNarrow) {
			shouldNarrow = false;
			Narrow(0.5f, 30.0f);
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