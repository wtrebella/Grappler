﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhitTerrainPair : MonoBehaviour {
	[SerializeField] private Transform focusObject;

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

	public Vector2 GetStartPoint() {
		return WhitTools.GetAveragePoint(topTerrain.GetStartPoint(), bottomTerrain.GetStartPoint());
	}

	public Vector2 GetEndPoint() {
		return WhitTools.GetAveragePoint(topTerrain.GetEndPoint(), bottomTerrain.GetEndPoint());
	}

	public float GetStartDist() {
		float topStartDist = topTerrain.GetStartDist();
		float bottomStartDist = bottomTerrain.GetStartDist();
		float startDist = (topStartDist + bottomStartDist) / 2f;
		return startDist;
	}

	public float GetEndDist() {
		float topEndDist = topTerrain.GetEndDist();
		float bottomEndDist = bottomTerrain.GetEndDist();
		float endDist = (topEndDist + bottomEndDist) / 2f;
		return endDist;
	}

	public float GetTotalDist() {
		float topTotalDist = topTerrain.GetTotalDist();
		float bottomTotalDist = bottomTerrain.GetTotalDist();
		float totalDist = (topTotalDist + bottomTotalDist) / 2f;
		return totalDist;
	}
		
	public void AddRandomPattern() {
		WhitTerrainPairPatternType patternType = WhitTerrainPairAttributes.instance.GetRandomPatternType();
		if (patternType == WhitTerrainPairPatternType.Straight) Straight();
		else if (patternType == WhitTerrainPairPatternType.Widen) Widen();
		else if (patternType == WhitTerrainPairPatternType.Narrow) Narrow();
		else if (patternType == WhitTerrainPairPatternType.Bump) Bump();
	}

	private void Update() {
		if (NeedsNewPattern(focusObject.position)) AddRandomPattern();
	}

	private bool NeedsNewPattern(float focusObjectDist) {
		bool needsTopPattern = topTerrain.DistIsWithinDistThreshold(focusObjectDist);
		bool needsBottomPattern = bottomTerrain.DistIsWithinDistThreshold(focusObjectDist);
		return needsTopPattern || needsBottomPattern;
	}

	private bool NeedsNewPattern(Vector2 focusObjectPosition) {
		return NeedsNewPattern(GetDistAtPosition(focusObjectPosition));
	}

	public bool IsValid() {
		return topTerrain.IsValid() && bottomTerrain.IsValid();
	}

	public float GetWidthAtEnd() {
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
		float maxRadius = WhitTerrainPairAttributes.instance.minCurveRadius + GetWidthAtEnd();
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

	public float GetDistAtPosition(Vector2 position) {
		Vector2 startPoint = GetStartPoint();
		Vector2 endPoint = GetEndPoint();

		Vector2 startToPosition = position - startPoint;
		Vector2 startToEnd = endPoint - startPoint;

		float projection = WhitTools.Project(startToPosition, startToEnd);
		float totalLength = startToEnd.magnitude;
		float percent = projection / totalLength;

		float startDist = GetStartDist();
		float totalDist = GetTotalDist();

		float dist = startDist + totalDist * percent;

		return dist;
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
		float width = GetWidthAtEnd();
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