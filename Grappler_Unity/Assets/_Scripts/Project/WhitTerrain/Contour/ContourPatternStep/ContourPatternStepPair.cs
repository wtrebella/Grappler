using UnityEngine;
using System.Collections;

namespace WhitTerrain {
	public class ContourPatternStepPair {
		public ContourPatternStep topInstruction;
		public ContourPatternStep bottomInstruction;

		public ContourPatternStepPair(ContourPatternStep topInstruction, ContourPatternStep bottomInstruction) {
			this.topInstruction = topInstruction;
			this.bottomInstruction = bottomInstruction;
		}
	}
}