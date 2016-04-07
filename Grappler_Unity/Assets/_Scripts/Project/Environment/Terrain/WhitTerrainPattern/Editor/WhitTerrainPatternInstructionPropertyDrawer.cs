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

		Rect rect = new Rect(pos.x, pos.y, pos.width, pos.height);

		EditorGUI.PropertyField(rect, instructionType);
		EditorGUI.PropertyField(rect, slope);
		EditorGUI.PropertyField(rect, length);
		EditorGUI.PropertyField(rect, radius);
	}
}
