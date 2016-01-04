using UnityEngine;
using System.Collections;
using Vectrosity;
using System.Collections.Generic;

[RequireComponent(typeof(GrapplerArmGrabber))]
public class GrapplerArm : MonoBehaviour {
	public bool isGrabbing {get; private set;}

	private GrapplerArmGrabber grip;

	public void Grab() {
		isGrabbing = true;
		grip.Grab();
	}

	public void Release() {
		isGrabbing = false;
		grip.Release();
	}

	private void Awake() {
		grip = GetComponent<GrapplerArmGrabber>();
	}
}
