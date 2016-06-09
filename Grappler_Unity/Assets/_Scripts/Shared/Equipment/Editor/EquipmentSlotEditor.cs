using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EquipmentSlot))]
public class EquipmentSlotEditor : Editor {
	private EquipmentSlot equipmentSlot;

	private void OnEnable() {
		equipmentSlot = (EquipmentSlot)target;
	}

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		GUIStyle style = new GUIStyle();
		style.fontSize = 16;
		EditorGUILayout.Space();

		if (equipmentSlot.IsEquipped()) {
			style.normal.textColor = Color.green;
			EditorGUILayout.LabelField(equipmentSlot.GetItem().itemName, style);
		}
		else {
			style.normal.textColor = Color.red;
			EditorGUILayout.LabelField("Nothing Equipped", style);
		}
	}
}