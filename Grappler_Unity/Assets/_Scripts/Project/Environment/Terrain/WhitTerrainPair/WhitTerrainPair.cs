using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhitTerrainPair : MonoBehaviour {
	[SerializeField] private WhitTerrainFollower playerTest;

	[SerializeField] private WhitTerrain topTerrain;
	[SerializeField] private WhitTerrain bottomTerrain;

	[SerializeField] private float initialSlope = 0.1f;
	[SerializeField] private float initialWidth = 16.0f;

	private float currentSlope;
	private float currentWidth;

	public Vector2 GetAveragePointAtDist(float dist) {
		Vector2 topPoint = topTerrain.GetPointAtDist(dist);
		Vector2 bottomPoint = bottomTerrain.GetPointAtDist(dist);
		Vector2 averagePoint = WhitTools.GetAveragePoint(topPoint, bottomPoint);
		return averagePoint;
	}
		
	public void AddRandomPattern() {
		WhitTerrainPairPatternType patternType = WhitTerrainPairAttributes.instance.GetRandomPatternType();
		if (patternType == WhitTerrainPairPatternType.Straight) Straight();
		else if (patternType == WhitTerrainPairPatternType.Widen) Widen();
		else if (patternType == WhitTerrainPairPatternType.Narrow) Narrow();
		else if (patternType == WhitTerrainPairPatternType.Bump) Bump();
	}

	private void Update() {
		if (NeedsNewPattern(playerTest.dist)) AddRandomPattern();
	}

	public bool NeedsNewPattern(float playerDist) {
		bool needsTopPattern = topTerrain.DistIsWithinDistThreshold(playerDist);
		bool needsBottomPattern = bottomTerrain.DistIsWithinDistThreshold(playerDist);
		return needsTopPattern || needsBottomPattern;
	}

	public bool IsValid() {
		return topTerrain.IsValid() && bottomTerrain.IsValid();
	}

	public float GetWidth() {
		return (topTerrain.GetEndPoint() - bottomTerrain.GetEndPoint()).magnitude;
	}

	public void Straight() {
		WhitTerrainPairPattern pattern = WhitTerrainPairPatternGenerator.GetStraightPattern(currentSlope, GetTopStraightLength(), GetBottomStraightLength());
		AddPattern(pattern);
	}

	public void Widen() {
		WhitTerrainPairPattern pattern = WhitTerrainPairPatternGenerator.GetWidenPattern(currentSlope, 0.2f, 30.0f);
		AddPattern(pattern);
	}

	public void Narrow() {
		WhitTerrainPairPattern pattern = WhitTerrainPairPatternGenerator.GetNarrowPattern(currentSlope, 0.2f, 30.0f);
		AddPattern(pattern);
	}

	public void Bump() {
		float maxRadius = WhitTerrainPairAttributes.instance.minCurveRadius + GetWidth();
		WhitTerrainPairPattern pattern = WhitTerrainPairPatternGenerator.GetBumpPattern(currentSlope, 0.3f, WhitTerrainPairAttributes.instance.minCurveRadius, maxRadius);
		AddPattern(pattern);
	}

	private void AddPattern(WhitTerrainPairPattern pattern) {
		foreach (WhitTerrainPatternInstructionPair instructionPair in pattern.instructionPairs) {
			if (instructionPair.topInstruction.instructionType == WhitTerrainPatternInstructionType.Straight) {
				WhitTerrainPatternInstructionStraight topInstruction = (WhitTerrainPatternInstructionStraight)instructionPair.topInstruction;
				topTerrain.AddStraight(topInstruction.slope, topInstruction.length);
			}
			else if (instructionPair.topInstruction.instructionType == WhitTerrainPatternInstructionType.Curve) {
				WhitTerrainPatternInstructionCurve topInstruction = (WhitTerrainPatternInstructionCurve)instructionPair.topInstruction;
				topTerrain.AddCurve(topInstruction.targetSlope, topInstruction.radius);
			}

			if (instructionPair.bottomInstruction.instructionType == WhitTerrainPatternInstructionType.Straight) {
				WhitTerrainPatternInstructionStraight bottomInstruction = (WhitTerrainPatternInstructionStraight)instructionPair.bottomInstruction;
				bottomTerrain.AddStraight(bottomInstruction.slope, bottomInstruction.length);
			}
			else if (instructionPair.bottomInstruction.instructionType == WhitTerrainPatternInstructionType.Curve) {
				WhitTerrainPatternInstructionCurve bottomInstruction = (WhitTerrainPatternInstructionCurve)instructionPair.bottomInstruction;
				bottomTerrain.AddCurve(bottomInstruction.targetSlope, bottomInstruction.radius);
			}
		}
	}

	private Vector2 GetDirectionAtEnd() {
		Vector2 topDirection = topTerrain.GetLastSectionDirection();
		Vector2 bottomDirection = bottomTerrain.GetLastSectionDirection();
		Vector2 averageDirection = WhitTools.GetAveragePoint(topDirection, bottomDirection).normalized;
		return averageDirection;
	}

	private Vector2 GetDirectionBetweenEndPoints() {
		Vector2 topEndPoint = topTerrain.GetEndPoint();
		Vector2 bottomEndPoint = bottomTerrain.GetEndPoint();

		Vector2 endPointVector = topEndPoint - bottomEndPoint;
		Vector2 endPointDirection = endPointVector.normalized;

		return endPointDirection;
	}

	private float GetTopStraightLength() {
		return WhitTerrainPairAttributes.instance.straightLength;
	}

	private float GetBottomStraightLength() {
		float width = GetWidth();
		Vector2 directionAtEnd = GetDirectionAtEnd();

		Vector2 targetDirectionBetweenEndPoints = new Vector2(directionAtEnd.y, -directionAtEnd.x);

		Vector2 topEndPoint = topTerrain.GetEndPoint();
		Vector2 bottomEndPoint = bottomTerrain.GetEndPoint();

		float topLength = GetTopStraightLength();
		Vector2 newTopEndPoint = topEndPoint + directionAtEnd * topLength;
		Vector2 newBottomEndPoint = newTopEndPoint + targetDirectionBetweenEndPoints * width;
		float bottomLength = (newBottomEndPoint - bottomEndPoint).magnitude;
		return bottomLength;
	}

	private void Awake() {
		currentSlope = initialSlope;
		currentWidth = initialWidth;

		topTerrain.Initialize(Vector2.zero);
		bottomTerrain.Initialize(new Vector2(0, -currentWidth));
	}
}