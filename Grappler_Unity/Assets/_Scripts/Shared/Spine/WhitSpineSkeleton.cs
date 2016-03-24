using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine;
using System.Linq;
using UnityEngine.Events;

public class WhitSpineSkeleton : MonoBehaviour {
	public SkeletonAnimation spineSkeleton;

	private Dictionary<string, WhitSpineSlot> slots;

	private void Awake() {
		InitializeSlots();
	}

	private void InitializeSlots() {
		var allSlots = GetComponentsInChildren<WhitSpineSlot>();
		foreach (WhitSpineSlot slot in allSlots) AddSlot(slot);
	}
		
	public WhitSpineSlot GetSlot(string slotName) {
		WhitSpineSlot slot;
		if (slots.TryGetValue(slotName, out slot)) return slot;
		else {
			Debug.LogError("no slot with name: " + slotName);
			return null;
		}
	}

	private WhitSpineSlot AddSlot(WhitSpineSlot slot) {
		if (slots == null) slots = new Dictionary<string, WhitSpineSlot>();
		if (spineSkeleton == null) {
			Debug.LogError("set the skeleton before adding slots!");
			return null;
		}
		slots.Add(slot.slotName, slot);
		return slot;
	}
}
