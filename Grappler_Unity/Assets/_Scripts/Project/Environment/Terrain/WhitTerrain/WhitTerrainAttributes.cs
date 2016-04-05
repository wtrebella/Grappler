using UnityEngine;
using System.Collections;

public class WhitTerrainAttributes : ScriptableObjectSingleton<WhitTerrainAttributes> {
	public FloatRange curveRadiusRange = new FloatRange(30.0f, 100.0f);
}