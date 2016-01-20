using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpikesGenerator : MonoBehaviour {
	[SerializeField] private SpikeBall spikeBallPrefab;
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;

	private List<SpikeBall> spikeBalls;
	
	private void Awake() {
		spikeBalls = new List<SpikeBall>();
		mountainChunkGenerator.SignalMountainChunkCreated += HandleMountainChunkCreated;
	}
	
	private void FixedUpdate() {
		RemoveOffscreenSpikeBalls();
	}

	private void HandleMountainChunkCreated(MountainChunk chunk) {
		int num = 10;
		float chanceOfSpawn = 0.3f;
		float dist = 1f / ((float)num + 1f);
		for (int i = 1; i <= num; i++) {
			if (Random.value < chanceOfSpawn) CreateSpikeBall(chunk, dist * i);
		}
	}
	
	private void CreateSpikeBall(MountainChunk chunk, float place) {
		SpikeBall spikeBall = spikeBallPrefab.Spawn();
		spikeBall.transform.parent = chunk.transform;
		spikeBall.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 359));
		Vector3 position = chunk.GetPositionAtPlace(place);;
		position.z += 0.1f;
		spikeBall.transform.position = position;
		spikeBalls.Add(spikeBall);
	}

	private void RemoveOffscreenSpikeBalls() {
		for (int i = 0; i < spikeBalls.Count; i++) {
			SpikeBall spikeBall = spikeBalls[i];
			if (ShouldRecycleSpikeBall(spikeBall)) RecycleSpikeBallAtIndex(i);
			else break;
		}
	}
	
	private bool ShouldRecycleAnchorable(Anchorable anchorable) {
		return !anchorable.isConnected && GameScreen.instance.IsOffLeftOfScreenWithMargin(anchorable.transform.position.x);
	}
	
	private bool ShouldRecycleSpikeBall(SpikeBall spikeBall) {
		return GameScreen.instance.IsOffLeftOfScreenWithMargin(spikeBall.transform.position.x);
	}
	
	private void RecycleSpikeBall(SpikeBall spikeBall) {
		int indexOfSpikeBall = spikeBalls.IndexOf(spikeBall);
		RecycleSpikeBallAtIndex(indexOfSpikeBall);
	}
	
	private void RecycleSpikeBallAtIndex(int index) {
		WhitTools.Assert(index >= 0 && index < spikeBalls.Count, "invalid spikeBall index");
		
		SpikeBall spikeBall = spikeBalls[index];
		spikeBalls.RemoveAt(index);
		spikeBall.Recycle();
	}
}
