using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using System.Linq;

public static class WhitExtensions {
	public static void IncrementWithWrap(this int value, IntRange wrapRange) {
		value = WhitTools.IncrementWithWrap(value, wrapRange);
	}

	public static void DecrementWithWrap(this int value, IntRange wrapRange) {
		value = WhitTools.DecrementWithWrap(value, wrapRange);
	}

	public static Vector2 GetConnectedAnchorInWorldPosition(this SpringJoint2D springJoint) {
		return springJoint.connectedBody.transform.TransformPoint(springJoint.connectedAnchor);
	}

	public static Vector2 GetAnchorInWorldPosition(this SpringJoint2D springJoint) {
		return springJoint.transform.TransformPoint(springJoint.anchor);
	}

	public static void ApplyAttributes(this SpringJoint2D springJoint, SpringJointAttributes attributes) {
		SpringJointAttributes.ApplyAttributes(springJoint, attributes);
	}

	public static Vector2[] ToVector2Array(this List<Point> list) {
		Vector2[] array = new Vector2[list.Count];
		for (int i = 0; i < list.Count; i++) array[i] = list[i].vector;
		return array;
	}
}
