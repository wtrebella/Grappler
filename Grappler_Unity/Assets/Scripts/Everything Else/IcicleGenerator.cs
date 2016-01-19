using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IcicleGenerator : MonoBehaviour {
	[SerializeField] private Icicle iciclePrefab;
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;

	private List<Icicle> icicles;
	
	private void Awake() {
		icicles = new List<Icicle>();
		mountainChunkGenerator.SignalMountainChunkCreated += HandleMountainChunkCreated;
	}
	
	private void FixedUpdate() {
		RemoveOffscreenIcicles();
	}

	private void HandleMountainChunkCreated(MountainChunk chunk) {
		int num = 60;
		float chanceOfSpawn = 1;
		float dist = 1f / ((float)num + 1f);
		float distVar = dist / 3f;
		for (int i = 1; i <= num; i++) {
			if (Random.value < chanceOfSpawn) CreateIcicle(chunk, dist * i + Random.Range(-distVar, distVar));
		}
	}
	
	private void CreateIcicle(MountainChunk chunk, float place) {
		Icicle icicle = iciclePrefab.Spawn();
		icicle.transform.localScale = new Vector3(Random.Range(0.2f, 0.6f), Random.Range(0.3f, 0.6f), 1);
		icicle.transform.parent = chunk.transform;
		Vector3 position = chunk.GetPositionFromPlace(place);;
		position.z += 0.1f;
		icicle.transform.position = position;
		icicles.Add(icicle);
	}

	private void RemoveOffscreenIcicles() {
		for (int i = 0; i < icicles.Count; i++) {
			Icicle icicle = icicles[i];
			if (ShouldRecycleIcicle(icicle)) RecycleIcicleAtIndex(i);
			else break;
		}
	}
	
	private bool ShouldRecycleAnchorable(Anchorable anchorable) {
		return !anchorable.isConnected && GameScreen.instance.IsOffLeftOfScreenWithMargin(anchorable.transform.position.x);
	}
	
	private bool ShouldRecycleIcicle(Icicle icicle) {
		return GameScreen.instance.IsOffLeftOfScreenWithMargin(icicle.transform.position.x);
	}
	
	private void RecycleIcicle(Icicle icicle) {
		int indexOfIcicle = icicles.IndexOf(icicle);
		RecycleIcicleAtIndex(indexOfIcicle);
	}
	
	private void RecycleIcicleAtIndex(int index) {
		WhitTools.Assert(index >= 0 && index < icicles.Count, "invalid icicle index");
		
		Icicle icicle = icicles[index];
		icicles.RemoveAt(index);
		icicle.Recycle();
	}
}
