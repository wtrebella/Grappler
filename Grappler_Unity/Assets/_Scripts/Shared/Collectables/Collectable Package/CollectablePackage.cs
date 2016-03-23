using UnityEngine;
using System.Collections;

[System.Serializable]
public class CollectablePackage : ScriptableObject {
	public bool isLocked {get; private set;}

	public bool isLockedByDefault = true;
	public CollectableItem[] items;
	public CollectablePackageType type = CollectablePackageType.None;

	private string _packageName;
	public string packageName {
		get {
			if (string.IsNullOrEmpty(_packageName)) {
				string[] nameParts = name.Split('_');
				string justName = nameParts[nameParts.Length - 1];
				_packageName = justName;
			}
			return _packageName;
		}
	}

	public CollectableItem GetFirstItem() {
		if (items == null || items.Length == 0) return null;
		return items[0];
	}

	public void ClearLockedData() {
		string key = GetIsLockedKey();
		WhitPrefs.RemoveObjectForKey(key);
		Debug.Log("key \"" + key + "\" was deleted!");
	}

	private void OnEnable() {
		isLocked = GetIsLockedSave();
	}

	private void OnDisable() {
		WhitPrefs.SetBool(GetIsLockedKey(), PrefsBoolPriority.False, isLocked);
	}

	private bool GetIsLockedSave() {
		return WhitPrefs.GetBool(GetIsLockedKey(), PrefsBoolPriority.False, isLocked, isLockedByDefault);
	}
		
	private string GetIsLockedKey() {
		return packageName + "_isLocked";
	}
}