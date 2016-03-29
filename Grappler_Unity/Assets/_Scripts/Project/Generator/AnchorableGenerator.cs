﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AnchorableGenerator : Generator {
	private static int currentAnchorableID = 0;

	public void GenerateAnchorables(MountainChunk mountainChunk) {
		foreach (Point point in mountainChunk.edgePoints) CreateAnchorableAtPoint(mountainChunk, point);
	}

	private void CreateAnchorableAtPoint(MountainChunk chunk, Point point) {
		Anchorable anchorable = (Anchorable)GenerateItem();
		anchorable.transform.parent = chunk.transform;
		anchorable.transform.position = point.pointVector;
		anchorable.SetAnchorableID(currentAnchorableID++);
		anchorable.SetLinePoint(point);
	}
}
