using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomPropertyDrawer(typeof(WhitTerrainPatternInstruction))]
public class WhitTerrainPatternInstructionPropertyDrawer : PropertyDrawer {
	public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {
		SerializedProperty instructionType = prop.FindPropertyRelative("instructionType");
		SerializedProperty slope = prop.FindPropertyRelative("slope");
		SerializedProperty length = prop.FindPropertyRelative("length");
		SerializedProperty radius = prop.FindPropertyRelative("radius");

		EditorGUI.BeginProperty(pos, label, slope);
		EditorGUI.PropertyField(new Rect(pos.x, pos.y + 20, pos.width, pos.height), slope);
		EditorGUI.EndProperty();
//
//		EditorGUI.BeginProperty(pos, label, length);
//		EditorGUI.PropertyField(new Rect(pos.x, pos.y + 40, pos.width, pos.height), length);
//		EditorGUI.EndProperty();
//		EditorGUI.PropertyField(new Rect(pos.x, pos.y, pos.width, pos.height), instructionType);

//		EditorGUI.PropertyField(new Rect(pos.x, pos.y + 60, pos.width, pos.height), length);
//		EditorGUI.PropertyField(new Rect(pos.x, pos.y + 90, pos.width, pos.height), radius);

	}
}
