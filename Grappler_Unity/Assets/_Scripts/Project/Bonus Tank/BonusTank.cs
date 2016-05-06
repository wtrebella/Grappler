using UnityEngine;
using System.Collections;
using System;

public class BonusTank : MonoBehaviour {
	public const int capacity = 100;

	public Action SignalTankLevelChanged;
	public Action SignalTankFull;
	public Action SignalTankEmpty;
	public Action SignalTankLostFull;

	public float percentFull {
		get {
			float percent = (float)level / (float)capacity;
			return percent;
		}
	}

	private int level = 0;

	public void Empty() {
		Set(0);
	}

	public void Fill() {
		Set(capacity);
	}

	public void Add(int amt) {
		Set(level + amt);
	}

	public void Set(int amt) {
		int previousLevel = level;
		level = ClampToCapacity(amt);
		FireSignals(previousLevel);
	}

	private int ClampToCapacity(int amt) {
		return Mathf.Clamp(amt, 0, capacity);
	}

	private void FireSignals(int previousLevel) {
		if (level != previousLevel) {
			OnChange();
			if (previousLevel == capacity) OnLostFull();

			if (level == 0) OnEmpty();
			else if (level == capacity) OnFull();
		}
	}

	private void OnChange() {
		if (SignalTankLevelChanged != null) SignalTankLevelChanged();
	}

	private void OnFull() {
		if (SignalTankFull != null) SignalTankFull();
	}

	private void OnEmpty() {
		if (SignalTankEmpty != null) SignalTankEmpty();
	}

	private void OnLostFull() {
		if (SignalTankLostFull != null) SignalTankLostFull();
	}
}
