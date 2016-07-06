using UnityEngine;
using System.Collections;

public class ContourPatternInstructionPair {
	public ContourPatternInstruction topInstruction;
	public ContourPatternInstruction bottomInstruction;

	public ContourPatternInstructionPair(ContourPatternInstruction topInstruction, ContourPatternInstruction bottomInstruction) {
		this.topInstruction = topInstruction;
		this.bottomInstruction = bottomInstruction;
	}
}