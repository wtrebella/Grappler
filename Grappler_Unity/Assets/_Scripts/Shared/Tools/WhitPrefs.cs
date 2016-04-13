using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Prime31;
using UnityEngine.Events;

public class WhitPrefs : AutoSingleton<WhitPrefs> {
	public UnityEventWithList<object> OnKeyValueStoreDidChange;

	private void Awake() {
		iCloudManager.keyValueStoreDidChangeEvent += HandleKeyValueStoreDidChange;
	}

	private void OnDestroy() {
		iCloudManager.keyValueStoreDidChangeEvent -= HandleKeyValueStoreDidChange;
	}

	private void HandleKeyValueStoreDidChange(List<object> keys) {
		WhitTools.Invoke<object>(OnKeyValueStoreDidChange, keys);
	}

	public static void SetList<T>(string key, List<T> list) {
		var b = new BinaryFormatter();
		var m = new MemoryStream();
		b.Serialize(m, list);

		SetString(key, Convert.ToBase64String(m.GetBuffer()));
	}

	public static List<T> GetList<T>(string key) {
		List<T> list = new List<T>();

		var data = GetString(key);

		if(!string.IsNullOrEmpty(data)) {
			var b = new BinaryFormatter();
			var m = new MemoryStream(Convert.FromBase64String(data));
			list = (List<T>)b.Deserialize(m);
		}

		return list;
	}

	public static bool Synchronize() {
		return P31Prefs.synchronize();
	}

	public static void RemovePrefsKeyData(string aKey) {
		P31Prefs.removeObjectForKey(aKey);
	}

	public static bool HasKey(string key) {
		return P31Prefs.hasKey(key);
	}

	public static List<object> AllKeys() {
		return P31Prefs.allKeys();
	}

	public static void RemoveAll() {
		P31Prefs.removeAll();
	}

	public static void SetString(string aKey, string aString) {
		P31Prefs.setString(aKey, aString);
	}

	public static string GetString(string key, string defaultString = "") {
		if (!HasKey(key)) return defaultString;
		return P31Prefs.getString(key);
	}

	public static IDictionary GetDictionary(string aKey) {
		return P31Prefs.getDictionary(aKey);
	}

	public static void SetDictionary(string aKey, Dictionary<string, object> dict) {
		P31Prefs.setDictionary(aKey, dict);
	}

	public static float GetFloat(string aKey, float defaultFloat = 0.0f) {
		if (!HasKey(aKey)) return defaultFloat;
		return P31Prefs.getFloat(aKey);
	}

	public static float GetFloat(string aKey, PrefsNumberPriority priority, float oldValue, float defaultFloat = 0.0f) {
		float newValue = GetFloat(aKey, defaultFloat);
		float value = ApplyPrefsPriority(newValue, oldValue, priority);
		return value;
	}

	public static void SetFloat(string aKey, float value) {
		P31Prefs.setFloat(aKey, value);
	}

	public static void SetFloat(string aKey, PrefsNumberPriority priority, float newValue) {
		float oldValue = GetFloat(aKey);
		float value = ApplyPrefsPriority(newValue, oldValue, priority);
		SetFloat(aKey, value);
	}

	public static int GetInt(string aKey, int defaultInt = 0) {
		if (!HasKey(aKey)) return defaultInt;
		return P31Prefs.getInt(aKey);
	}

	public static int GetInt(string aKey, PrefsNumberPriority priority, int oldValue, int defaultInt = 0) {
		int newValue = GetInt(aKey, defaultInt);
		int value = ApplyPrefsPriority(newValue, oldValue, priority);
		return value;
	}

	public static void SetInt(string aKey, int value) {
		P31Prefs.setInt(aKey, value);
	}

	public static void SetInt(string aKey, PrefsNumberPriority priority, int newValue) {
		int oldValue = GetInt(aKey);
		int value = ApplyPrefsPriority(newValue, oldValue, priority);
		SetInt(aKey, value);
	}

	public static bool GetBool(string key, bool defaultBool = false) {
		if (!HasKey(key)) return defaultBool;
		return P31Prefs.getBool(key);
	}

	public static bool GetBool(string aKey, PrefsBoolPriority priority, bool oldValue, bool defaultBool = false) {
		bool newValue = GetBool(aKey, defaultBool);
		bool value = ApplyPrefsPriority(newValue, oldValue, priority);
		return value;
	}

	public static void SetBool(string key, bool b) {
		P31Prefs.setBool(key, b);
	}

	public static void SetBool(string aKey, PrefsBoolPriority priority, bool newValue) {
		bool oldValue = GetBool(aKey);
		bool value = ApplyPrefsPriority(newValue, oldValue, priority);
		SetBool(aKey, value);
	}

	private static int ApplyPrefsPriority(int newValue, int oldValue, PrefsNumberPriority priority) {
		if (priority == PrefsNumberPriority.New) return newValue;
		else if (priority == PrefsNumberPriority.Bigger) return Mathf.Max(newValue, oldValue);
		else if (priority == PrefsNumberPriority.Smaller) return Mathf.Min(newValue, oldValue);
		Debug.LogError("invalid PrefsNumberBehavior: " + priority.ToString());
		return newValue;
	}

	private static float ApplyPrefsPriority(float newValue, float oldValue, PrefsNumberPriority priority) {
		if (priority == PrefsNumberPriority.New) return newValue;
		else if (priority == PrefsNumberPriority.Bigger) return Mathf.Max(newValue, oldValue);
		else if (priority == PrefsNumberPriority.Smaller) return Mathf.Min(newValue, oldValue);
		Debug.LogError("invalid PrefsNumberBehavior: " + priority.ToString());
		return newValue;
	}

	private static bool ApplyPrefsPriority(bool newValue, bool oldValue, PrefsBoolPriority priority) {
		if (priority == PrefsBoolPriority.New) return newValue;
		else if (priority == PrefsBoolPriority.True) return newValue || oldValue;
		else if (priority == PrefsBoolPriority.False) return newValue && oldValue;
		Debug.LogError("invalid PrefsBoolBehavior: " + priority.ToString());
		return newValue;
	}
}