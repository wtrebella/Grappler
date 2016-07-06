using UnityEngine;
using System.Collections;
using System;

namespace WhitTerrain {
	[Serializable]
	public class ContourSectionConfig {
		public Vector2 startPoint = Vector2.zero;
		public float distStart = 0;
		public float slope = 0.5f;
		public float length = 5.0f;
		public bool bumpify = true;

		public ContourSectionConfig() {
			
		}
	}
}