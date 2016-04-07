using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class WhitTerrainPatternInstruction {
	public WhitTerrainPatternInstructionType instructionType = WhitTerrainPatternInstructionType.None;

	public float slope = 0.0f;
	public float length = 10.0f;
	public float radius = 30.0f;
}