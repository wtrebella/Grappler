using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace WhitTerrain {
	public class ContourLine : MonoBehaviour {
		[SerializeField] private Contour terrain;
		[SerializeField] private SnowPuff snowPuffPrefab;

		private Dictionary<ContourSection, List<SnowPuff>> snowPuffDict;

		private void Awake() {
			snowPuffDict = new Dictionary<ContourSection, List<SnowPuff>>();

			terrain.SignalTerrainSectionAdded += OnSectionAdded;
			terrain.SignalTerrainSectionRemoved += OnSectionRemoved;
		}

		private void Start() {
			ContourSection firstSection = terrain.GetFirstSection();
			if (!snowPuffDict.ContainsKey(firstSection)) {
				AddSnowPuffsToSection(firstSection);
			}
		}

		private void AddSnowPuffsToSection(ContourSection section) {
			var points = section.allPoints;
			snowPuffDict.Add(section, new List<SnowPuff>());
			for (int i = 0; i < points.Count - 1; i++) {
				SnowPuff snowPuff = snowPuffPrefab.Spawn();
				snowPuff.transform.SetParent(transform);
				Vector2 pointA = points[i];
				Vector2 pointB = points[i+1];
				snowPuff.SetPoints(pointA, pointB);
				snowPuffDict[section].Add(snowPuff);
			}
		}

		private void RemoveSnowPuffsFromSection(ContourSection section) {
			var snowPuffs = snowPuffDict[section];
			for (int i = snowPuffs.Count - 1; i >= 0; i--) {
				SnowPuff snowPuff = snowPuffs[i];
				snowPuffs.RemoveAt(i);
				snowPuff.Recycle();
			}
			snowPuffDict.Remove(section);
		}

		private void OnSectionAdded(ContourSection section) {
			AddSnowPuffsToSection(section);
		}

		private void OnSectionRemoved(ContourSection section) {
			RemoveSnowPuffsFromSection(section);
		}
		
		private void Update() {
		
		}
	}
}