using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PointInQuadTest))]
public class PointInQuadTestEditor : Editor {
	PointInQuadTest quadTest;

	void OnEnable() {
		quadTest = target as PointInQuadTest;
	}

	void OnSceneGUI() {
		DrawCorners();
		DrawTestPoint();
	}

	void DrawCorners() {
		Handles.color = Color.blue;

		EditorGUI.BeginChangeCheck();
		Vector3 bottomLeft = Handles.FreeMoveHandle(new Vector3(quadTest.quad.bottomLeft.x, quadTest.quad.bottomLeft.y, 0), Quaternion.identity, 1, Vector3.zero, Handles.SphereCap);
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject(quadTest, "Move Quad Corner (Bottom Left)");
			quadTest.quad.bottomLeft = bottomLeft;
			EditorUtility.SetDirty(quadTest);
		}

		EditorGUI.BeginChangeCheck();
		Vector3 topLeft = Handles.FreeMoveHandle(new Vector3(quadTest.quad.topLeft.x, quadTest.quad.topLeft.y, 0), Quaternion.identity, 1, Vector3.zero, Handles.SphereCap);
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject(quadTest, "Move Quad Corner (Top Left)");
			quadTest.quad.topLeft = topLeft;
			EditorUtility.SetDirty(quadTest);
		}

		EditorGUI.BeginChangeCheck();
		Vector3 topRight = Handles.FreeMoveHandle(new Vector3(quadTest.quad.topRight.x, quadTest.quad.topRight.y, 0), Quaternion.identity, 1, Vector3.zero, Handles.SphereCap);
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject(quadTest, "Move Quad Corner (Top Right)");
			quadTest.quad.topRight = topRight;
			EditorUtility.SetDirty(quadTest);
		}

		EditorGUI.BeginChangeCheck();
		Vector3 bottomRight = Handles.FreeMoveHandle(new Vector3(quadTest.quad.bottomRight.x, quadTest.quad.bottomRight.y, 0), Quaternion.identity, 1, Vector3.zero, Handles.SphereCap);
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject(quadTest, "Move Quad Corner (Bottom Right)");
			quadTest.quad.bottomRight = bottomRight;
			EditorUtility.SetDirty(quadTest);
		}

		Handles.DrawLine(bottomLeft, topLeft);
		Handles.DrawLine(topLeft, topRight);
		Handles.DrawLine(topRight, bottomRight);
		Handles.DrawLine(bottomRight, bottomLeft);
	}

	void DrawTestPoint() {
		bool containsPoint = Quad.ContainsPoint(quadTest.quad, quadTest.testPoint);
		if (containsPoint) Handles.color = Color.green;
		else Handles.color = Color.red;

		EditorGUI.BeginChangeCheck();
		Vector3 movedPoint = Handles.FreeMoveHandle(new Vector3(quadTest.testPoint.x, quadTest.testPoint.y, 0), Quaternion.identity, 1, Vector3.zero, Handles.SphereCap);
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(quadTest, "Move Test Point");
			quadTest.testPoint = movedPoint;
			EditorUtility.SetDirty(quadTest);
		}
	}

	public override void OnInspectorGUI () {
		base.OnInspectorGUI();

		EditorGUI.BeginChangeCheck();
		GUILayout.Button("Reset");
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(quadTest, "Reset");
			quadTest.Reset();
			EditorUtility.SetDirty(quadTest);
		}
	}
}
