using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CollectionItem))]
public class CollectionItemEditor : Editor {
	private CollectionItem collectionItem;

	private void OnEnable() {
		collectionItem = (CollectionItem)target;
	}

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		GUIStyle ownedStyle = new GUIStyle(EditorStyles.boldLabel);
		ownedStyle.normal.textColor = new Color(0.1f, 0.8f, 0.2f);
		GUIStyle unownedStyle = new GUIStyle(EditorStyles.boldLabel);
		unownedStyle.normal.textColor = new Color(0.8f, 0.1f, 0.2f);

		if (collectionItem.owned) EditorGUILayout.LabelField("Owned", ownedStyle);
		else EditorGUILayout.LabelField("Unowned", unownedStyle);

		if (GUILayout.Button("Remove Prefs Data")) collectionItem.RemovePrefsData();

		EditorUtility.SetDirty(collectionItem);
	}
}
