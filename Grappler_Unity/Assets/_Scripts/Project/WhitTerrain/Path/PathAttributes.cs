using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhitDataTypes;

namespace WhitTerrain {
	public class PathAttributes : ScriptableObjectSingleton<PathAttributes> {
		public FloatRange slopeVariationRange = new FloatRange(-0.2f, 0.2f);
		public FloatRange widthRange = new FloatRange(6.0f, 20.0f);
		public FloatRange iceWidthRange = new FloatRange(100.0f, 200.0f);

		public float drawDistWidth = 200.0f;
		public float playerDistThreshold = 30.0f;
		public float straightLength = 30.0f;
		public float minCurveRadius = 20.0f;

		[SerializeField] private int continuePatternWeight = 10;
		[SerializeField] private int widenPatternWeight = 2;
		[SerializeField] private int narrowPatternWeight = 2;
		[SerializeField] private int bumpPatternWeight = 1;
		[SerializeField] private int flatPatternWeight = 3;
		[SerializeField] private int openingPatternWeight = 1;

		public PathPatternType GetRandomPatternType() {
			List<PathPatternType> patternTypes = new List<PathPatternType>();
			for (int i = 0; i < continuePatternWeight; i++) patternTypes.Add(PathPatternType.Straight);
			for (int i = 0; i < widenPatternWeight; i++) patternTypes.Add(PathPatternType.Widen);
			for (int i = 0; i < narrowPatternWeight; i++) patternTypes.Add(PathPatternType.Narrow);
			for (int i = 0; i < bumpPatternWeight; i++) patternTypes.Add(PathPatternType.Bump);
			for (int i = 0; i < flatPatternWeight; i++) patternTypes.Add(PathPatternType.Flat);
			return patternTypes[Random.Range(0, patternTypes.Count)];
		}
	}
}