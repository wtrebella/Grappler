using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AnchorableGenerator : MonoBehaviour {
	private static int currentAnchorableID = 0;
	
	[SerializeField] private Anchorable anchorablePrefab;
	
	private List<Anchorable> anchorables = new List<Anchorable>();

	private void FixedUpdate() {
		RemoveOffscreenAnchorables();
	}

	public void GenerateAnchorables(MountainChunk mountainChunk) {
		var points = mountainChunk.GetListOfMacroLinePoints();
		foreach (Point point in points) CreateAnchorableAtPoint(mountainChunk, point);
	}

	private void CreateAnchorableAtPoint(MountainChunk chunk, Point point) {
		Anchorable anchorable = anchorablePrefab.Spawn();
		anchorable.transform.parent = chunk.transform;
		anchorable.transform.position = point.pointVector;
		anchorable.SetAnchorableID(currentAnchorableID++);
		anchorable.SetLinePoint(point);
		anchorables.Add(anchorable);
	}

	private void RemoveOffscreenAnchorables() {
		for (int i = 0; i < anchorables.Count; i++) {
			Anchorable anchorable = anchorables[i];
			if (ShouldRecycleAnchorable(anchorable)) RecycleAnchorableAtIndex(i);
			else break;
		}
	}
	
	private bool ShouldRecycleAnchorable(Anchorable anchorable) {
		return !anchorable.isConnected && GameScreen.instance.IsOffLeftOfScreenWithMargin(anchorable.transform.position.x);
	}

	private void RecycleAnchorable(Anchorable anchorable) {
		int indexOfAnchorable = anchorables.IndexOf(anchorable);
		RecycleAnchorableAtIndex(indexOfAnchorable);
	}
	
	private void RecycleAnchorableAtIndex(int index) {
		WhitTools.Assert(index >= 0 && index < anchorables.Count, "invalid anchorable index");
		
		Anchorable anchorable = anchorables[index];
		anchorables.RemoveAt(index);
		anchorable.Recycle();
	}
}
