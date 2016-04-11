using UnityEngine;
using System.Collections;

public class ItemBetweenMountainAndGroundGenerator : Generator {
	protected virtual void HandleGroundChunkGenerated(GroundChunk chunk) {

	}

	protected GeneratableItem GenerateItemBetweenMountainAndGround(MountainChunk mountainChunk, GroundChunk groundChunk, float place, float betweenPercent) {
		GeneratableItem item = GenerateItem();
		item.transform.parent = mountainChunk.transform;
		Vector3 position = PlaceToPosition(mountainChunk, groundChunk, place, betweenPercent);
		position.z += 0.1f;
		item.transform.position = position;
		return item;
	}

	protected Vector3 PlaceToPosition(MountainChunk mountainChunk, GroundChunk groundChunk, float place, float betweenPercent) {
		betweenPercent = Mathf.Clamp01(betweenPercent);
		Vector3 mountainPosition = mountainChunk.PlaceToPosition(place);
		Vector3 groundPosition = groundChunk.PlaceToPosition(place);
		Vector3 position = Vector3.Lerp(groundPosition, mountainPosition, betweenPercent);
		return position;
	}
}