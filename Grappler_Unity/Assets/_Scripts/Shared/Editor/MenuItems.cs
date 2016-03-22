using UnityEngine;
using System.Collections;
using UnityEditor;

public class MenuItems : Editor {
	[MenuItem("Utilities/Clear PlayerPrefs")]
	public static void ClearPlayerPrefs() {
		PlayerPrefs.DeleteAll();
		Debug.Log("PlayerPrefs cleared!");
	}
}
