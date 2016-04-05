using UnityEngine;
using System.Collections;

public class WhitTerrainAttributes : ScriptableObjectSingleton<WhitTerrainAttributes> {
	public FloatRange slopeVariationRange = new FloatRange(-0.2f, 0.2f);
}