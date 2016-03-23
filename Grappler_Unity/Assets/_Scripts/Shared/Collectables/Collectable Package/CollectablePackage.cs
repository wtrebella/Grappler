using UnityEngine;
using System.Collections;

[System.Serializable]
public class CollectablePackage : ScriptableObject {
	private bool _isLocked;
	public bool isLocked {
		get {
			if (!hasLoadedLockedStatus) LoadLockedStatus();
			return _isLocked;
		}
	}

	private bool _isEquipped;
	public bool isEquipped {
		get {
			if (hasLoadedEquippedStatus) LoadEquippedStatus();
			return _isEquipped;
		}
	}

	public bool isLockedByDefault = true;
	public bool isEquippedByDefault = false;
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

	private bool hasLoadedLockedStatus = false;
	private bool hasLoadedEquippedStatus = false;

	public CollectableItem GetFirstItem() {
		if (items == null || items.Length == 0) return null;
		return items[0];
	}

	public void ClearLockedData() {
		string key = GetKeyIsLocked();
		WhitPrefs.RemoveObjectForKey(key);
		Debug.Log("key \"" + key + "\" was deleted!");
	}

	public void ClearEquippedData() {
		string key = GetKeyIsLocked();
		WhitPrefs.RemoveObjectForKey(key);
		Debug.Log("key \"" + key + "\" was deleted!");
	}

	private void OnDisable() {
		SaveLockedStatus();
		SaveEquippedStatus();
	}

	private void SaveEquippedStatus() {
		WhitPrefs.SetBool(GetKeyIsEquipped(), PrefsBoolPriority.New, _isEquipped);
	}

	private bool LoadEquippedStatus() {
		hasLoadedEquippedStatus = true;
		return WhitPrefs.GetBool(GetKeyIsLocked(), PrefsBoolPriority.New, _isEquipped, isEquippedByDefault);
	}

	private void SaveLockedStatus() {
		WhitPrefs.SetBool(GetKeyIsLocked(), PrefsBoolPriority.False, _isLocked);
	}

	private bool LoadLockedStatus() {
		hasLoadedLockedStatus = true;
		return WhitPrefs.GetBool(GetKeyIsLocked(), PrefsBoolPriority.False, _isLocked, isLockedByDefault);
	}
		
	private string GetKeyIsLocked() {
		return packageName + "_isLocked";
	}

	private string GetKeyIsEquipped() {
		return packageName + "_isEquipped";
	}
}