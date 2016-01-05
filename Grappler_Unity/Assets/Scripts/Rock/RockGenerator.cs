using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RockGenerator : MonoBehaviour {
//	public int numMountainChunksCreated {get; private set;}
//	
//	public Action<MountainChunk> SignalMountainChunkCreated;
//	
//	[SerializeField] private MountainChunk mountainChunkPrefab;
//	
//	private List<MountainChunk> mountainChunks;
//	
//	private void Awake() {
//		mountainChunks = new List<MountainChunk>();
//	}
//	
//	private void Start() {
//		GenerateMountainChunks(3);
//	}
//	
//	private void GenerateMountainChunks(int numToGenerate) {
//		for (int i = 0; i < numToGenerate; i++) GenerateMountainChunk();
//	}
//	
//	private void GenerateMountainChunk() {
//		numMountainChunksCreated++;
//		
//		MountainChunk mountainChunk = Instantiate(mountainChunkPrefab);
//		
//		if (mountainChunks.Count == 0) mountainChunk.Generate(Vector2.zero);
//		else mountainChunk.Generate(mountainChunks.GetLastItem().GetLastLinePoint());
//		
//		mountainChunks.Add(mountainChunk);
//		
//		if (SignalMountainChunkCreated != null) SignalMountainChunkCreated(mountainChunk);
//	}
}
