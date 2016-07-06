using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace WhitTerrain {
	public class ContourPair : MonoBehaviour {
		public Action<ContourPairPatternType, List<ContourSection>, List<ContourSection>> SignalPatternAdded;

		public Contour topTerrain;
		public Contour bottomTerrain;

		[SerializeField] private Transform focusObject;

		[SerializeField] private float initialSlope = 0.1f;
		[SerializeField] private float initialWidth = 16.0f;
		[SerializeField] private int numPatternsBeforeEnd = 40;

		private float currentSlope;
		private float currentWidth;
		private List<ContourPairPatternType> patternTypes;

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
			return WhitTools.GetAveragePoint(topTerrain.GetWorldStartPoint(), bottomTerrain.GetWorldStartPoint());
		}

		public Vector2 GetEndPoint() {
			return WhitTools.GetAveragePoint(topTerrain.GetWorldEndPoint(), bottomTerrain.GetWorldEndPoint());
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
			ContourPairPatternType patternType = GetNextPatternType();

			if (patternType == ContourPairPatternType.Straight) Straight();
			else if (patternType == ContourPairPatternType.Widen) Widen();
			else if (patternType == ContourPairPatternType.Narrow) Narrow();
			else if (patternType == ContourPairPatternType.Bump) Bump();
			else if (patternType == ContourPairPatternType.Flat) Flat();
			else if (patternType == ContourPairPatternType.End) End();
		}

		public ContourPairPatternType GetNextPatternType() {
	//		return WhitTerrainPairPatternType.Straight;

			if (NeedsEnd()) return ContourPairPatternType.End;
			else return ContourPairAttributes.instance.GetRandomPatternType();
		}

		private void Awake() {
			currentSlope = initialSlope;
			currentWidth = initialWidth;
			patternTypes = new List<ContourPairPatternType>();

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
			return (topTerrain.GetWorldEndPoint() - bottomTerrain.GetWorldEndPoint()).magnitude;
		}

		public void RestartTerrain(Vector2 newStartPoint) {
			topTerrain.ResetTerrain();
			bottomTerrain.ResetTerrain();
			patternTypes.Clear();
			InitializeTerrains(newStartPoint);
		}

		public bool GetXIsPastEnd(float x) {
			float topEnd = topTerrain.GetWorldEndPoint().x;
			float bottomEnd = bottomTerrain.GetWorldEndPoint().x;
			return x > topEnd || x > bottomEnd;
		}

		public bool GetXIsPastStart(float x) {
			float topStart = topTerrain.GetWorldStartPoint().x;
			float bottomStart = bottomTerrain.GetWorldStartPoint().x;
			return x > topStart || x > bottomStart;
		}

		public void End() {
			ContourPairPattern pattern = ContourPairPatternGenerator.GetEndPattern(currentSlope, 150, 150, 1f, 30.0f);
			AddPattern(ContourPairPatternType.End, pattern);
		}

		public void Straight() {
			ContourPairPattern pattern = ContourPairPatternGenerator.GetStraightPattern(currentSlope + ContourPairAttributes.instance.slopeVariationRange.GetRandom(), GetTopStraightLength(), GetBottomStraightLength());
			AddPattern(ContourPairPatternType.Straight, pattern);
		}

		public void Flat() {
			ContourPairPattern pattern = ContourPairPatternGenerator.GetFlatPattern(ContourPairAttributes.instance.iceWidthRange.GetRandom());
			AddPattern(ContourPairPatternType.Flat, pattern);
		}

		public void Widen() {
			ContourPairPattern pattern = ContourPairPatternGenerator.GetWidenPattern(currentSlope, 0.2f, 30.0f);
			AddPattern(ContourPairPatternType.Widen, pattern);
		}

		public void Narrow() {
			ContourPairPattern pattern = ContourPairPatternGenerator.GetNarrowPattern(currentSlope, 0.1f, 30.0f);
			AddPattern(ContourPairPatternType.Narrow, pattern);
		}

		public void Bump() {
			float maxRadius = ContourPairAttributes.instance.minCurveRadius + GetWidthAtEnd();
			ContourPairPattern pattern = ContourPairPatternGenerator.GetBumpPattern(currentSlope, 0.3f, 50, 10, 100, ContourPairAttributes.instance.minCurveRadius, maxRadius);
			AddPattern(ContourPairPatternType.Bump, pattern);
		}

		private void AddPattern(ContourPairPatternType patternType, ContourPairPattern pattern) {
			List<ContourSection> topSections = new List<ContourSection>();
			List<ContourSection> bottomSections = new List<ContourSection>();

			foreach (ContourPatternInstructionPair instructionPair in pattern.instructionPairs) {
				if (instructionPair.topInstruction.instructionType == ContourPatternInstructionType.Straight) {
					ContourPatternInstructionStraight topInstruction = (ContourPatternInstructionStraight)instructionPair.topInstruction;
					var newSection = topTerrain.AddStraight(topInstruction.slope, topInstruction.length, topInstruction.bumpify);
					if (newSection) topSections.Add(newSection);
				}
				else if (instructionPair.topInstruction.instructionType == ContourPatternInstructionType.Curve) {
					ContourPatternInstructionCurve topInstruction = (ContourPatternInstructionCurve)instructionPair.topInstruction;
					var newSections = topTerrain.AddCurve(topInstruction.targetSlope, topInstruction.radius, topInstruction.bumpify);
					if (newSections.Count > 0) topSections.AddAll(newSections);
				}

				if (instructionPair.bottomInstruction.instructionType == ContourPatternInstructionType.Straight) {
					ContourPatternInstructionStraight bottomInstruction = (ContourPatternInstructionStraight)instructionPair.bottomInstruction;
					var newSection = bottomTerrain.AddStraight(bottomInstruction.slope, bottomInstruction.length, bottomInstruction.bumpify);
					if (newSection) bottomSections.Add(newSection);
				}
				else if (instructionPair.bottomInstruction.instructionType == ContourPatternInstructionType.Curve) {
					ContourPatternInstructionCurve bottomInstruction = (ContourPatternInstructionCurve)instructionPair.bottomInstruction;
					var newSections = bottomTerrain.AddCurve(bottomInstruction.targetSlope, bottomInstruction.radius, bottomInstruction.bumpify);
					if (newSections.Count > 0) bottomSections.AddAll(newSections);
				}
			}
				
			OnPatternAdded(patternType, topSections, bottomSections);
		}

		private void OnPatternAdded(ContourPairPatternType patternType, List<ContourSection> topSections, List<ContourSection> bottomSections) {
			patternTypes.Add(patternType);
			if (SignalPatternAdded != null) SignalPatternAdded(patternType, topSections, bottomSections);
		}

		public bool HasEnd() {
			if (patternTypes.Count == 0) return false;
			return patternTypes.GetLast() == ContourPairPatternType.End;
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
			Vector2 topEndPoint = topTerrain.GetWorldEndPoint();
			Vector2 bottomEndPoint = bottomTerrain.GetWorldEndPoint();

			Vector2 endPointVector = topEndPoint - bottomEndPoint;
			Vector2 endPointDirection = endPointVector.normalized;

			return endPointDirection;
		}

		private float GetTopStraightLength() {
			return ContourPairAttributes.instance.straightLength;
		}

		private float GetBottomStraightLength() {
			float width = GetWidthAtEnd();
			Vector2 directionAtEnd = GetDirectionAtEnd();

			Vector2 targetDirectionBetweenEndPoints = new Vector2(directionAtEnd.y, -directionAtEnd.x);

			Vector2 topEndPoint = topTerrain.GetWorldEndPoint();
			Vector2 bottomEndPoint = bottomTerrain.GetWorldEndPoint();

			float topLength = GetTopStraightLength();
			Vector2 newTopEndPoint = topEndPoint + directionAtEnd * topLength;
			Vector2 newBottomEndPoint = newTopEndPoint + targetDirectionBetweenEndPoints * width;
			float bottomLength = (newBottomEndPoint - bottomEndPoint).magnitude;
			return bottomLength;
		}
	}
}