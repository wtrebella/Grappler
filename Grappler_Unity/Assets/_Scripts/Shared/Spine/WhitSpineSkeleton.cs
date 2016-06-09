using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine;
using System.Linq;
using UnityEngine.Events;

public class WhitSpineSkeleton : MonoBehaviour {
	public SkeletonAnimation spineSkeleton;

	private Dictionary<string, WhitSpineSlot> slots;
	private bool initialized = false;

	private void Awake() {
		if (!initialized) InitializeSlots();
	}

	private void InitializeSlots() {
		slots = new Dictionary<string, WhitSpineSlot>();
		var allSlots = GetComponentsInChildren<WhitSpineSlot>();
		foreach (WhitSpineSlot slot in allSlots) AddSlot(slot);
		initialized = true;
	}
		
	public WhitSpineSlot GetSlot(string slotName) {
		if (!initialized) InitializeSlots();

		WhitSpineSlot slot;
		if (slots.TryGetValue(slotName, out slot)) return slot;
		else {
			Debug.LogError("no slot with name: " + slotName);
			return null;
		}
	}

	private WhitSpineSlot AddSlot(WhitSpineSlot slot) {
		if (spineSkeleton == null) {
			Debug.LogError("set the skeleton before adding slots!");
			return null;
		}
		slots.Add(slot.slotName, slot);
		return slot;
	}
}
