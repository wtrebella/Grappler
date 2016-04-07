using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhitTerrainPair : MonoBehaviour {
	[SerializeField] private WhitTerrainFollower playerTest;

	[SerializeField] private WhitTerrain topTerrain;
	[SerializeField] private WhitTerrain bottomTerrain;

	[SerializeField] private float straightLength = 30;
	[SerializeField] private float minRadius = 20;

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
		if (patternType == WhitTerrainPairPatternType.Continue) Continue();
		else if (patternType == WhitTerrainPairPatternType.Widen) Widen();
		else if (patternType == WhitTerrainPairPatternType.Narrow) Narrow();
		else if (patternType == WhitTerrainPairPatternType.Bump) Bump();
	}

	private void Update() {
		Debug.Log(topTerrain.GetLastSection().distStart + ", " + bottomTerrain.GetLastSection().distStart);
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
		return (topTerrain.GetLastPoint() - bottomTerrain.GetLastPoint()).magnitude;
	}

	public void Continue() {
		WhitTerrainPairPattern pattern = WhitTerrainPairPatternManager.GetStraightPattern(currentSlope, 30.0f);
		AddPattern(pattern);
	}

	public void Widen() {
		WhitTerrainPairPattern pattern = WhitTerrainPairPatternManager.GetWidenPattern(currentSlope, 0.2f, 30.0f);
		AddPattern(pattern);
	}

	public void Narrow() {
		WhitTerrainPairPattern pattern = WhitTerrainPairPatternManager.GetNarrowPattern(currentSlope, 0.2f, 30.0f);
		AddPattern(pattern);
	}

	public void Bump() {
		float minRadius = 20;
		float maxRadius = minRadius + GetWidth();
		WhitTerrainPairPattern pattern = WhitTerrainPairPatternManager.GetBumpPattern(currentSlope, 0.3f, minRadius, maxRadius);
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

	private void Awake() {
		currentSlope = initialSlope;
		currentWidth = initialWidth;

		topTerrain.Initialize(Vector2.zero);
		bottomTerrain.Initialize(new Vector2(0, -currentWidth));
	}
}