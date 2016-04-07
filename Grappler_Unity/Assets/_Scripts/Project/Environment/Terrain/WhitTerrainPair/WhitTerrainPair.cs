using UnityEngine;
using System.Collections;

public class WhitTerrainPair : MonoBehaviour {
	[SerializeField] private WhitTerrain topTerrain;
	[SerializeField] private WhitTerrain bottomTerrain;

	[SerializeField] private float straightLength = 30;
	[SerializeField] private float minRadius = 20;

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
		WhitTerrainPairPattern pattern = WhitTerrainPairPatternManager.GetStraightPattern(currentSlope, 30.0f);
		AddPattern(pattern);
	}

	public void Widen() {
		WhitTerrainPairPattern pattern = WhitTerrainPairPatternManager.GetWidenPattern(currentSlope, 0.2f, 30.0f);
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

	private void Update() {
		if (NeedsNewPart()) {
			float val = Random.value;
			if (val < 0.8f) Continue();
			else if (val < 0.9f) Widen();
			else Bump();
		}
	}
}