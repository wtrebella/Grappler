using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WhitTerrainPair : MonoBehaviour {
	public Action<WhitTerrainPairPatternType, List<WhitTerrainSection>, List<WhitTerrainSection>> SignalPatternAdded;

	public WhitTerrain topTerrain;
	public WhitTerrain bottomTerrain;

	[SerializeField] private Transform focusObject;

	[SerializeField] private float initialSlope = 0.1f;
	[SerializeField] private float initialWidth = 16.0f;
	[SerializeField] private int numPatternsBeforeEnd = 40;

	private float currentSlope;
	private float currentWidth;
	private List<WhitTerrainPairPatternType> patternTypes;

	public Vector2 GetThroughDirection(Vector3 position) {
		float dist = GetDistAtPosition(position);
		Vector2 distPoint = GetPointAtDist(dist);
		Vector2 endPoint = GetEndPoint();
		Vector2 delta = endPoint - distPoint;
		Vector2 targetDirection = delta.normalized;
		return targetDirection;
	}

	public float GetThroughSlope(Vector3 position) {
		Vector2 direction = GetThroughDirection(position);
		float slope = WhitTools.DirectionToSlope(direction);
		return slope;
	}

	public Vector2 GetPointAtDist(float dist) {
		Vector2 topPoint = topTerrain.GetPointAtDist(dist);
		Vector2 bottomPoint = bottomTerrain.GetPointAtDist(dist);
		Vector2 averagePoint = WhitTools.GetAveragePoint(topPoint, bottomPoint);
		return averagePoint;
	}

	public float GetCurrentWidth() {
		return currentWidth;
	}

	public Vector2 GetStartPoint() {
		return WhitTools.GetAveragePoint(topTerrain.GetStartPoint(), bottomTerrain.GetStartPoint());
	}

	public Vector2 GetEndPoint() {
		return WhitTools.GetAveragePoint(topTerrain.GetEndPoint(), bottomTerrain.GetEndPoint());
	}

	public Vector2 GetPointAtX(float x) {
		return WhitTools.GetAveragePoint(topTerrain.GetPointAtX(x), bottomTerrain.GetPointAtX(x));
	}

	public float GetWidthAtX(float x) {
		Vector2 topPoint = topTerrain.GetPointAtX(x);
		Vector2 bottomPoint = bottomTerrain.GetPointAtX(x);
		Vector2 diff = topPoint - bottomPoint;
		float width = diff.magnitude;
		return width;
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

	public float GetDistLength() {
		float topDistLength = topTerrain.GetDistLength();
		float bottomDistLength = bottomTerrain.GetDistLength();
		float distLength = (topDistLength + bottomDistLength) / 2f;
		return distLength;
	}		

	public void AddNextPattern() {
		WhitTerrainPairPatternType patternType = GetNextPatternType();

		if (patternType == WhitTerrainPairPatternType.Straight) Straight();
		else if (patternType == WhitTerrainPairPatternType.Widen) Widen();
		else if (patternType == WhitTerrainPairPatternType.Narrow) Narrow();
		else if (patternType == WhitTerrainPairPatternType.Bump) Bump();
		else if (patternType == WhitTerrainPairPatternType.Flat) Flat();
		else if (patternType == WhitTerrainPairPatternType.End) End();
	}

	public WhitTerrainPairPatternType GetNextPatternType() {
		if (NeedsEnd()) return WhitTerrainPairPatternType.End;
		else return WhitTerrainPairAttributes.instance.GetRandomPatternType();
	}

	private void Awake() {
		currentSlope = initialSlope;
		currentWidth = initialWidth;
		patternTypes = new List<WhitTerrainPairPatternType>();

		InitializeTerrains(Vector2.zero);
	}

	private void InitializeTerrains(Vector2 startPosition) {
		topTerrain.Initialize(startPosition);
		bottomTerrain.Initialize(new Vector2(startPosition.x, startPosition.y - currentWidth));
	}

	private void Update() {
		if (NeedsNewPattern(focusObject.position)) AddNextPattern();
	}

	private bool NeedsEnd() {
		return patternTypes.Count >= numPatternsBeforeEnd;
	}

	private bool NeedsNewPattern(float focusObjectDist) {
		bool hasEnd = HasEnd();
		if (hasEnd) return false;
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

	public void RestartTerrain(Vector2 newStartPoint) {
		topTerrain.ResetTerrain();
		bottomTerrain.ResetTerrain();
		patternTypes.Clear();
		InitializeTerrains(newStartPoint);
	}

	public bool GetXIsPastEnd(float x) {
		float topEnd = topTerrain.GetEndPoint().x;
		float bottomEnd = bottomTerrain.GetEndPoint().x;
		return x > topEnd || x > bottomEnd;
	}

	public bool GetXIsPastStart(float x) {
		float topStart = topTerrain.GetStartPoint().x;
		float bottomStart = bottomTerrain.GetStartPoint().x;
		return x > topStart || x > bottomStart;
	}

	public void End() {
		WhitTerrainPairPattern pattern = WhitTerrainPairPatternGenerator.GetEndPattern(currentSlope, 150, 150, 1f, 30.0f);
		AddPattern(WhitTerrainPairPatternType.End, pattern);
	}

	public void Straight() {
		WhitTerrainPairPattern pattern = WhitTerrainPairPatternGenerator.GetStraightPattern(currentSlope + WhitTerrainPairAttributes.instance.slopeVariationRange.GetRandom(), GetTopStraightLength(), GetBottomStraightLength());
		AddPattern(WhitTerrainPairPatternType.Straight, pattern);
	}

	public void Flat() {
		WhitTerrainPairPattern pattern = WhitTerrainPairPatternGenerator.GetFlatPattern(WhitTerrainPairAttributes.instance.iceWidthRange.GetRandom());
		AddPattern(WhitTerrainPairPatternType.Flat, pattern);
	}

	public void Widen() {
		WhitTerrainPairPattern pattern = WhitTerrainPairPatternGenerator.GetWidenPattern(currentSlope, 0.2f, 30.0f);
		AddPattern(WhitTerrainPairPatternType.Widen, pattern);
	}

	public void Narrow() {
		WhitTerrainPairPattern pattern = WhitTerrainPairPatternGenerator.GetNarrowPattern(currentSlope, 0.1f, 30.0f);
		AddPattern(WhitTerrainPairPatternType.Narrow, pattern);
	}

	public void Bump() {
		float maxRadius = WhitTerrainPairAttributes.instance.minCurveRadius + GetWidthAtEnd();
		WhitTerrainPairPattern pattern = WhitTerrainPairPatternGenerator.GetBumpPattern(currentSlope, 0.3f, WhitTerrainPairAttributes.instance.minCurveRadius, maxRadius);
		AddPattern(WhitTerrainPairPatternType.Bump, pattern);
	}

	private void AddPattern(WhitTerrainPairPatternType patternType, WhitTerrainPairPattern pattern) {
		List<WhitTerrainSection> topSections = new List<WhitTerrainSection>();
		List<WhitTerrainSection> bottomSections = new List<WhitTerrainSection>();

		foreach (WhitTerrainPatternInstructionPair instructionPair in pattern.instructionPairs) {
			if (instructionPair.topInstruction.instructionType == WhitTerrainPatternInstructionType.Straight) {
				WhitTerrainPatternInstructionStraight topInstruction = (WhitTerrainPatternInstructionStraight)instructionPair.topInstruction;
				var newSection = topTerrain.AddStraight(topInstruction.slope, topInstruction.length, topInstruction.bumpify);
				if (newSection) topSections.Add(newSection);
			}
			else if (instructionPair.topInstruction.instructionType == WhitTerrainPatternInstructionType.Curve) {
				WhitTerrainPatternInstructionCurve topInstruction = (WhitTerrainPatternInstructionCurve)instructionPair.topInstruction;
				var newSections = topTerrain.AddCurve(topInstruction.targetSlope, topInstruction.radius, topInstruction.bumpify);
				if (newSections.Count > 0) topSections.AddAll(newSections);
			}

			if (instructionPair.bottomInstruction.instructionType == WhitTerrainPatternInstructionType.Straight) {
				WhitTerrainPatternInstructionStraight bottomInstruction = (WhitTerrainPatternInstructionStraight)instructionPair.bottomInstruction;
				var newSection = bottomTerrain.AddStraight(bottomInstruction.slope, bottomInstruction.length, bottomInstruction.bumpify);
				if (newSection) bottomSections.Add(newSection);
			}
			else if (instructionPair.bottomInstruction.instructionType == WhitTerrainPatternInstructionType.Curve) {
				WhitTerrainPatternInstructionCurve bottomInstruction = (WhitTerrainPatternInstructionCurve)instructionPair.bottomInstruction;
				var newSections = bottomTerrain.AddCurve(bottomInstruction.targetSlope, bottomInstruction.radius, bottomInstruction.bumpify);
				if (newSections.Count > 0) bottomSections.AddAll(newSections);
			}
		}
			
		OnPatternAdded(patternType, topSections, bottomSections);
	}

	private void OnPatternAdded(WhitTerrainPairPatternType patternType, List<WhitTerrainSection> topSections, List<WhitTerrainSection> bottomSections) {
		patternTypes.Add(patternType);
		if (SignalPatternAdded != null) SignalPatternAdded(patternType, topSections, bottomSections);
	}

	public bool HasEnd() {
		if (patternTypes.Count == 0) return false;
		return patternTypes.GetLast() == WhitTerrainPairPatternType.End;
	}

	public float GetDistAtPosition(Vector2 position) {
		Vector2 startPoint = GetStartPoint();
		Vector2 endPoint = GetEndPoint();

		Vector2 startToPosition = position - startPoint;
		Vector2 startToEnd = endPoint - startPoint;

		float projection = WhitTools.Project(startToEnd, startToPosition);
		float totalMagnitude = startToEnd.magnitude;
		float percent = projection / totalMagnitude;

		float startDist = GetStartDist();
		float distLength = GetDistLength();

		float dist = startDist + distLength * percent;

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
}