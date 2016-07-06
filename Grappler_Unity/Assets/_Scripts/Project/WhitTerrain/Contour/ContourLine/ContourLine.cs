using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace WhitTerrain {
	public class ContourLine : MonoBehaviour {
		[SerializeField] private Contour contour;
		[SerializeField] private ContourLinePiece contourLinePiecePrefab;

		private Dictionary<ContourSegment, List<ContourLinePiece>> pieceDict;

		private void Awake() {
			pieceDict = new Dictionary<ContourSegment, List<ContourLinePiece>>();

			contour.SignalSegmentAdded += OnSectionAdded;
			contour.SignalSegmentRemoved += OnSectionRemoved;
		}

		private void Start() {
			ContourSegment firstSection = contour.GetFirstSection();
			if (!pieceDict.ContainsKey(firstSection)) {
				AddPiecesToSection(firstSection);
			}
		}

		private void AddPiecesToSection(ContourSegment section) {
			var points = section.allPoints;
			pieceDict.Add(section, new List<ContourLinePiece>());
			for (int i = 0; i < points.Count - 1; i++) {
				ContourLinePiece snowPuff = contourLinePiecePrefab.Spawn();
				snowPuff.transform.SetParent(transform);
				Vector2 pointA = points[i];
				Vector2 pointB = points[i+1];
				snowPuff.SetPoints(pointA, pointB);
				pieceDict[section].Add(snowPuff);
			}
		}

		private void RemovePiecesFromSection(ContourSegment section) {
			var snowPuffs = pieceDict[section];
			for (int i = snowPuffs.Count - 1; i >= 0; i--) {
				ContourLinePiece snowPuff = snowPuffs[i];
				snowPuffs.RemoveAt(i);
				snowPuff.Recycle();
			}
			pieceDict.Remove(section);
		}

		private void OnSectionAdded(ContourSegment section) {
			AddPiecesToSection(section);
		}

		private void OnSectionRemoved(ContourSegment section) {
			RemovePiecesFromSection(section);
		}
		
		private void Update() {
		
		}
	}
}