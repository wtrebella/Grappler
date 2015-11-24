using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(QuadOverlapTest))]
public class QuadOverlapTestEditor : Editor {
	QuadOverlapTest quadOverlapTest;

	void OnEnable() {
		quadOverlapTest = target as QuadOverlapTest;
	}

	void OnSceneGUI() {
		Color color;

		if (Quad.OverlapQuads(quadOverlapTest.quad1, quadOverlapTest.quad2)) color = Color.red;
		else color = Color.green;

		DrawCorners(quadOverlapTest.quad1, color);
		DrawCorners(quadOverlapTest.quad2, color);
	}

	void DrawCorners(Quad quad, Color color) {
		Handles.color = color;

		EditorGUI.BeginChangeCheck();
		Vector3 bottomLeft = Handles.FreeMoveHandle(new Vector3(quad.bottomLeft.x, quad.bottomLeft.y, 0), Quaternion.identity, 1, Vector3.zero, Handles.SphereCap);
		bottomLeft.x = Mathf.Min(quad.bottomRight.x, bottomLeft.x);
		bottomLeft.y = 0;
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject(quadOverlapTest, "Move Quad Corner (Bottom Left)");
			quad.bottomLeft = bottomLeft;
			EditorUtility.SetDirty(quadOverlapTest);
		}

		EditorGUI.BeginChangeCheck();
		Vector3 topLeft = Handles.FreeMoveHandle(new Vector3(quad.topLeft.x, quad.topLeft.y, 0), Quaternion.identity, 1, Vector3.zero, Handles.SphereCap);
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject(quadOverlapTest, "Move Quad Corner (Top Left)");
			quad.topLeft = topLeft;
			EditorUtility.SetDirty(quadOverlapTest);
		}

		EditorGUI.BeginChangeCheck();
		Vector3 topRight = Handles.FreeMoveHandle(new Vector3(quad.topRight.x, quad.topRight.y, 0), Quaternion.identity, 1, Vector3.zero, Handles.SphereCap);
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject(quadOverlapTest, "Move Quad Corner (Top Right)");
			quad.topRight = topRight;
			EditorUtility.SetDirty(quadOverlapTest);
		}

		EditorGUI.BeginChangeCheck();
		Vector3 bottomRight = Handles.FreeMoveHandle(new Vector3(quad.bottomRight.x, quad.bottomRight.y, 0), Quaternion.identity, 1, Vector3.zero, Handles.SphereCap);
		bottomRight.x = Mathf.Max(quad.bottomLeft.x, bottomRight.x);
		bottomRight.y = 0;
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject(quadOverlapTest, "Move Quad Corner (Bottom Right)");
			quad.bottomRight = bottomRight;
			EditorUtility.SetDirty(quadOverlapTest);
		}

		Handles.DrawLine(bottomLeft, topLeft);
		Handles.DrawLine(topLeft, topRight);
		Handles.DrawLine(topRight, bottomRight);
		Handles.DrawLine(bottomRight, bottomLeft);
	}

	public override void OnInspectorGUI () {
		base.OnInspectorGUI();

		EditorGUI.BeginChangeCheck();
		GUILayout.Button("Reset");
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(quadOverlapTest, "Reset");
			quadOverlapTest.Reset();
			EditorUtility.SetDirty(quadOverlapTest);
		}
	}
}
