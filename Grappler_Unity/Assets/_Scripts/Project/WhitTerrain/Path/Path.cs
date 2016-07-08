using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace WhitTerrain {
	public class Path : MonoBehaviour {
		public Action<PathPatternType, List<ContourSegment>, List<ContourSegment>> SignalPatternAdded;

		public Contour topContour;
		public Contour bottomContour;

		[SerializeField] private Transform focusObject;

		[SerializeField] private float initialSlope = 0.1f;
		[SerializeField] private float initialWidth = 16.0f;
		[SerializeField] private int numPatternsBeforeEnd = 40;

		private float currentSlope;
		private float currentWidth;
		private List<PathPatternType> patternTypes;

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
			Vector2 topPoint = topContour.GetPointAtDist(dist);
			Vector2 bottomPoint = bottomContour.GetPointAtDist(dist);
			Vector2 averagePoint = WhitTools.GetAveragePoint(topPoint, bottomPoint);
			return averagePoint;
		}

		public float GetTopDistAtBottomDist(float dist) {
			return bottomContour.TranslateDistToContour(dist, topContour);
		}

		public float GetBottomDistAtTopDist(float dist) {
			return topContour.TranslateDistToContour(dist, bottomContour);
		}

		public float GetCurrentWidth() {
			return currentWidth;
		}

		public Vector2 GetStartPoint() {
			return WhitTools.GetAveragePoint(topContour.GetWorldStartPoint(), bottomContour.GetWorldStartPoint());
		}

		public Vector2 GetEndPoint() {
			return WhitTools.GetAveragePoint(topContour.GetWorldEndPoint(), bottomContour.GetWorldEndPoint());
		}

		public Vector2 GetPointAtX(float x) {
			return WhitTools.GetAveragePoint(topContour.GetPointAtX(x), bottomContour.GetPointAtX(x));
		}

		public float GetWidthAtX(float x) {
			Vector2 topPoint = topContour.GetPointAtX(x);
			Vector2 bottomPoint = bottomContour.GetPointAtX(x);
			Vector2 diff = topPoint - bottomPoint;
			float width = diff.magnitude;
			return width;
		}

		public float GetStartDist() {
			float topStartDist = topContour.GetStartDist();
			float bottomStartDist = bottomContour.GetStartDist();
			float startDist = (topStartDist + bottomStartDist) / 2f;
			return startDist;
		}

		public float GetEndDist() {
			float topEndDist = topContour.GetEndDist();
			float bottomEndDist = bottomContour.GetEndDist();
			float endDist = (topEndDist + bottomEndDist) / 2f;
			return endDist;
		}

		public float GetDistLength() {
			float topDistLength = topContour.GetDistLength();
			float bottomDistLength = bottomContour.GetDistLength();
			float distLength = (topDistLength + bottomDistLength) / 2f;
			return distLength;
		}		

		public void AddNextPattern() {
			PathPatternType patternType = GetNextPatternType();

			if (patternType == PathPatternType.Straight) Straight();
			else if (patternType == PathPatternType.Widen) Widen();
			else if (patternType == PathPatternType.Narrow) Narrow();
			else if (patternType == PathPatternType.Bump) Bump();
			else if (patternType == PathPatternType.Flat) Flat();
			else if (patternType == PathPatternType.End) End();
		}

		public PathPatternType GetNextPatternType() {
	//		return WhitTerrainPairPatternType.Straight;

			if (NeedsEnd()) return PathPatternType.End;
			else return PathAttributes.instance.GetRandomPatternType();
		}

		private void Awake() {
			currentSlope = initialSlope;
			currentWidth = initialWidth;
			patternTypes = new List<PathPatternType>();

			InitializeTerrains(Vector2.zero);
		}

		private void InitializeTerrains(Vector2 startPosition) {
			topContour.Initialize(startPosition);
			bottomContour.Initialize(new Vector2(startPosition.x, startPosition.y - currentWidth));
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
			bool needsTopPattern = topContour.DistIsWithinDistThreshold(focusObjectDist);
			bool needsBottomPattern = bottomContour.DistIsWithinDistThreshold(focusObjectDist);
			return needsTopPattern || needsBottomPattern;
		}

		private bool NeedsNewPattern(Vector2 focusObjectPosition) {
			return NeedsNewPattern(GetDistAtPosition(focusObjectPosition));
		}

		public bool IsValid() {
			return topContour.IsValid() && bottomContour.IsValid();
		}

		public float GetWidthAtEnd() {
			return (topContour.GetWorldEndPoint() - bottomContour.GetWorldEndPoint()).magnitude;
		}

		public void RestartTerrain(Vector2 newStartPoint) {
			topContour.ResetTerrain();
			bottomContour.ResetTerrain();
			patternTypes.Clear();
			InitializeTerrains(newStartPoint);
		}

		public bool GetXIsPastEnd(float x) {
			float topEnd = topContour.GetWorldEndPoint().x;
			float bottomEnd = bottomContour.GetWorldEndPoint().x;
			return x > topEnd || x > bottomEnd;
		}

		public bool GetXIsPastStart(float x) {
			float topStart = topContour.GetWorldStartPoint().x;
			float bottomStart = bottomContour.GetWorldStartPoint().x;
			return x > topStart || x > bottomStart;
		}

		public void End() {
			PathPattern pattern = PathPatternGenerator.GetEndPattern(currentSlope, 150, 150, 1f, 30.0f);
			AddPattern(PathPatternType.End, pattern);
		}

		public void Straight() {
			PathPattern pattern = PathPatternGenerator.GetStraightPattern(currentSlope + PathAttributes.instance.slopeVariationRange.GetRandom(), GetTopStraightLength(), GetBottomStraightLength());
			AddPattern(PathPatternType.Straight, pattern);
		}

		public void Flat() {
			PathPattern pattern = PathPatternGenerator.GetFlatPattern(PathAttributes.instance.iceWidthRange.GetRandom());
			AddPattern(PathPatternType.Flat, pattern);
		}

		public void Widen() {
			PathPattern pattern = PathPatternGenerator.GetWidenPattern(currentSlope, 0.2f, 30.0f);
			AddPattern(PathPatternType.Widen, pattern);
		}

		public void Narrow() {
			PathPattern pattern = PathPatternGenerator.GetNarrowPattern(currentSlope, 0.1f, 30.0f);
			AddPattern(PathPatternType.Narrow, pattern);
		}

		public void Bump() {
			float maxRadius = PathAttributes.instance.minCurveRadius + GetWidthAtEnd();
			PathPattern pattern = PathPatternGenerator.GetBumpPattern(currentSlope, 0.3f, 50, 10, 100, PathAttributes.instance.minCurveRadius, maxRadius);
			AddPattern(PathPatternType.Bump, pattern);
		}

		private void AddPattern(PathPatternType patternType, PathPattern pattern) {
			List<ContourSegment> topSections = new List<ContourSegment>();
			List<ContourSegment> bottomSections = new List<ContourSegment>();

			foreach (ContourPatternStepPair instructionPair in pattern.instructionPairs) {
				if (instructionPair.topInstruction.stepType == ContourPatternStepType.Straight) {
					ContourPatternStepStraight topInstruction = (ContourPatternStepStraight)instructionPair.topInstruction;
					var newSection = topContour.AddStraight(topInstruction.slope, topInstruction.length, topInstruction.bumpify);
					if (newSection) topSections.Add(newSection);
				}
				else if (instructionPair.topInstruction.stepType == ContourPatternStepType.Curve) {
					ContourPatternStepCurve topInstruction = (ContourPatternStepCurve)instructionPair.topInstruction;
					var newSections = topContour.AddCurve(topInstruction.targetSlope, topInstruction.radius, topInstruction.bumpify);
					if (newSections.Count > 0) topSections.AddAll(newSections);
				}

				if (instructionPair.bottomInstruction.stepType == ContourPatternStepType.Straight) {
					ContourPatternStepStraight bottomInstruction = (ContourPatternStepStraight)instructionPair.bottomInstruction;
					var newSection = bottomContour.AddStraight(bottomInstruction.slope, bottomInstruction.length, bottomInstruction.bumpify);
					if (newSection) bottomSections.Add(newSection);
				}
				else if (instructionPair.bottomInstruction.stepType == ContourPatternStepType.Curve) {
					ContourPatternStepCurve bottomInstruction = (ContourPatternStepCurve)instructionPair.bottomInstruction;
					var newSections = bottomContour.AddCurve(bottomInstruction.targetSlope, bottomInstruction.radius, bottomInstruction.bumpify);
					if (newSections.Count > 0) bottomSections.AddAll(newSections);
				}
			}
				
			OnPatternAdded(patternType, topSections, bottomSections);
		}

		private void OnPatternAdded(PathPatternType patternType, List<ContourSegment> topSections, List<ContourSegment> bottomSections) {
			patternTypes.Add(patternType);
			if (SignalPatternAdded != null) SignalPatternAdded(patternType, topSections, bottomSections);
		}

		public bool HasEnd() {
			if (patternTypes.Count == 0) return false;
			return patternTypes.GetLast() == PathPatternType.End;
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
			Vector2 topDirection = topContour.GetLastSectionDirection();
			Vector2 bottomDirection = bottomContour.GetLastSectionDirection();
			Vector2 averageDirection = WhitTools.GetAveragePoint(topDirection, bottomDirection).normalized;
			return averageDirection;
		}

		private Vector2 GetDirectionBetweenEndPoints() {
			Vector2 topEndPoint = topContour.GetWorldEndPoint();
			Vector2 bottomEndPoint = bottomContour.GetWorldEndPoint();

			Vector2 endPointVector = topEndPoint - bottomEndPoint;
			Vector2 endPointDirection = endPointVector.normalized;

			return endPointDirection;
		}

		private float GetTopStraightLength() {
			return PathAttributes.instance.straightLength;
		}

		private float GetBottomStraightLength() {
			float width = GetWidthAtEnd();
			Vector2 directionAtEnd = GetDirectionAtEnd();

			Vector2 targetDirectionBetweenEndPoints = new Vector2(directionAtEnd.y, -directionAtEnd.x);

			Vector2 topEndPoint = topContour.GetWorldEndPoint();
			Vector2 bottomEndPoint = bottomContour.GetWorldEndPoint();

			float topLength = GetTopStraightLength();
			Vector2 newTopEndPoint = topEndPoint + directionAtEnd * topLength;
			Vector2 newBottomEndPoint = newTopEndPoint + targetDirectionBetweenEndPoints * width;
			float bottomLength = (newBottomEndPoint - bottomEndPoint).magnitude;
			return bottomLength;
		}
	}
}