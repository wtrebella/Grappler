using UnityEngine;
using System.Collections;
using WhitDataTypes;

namespace WhitTerrain {
	public class ContourSegmentAttributes : ScriptableObject {
		public float maxBumpHeight = 0.75f;
		public FloatRange sectionSegmentLengthRange = new FloatRange(1.5f, 2.5f);
	}
}