using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(MountainChunkVectorLine))]
public class MountainChunkGenerator : MonoBehaviour {
	[SerializeField] private MountainChunk mountainChunkPrefab;

	private MountainChunkVectorLine mountainChunkVectorLine;
	private List<MountainChunk> mountainChunks;

	private void Awake() {
		mountainChunkVectorLine = GetComponent<MountainChunkVectorLine>();
		mountainChunks = new List<MountainChunk>();
	}

	private void Start() {
		GenerateMountainChunks(30);
	}

	private void GenerateMountainChunks(int numToGenerate) {
		for (int i = 0; i < numToGenerate; i++) GenerateMountainChunk();
	}

	private void GenerateMountainChunk() {
		MountainChunk mountainChunk = Instantiate(mountainChunkPrefab);

		if (mountainChunks.Count == 0) mountainChunk.Generate(Vector2.zero);
		else mountainChunk.Generate(mountainChunks.GetLastItem().GetLastLinePoint());

		mountainChunks.Add(mountainChunk);
		mountainChunkVectorLine.AddToLine(mountainChunk);
	}
}
