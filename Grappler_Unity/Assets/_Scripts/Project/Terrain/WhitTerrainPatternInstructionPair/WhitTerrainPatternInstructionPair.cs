using UnityEngine;
using System.Collections;

public class WhitTerrainPatternInstructionPair {
	public WhitTerrainPatternInstruction topInstruction;
	public WhitTerrainPatternInstruction bottomInstruction;

	public WhitTerrainPatternInstructionPair(WhitTerrainPatternInstruction topInstruction, WhitTerrainPatternInstruction bottomInstruction) {
		this.topInstruction = topInstruction;
		this.bottomInstruction = bottomInstruction;
	}
}