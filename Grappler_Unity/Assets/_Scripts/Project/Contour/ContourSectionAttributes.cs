using UnityEngine;
using System.Collections;

public class ContourSectionAttributes : ScriptableObject {
	public float maxBumpHeight = 0.75f;
	public FloatRange sectionSegmentLengthRange = new FloatRange(1.5f, 2.5f);
}