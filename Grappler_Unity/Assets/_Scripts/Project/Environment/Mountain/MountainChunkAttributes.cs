using UnityEngine;
using System.Collections;
using WhitDataTypes;

public class MountainChunkAttributes : ScriptableObject {
	public int numPoints = 10;
	public float maxSlopeChange = 0.15f;
	public float marginSize = 0.01f;
	public FloatRange slopeRange = new FloatRange(0.0f, 0.35f);
	public float maxPerpDist = 0.75f;
	public FloatRange pointDistRange = new FloatRange(1.5f, 2.5f);
}