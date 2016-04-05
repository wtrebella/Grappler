using UnityEngine;
using System.Collections;

public class WhitTerrainLineAttributes : ScriptableObjectSingleton<WhitTerrainLineAttributes> {
	public FloatRange curveRadiusRange = new FloatRange(30.0f, 100.0f);
}