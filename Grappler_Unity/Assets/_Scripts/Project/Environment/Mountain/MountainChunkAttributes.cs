using UnityEngine;
using System.Collections;

public class MountainChunkAttributes : ScriptableObject {
	[SerializeField] private int numPoints = 10;
	[SerializeField] private float bumpHeight = 0.05f;
	[SerializeField] private float slopeChange = 0.15f;
	[SerializeField] private float perpDist = 0.75f;
	[SerializeField] private float marginSize = 0.01f;
	[SerializeField] private FloatRange slopeRange = new FloatRange(0.0f, 0.35f);
	[SerializeField] private FloatRange bumpWidthRange = new FloatRange(0.15f, 0.25f);
	[SerializeField] private FloatRange pointDistRange = new FloatRange(1.5f, 2.5f);
}