using UnityEngine;
using System.Collections;
using Vectrosity;
using System.Collections.Generic;

public enum ArmType {
	Left,
	Right
}

[RequireComponent(typeof(GrapplerArmEndPoints))]
public class GrapplerArm : MonoBehaviour {
	[SerializeField] private ArmType armType;
	
	public ArmType GetArmType() {
		return armType;
	}
}
