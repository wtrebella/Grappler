using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using System.Linq;

public static class TransformListExtensions {
	public static void SortByX(this List<Transform> list) {
		Transform[] array = list.ToArray();
		Array.Sort(array, delegate(Transform t1, Transform t2) { return t1.position.x.CompareTo(t2.position.x); });
		List<Transform> newList = new List<Transform>();
		for (int i = 0; i < array.Length; i++) newList.Add(array[i]);
		list.Clear();
		for (int i = 0; i < newList.Count; i++) list.Add(newList[i]);
	}

	public static void SortByY(this List<Transform> list) {
		Transform[] array = list.ToArray();
		Array.Sort(array, delegate(Transform t1, Transform t2) { return t1.position.y.CompareTo(t2.position.y); });
		List<Transform> newList = new List<Transform>();
		for (int i = 0; i < array.Length; i++) newList.Add(array[i]);
		list.Clear();
		for (int i = 0; i < newList.Count; i++) list.Add(newList[i]);
	}

	public static Transform GetItemWithHighestX(this List<Transform> list) {
		if (list.Count == 0) return null;
		list.SortByX();
		return list[list.Count - 1];
	}

	public static Transform GetItemWithHighestY(this List<Transform> list) {
		if (list.Count == 0) return null;
		list.SortByY();
		return list[list.Count - 1];
	}

	public static int GetIndexOfFirstItemWithXValOver(this List<Transform> list, float x) {
		int index = -1;
		for (int i = 0; i < list.Count; i++) {
			Transform transform = list[i];
			if (transform.position.x > x) {
				index = i;
				break;
			}
		}

		return index;
	}

	public static Transform GetItemClosestTo(this List<Transform> list, Vector2 point) {
		float dist = Mathf.Infinity;
		Transform closestItem = null;
		foreach (Transform t in list) {
			float newDist = ((Vector2)t.position - point).sqrMagnitude;
			if (newDist < dist) {
				dist = newDist;
				closestItem = t;
			}
		}
		return closestItem;
	}

	public static void RemoveItemsWithXValsUnder(this List<Transform> list, float x) {
		list.SortByX();
		int index = list.GetIndexOfFirstItemWithXValOver(x);
		if (index >= 0) list.RemoveRange(0, index);
		else list.Clear();
	}

	public static void RemoveItemsWithXValsOver(this List<Transform> list, float x) {
		list.SortByX();
		int index = list.GetIndexOfFirstItemWithXValOver(x);
		if (index >= 0) list.RemoveRange(index, list.Count - index);
	}

	public static int GetIndexOfFirstItemWithYValOver(this List<Transform> list, float y) {
		int index = -1;
		for (int i = 0; i < list.Count; i++) {
			Transform transform = list[i];
			if (transform.position.y > y) {
				index = i;
				break;
			}
		}
		
		return index;
	}
	
	public static void RemoveItemsWithYValsUnder(this List<Transform> list, float y) {
		list.SortByX();
		int index = list.GetIndexOfFirstItemWithYValOver(y);
		if (index >= 0) list.RemoveRange(0, index);
		else list.Clear();
	}
	
	public static void RemoveItemsWithYValsOver(this List<Transform> list, float y) {
		list.SortByX();
		int index = list.GetIndexOfFirstItemWithYValOver(y);
		if (index >= 0) list.RemoveRange(index, list.Count - index);
	}
}
