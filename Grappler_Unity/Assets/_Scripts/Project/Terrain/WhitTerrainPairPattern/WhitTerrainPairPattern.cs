using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhitTerrainPairPattern {
	public List<WhitTerrainPatternInstructionPair> instructionPairs {get; private set;}

	public WhitTerrainPairPattern() {
		
	}

	public WhitTerrainPairPattern(params WhitTerrainPatternInstructionPair[] pairs) {
		AddInstructionPairs(pairs);
	}

	public void AddInstructionPairs(params WhitTerrainPatternInstructionPair[] pairs) {
		for (int i = 0; i < pairs.Length; i++) AddInstructionPair(pairs[i]);
	}
		
	private void AddInstructionPair(WhitTerrainPatternInstructionPair instructionPair) {
		if (instructionPairs == null) instructionPairs = new List<WhitTerrainPatternInstructionPair>();
		instructionPairs.Add(instructionPair);
	}
}
