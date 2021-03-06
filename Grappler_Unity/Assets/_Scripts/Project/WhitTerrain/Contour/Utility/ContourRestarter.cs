﻿using UnityEngine;
using System.Collections;

namespace WhitTerrain {
	public class ContourRestarter : MonoBehaviour {
		[SerializeField] private PlayerTrajectory playerTrajectory;
		[SerializeField] private Path terrainPair;
		[SerializeField] private Player player;
		[SerializeField] private float startPointOffset = 5;
		[SerializeField] private float pastEndOffset = -50;

		private void RestartTerrain() {
			Vector3 newStartPos = playerTrajectory.GetPredictedPoint();
			newStartPos.y += startPointOffset;
			terrainPair.RestartTerrain((Vector2)newStartPos);
		}

		private bool NeedsRestart() {
			return terrainPair.HasEnd() && terrainPair.GetXIsPastEnd(player.body.transform.position.x + pastEndOffset) && !player.isGrappling;
		}

		private void FixedUpdate() {
			if (NeedsRestart()) RestartTerrain();
		}
	}
}