using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WhitTerrain {
	public class PathPattern {
		public List<ContourPatternStepPair> instructionPairs {get; private set;}

		public PathPattern() {
			
		}

		public PathPattern(params ContourPatternStepPair[] pairs) {
			AddInstructionPairs(pairs);
		}

		public void AddInstructionPairs(params ContourPatternStepPair[] pairs) {
			for (int i = 0; i < pairs.Length; i++) AddInstructionPair(pairs[i]);
		}
			
		private void AddInstructionPair(ContourPatternStepPair instructionPair) {
			if (instructionPairs == null) instructionPairs = new List<ContourPatternStepPair>();
			instructionPairs.Add(instructionPair);
		}
	}
}