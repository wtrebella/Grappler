using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Overheat : MonoBehaviour {
	public Action SignalChange;
	public Action SignalOverheat;
	public Action SignalEmptyAfterOverheat;

	[SerializeField] private GrapplingManager grapplingManager;
	[SerializeField] private float amtPerGrapple = 0.1f;
	[SerializeField] private float drainRate = 1.0f;

	private float tank = 0.0f;
	private bool overheated = false;

	public float GetTankValue() {
		return Mathf.Clamp01(tank);
	}

	private void Awake() {
		grapplingManager.SignalGrapple += OnGrapple;
	}

	private void OnGrapple() {
		tank += amtPerGrapple;
		if (SignalChange != null) SignalChange();
		if (!overheated) CheckForOverheat();
	}

	private void Update() {
		Drain();
	}

	private void CheckForOverheat() {
		if (tank >= 1.0f) {
			tank = 1.0f;
			overheated = true;
			grapplingManager.DisableGrappling();
			if (SignalOverheat != null) SignalOverheat();
		}
	}

	private void Drain() {
		tank -= drainRate * Time.deltaTime;
		if (tank <= 0.0f) {
			tank = 0.0f;
			if (overheated) OnEmptyAfterOverheat();
		}
		if (SignalChange != null) SignalChange();
	}

	private void OnEmptyAfterOverheat() {
		overheated = false;
		grapplingManager.EnableGrappling();
		if (SignalEmptyAfterOverheat != null) SignalEmptyAfterOverheat();
	}
}
