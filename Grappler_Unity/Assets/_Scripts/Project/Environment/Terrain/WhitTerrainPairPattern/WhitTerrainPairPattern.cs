using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhitTerrainPairPattern {
	private List<WhitTerrainPatternInstructionPair> instructionPairs;

	public WhitTerrainPairPattern() {
		instructionPairs = new List<WhitTerrainPatternInstructionPair>();
	}

	public void AddInstructionPair(WhitTerrainPatternInstructionPair instructionPair) {
		instructionPairs.Add(instructionPair);
	}
}
