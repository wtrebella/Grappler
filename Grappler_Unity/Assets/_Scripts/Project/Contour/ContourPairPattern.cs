using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WhitTerrain {
	public class ContourPairPattern {
		public List<ContourPatternInstructionPair> instructionPairs {get; private set;}

		public ContourPairPattern() {
			
		}

		public ContourPairPattern(params ContourPatternInstructionPair[] pairs) {
			AddInstructionPairs(pairs);
		}

		public void AddInstructionPairs(params ContourPatternInstructionPair[] pairs) {
			for (int i = 0; i < pairs.Length; i++) AddInstructionPair(pairs[i]);
		}
			
		private void AddInstructionPair(ContourPatternInstructionPair instructionPair) {
			if (instructionPairs == null) instructionPairs = new List<ContourPatternInstructionPair>();
			instructionPairs.Add(instructionPair);
		}
	}
}