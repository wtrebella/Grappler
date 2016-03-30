using UnityEngine;
using System.Collections;

public class TerrainLineAttributes : ScriptableObject {
	public float sectionLength = 5;
	public float maxBumpHeight = 0.75f;
	public FloatRange sectionSegmentLengthRange = new FloatRange(1.5f, 2.5f);
}