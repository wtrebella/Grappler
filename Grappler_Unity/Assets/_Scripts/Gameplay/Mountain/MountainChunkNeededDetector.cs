using UnityEngine;
using System.Collections;
using System;

public class MountainChunkNeededDetector : MonoBehaviour {
	public bool NeedsNewMountainChunk(MountainChunk lastMountainChunk) {
		return lastMountainChunk.GetFirstLinePoint().x <= GameScreen.instance.maxX;
	}
}
