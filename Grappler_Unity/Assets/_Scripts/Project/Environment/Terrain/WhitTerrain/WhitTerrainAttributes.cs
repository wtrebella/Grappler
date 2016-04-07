using UnityEngine;
using System.Collections;

public class WhitTerrainAttributes : ScriptableObjectSingleton<WhitTerrainAttributes> {
	public FloatRange slopeVariationRange = new FloatRange(-0.2f, 0.2f);
	public float drawDistWidth = 100.0f;
	public float playerDistThreshold = 30.0f;
}