using UnityEngine;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GrapplerPrefs : ScriptableObjectSingleton<GrapplerPrefs> {
#pragma warning disable 414
	/*
	[SerializeField] private bool _skipMenu = false;
	public static bool skipMenu {
#if UNITY_EDITOR
		get { return instance._skipMenu; }
#else
		get { return false; }
#endif
	}
*/
#pragma warning restore 414
}