using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace WhitTerrain {
	public class Contour : MonoBehaviour {
		private static float lengthThreshold = 1.0f;

		public Action SignalTerrainChanged;
		public Action<ContourSection> SignalTerrainSectionAdded;
		public Action<ContourSection> SignalTerrainSectionRemoved;

		public List<ContourSection> sections {get; private set;}

		[SerializeField] private ContourSectionAttributes sectionAttributes;

		private ContourSectionGenerator sectionGenerator;

		public void Initialize(Vector2 startPoint) {
			if (sections == null) sections = new List<ContourSection>();
			if (sectionGenerator == null) sectionGenerator = new ContourSectionGenerator(sectionAttributes);
			AddFirstSection(startPoint);
		}

		public ContourSection AddStraight(float slope, float length, bool bumpify) {
			if (length > lengthThreshold) {
				var newSection = sectionGenerator.GenerateSection(this, GetLastSection(), length, slope, bumpify);
				AddSection(newSection);
				return newSection;
			}
			return null;
		}

		public List<ContourSection> AddCurve(float targetSlope, float radius, bool bumpify) {
			float startSlope = GetLastSectionSlope();
			float deltaSlope = targetSlope - startSlope;
			float angle = deltaSlope * WhitTools.Slope2Deg;
			float arcPercent = Mathf.Abs(angle / 360.0f);
			float length = 2 * Mathf.PI * radius * arcPercent;
			if (length > lengthThreshold) {
				var newSections = sectionGenerator.GenerateCurve(this, GetLastSection(), length, startSlope, targetSlope, bumpify);
				AddSections(newSections);
				return newSections;
			}
			return new List<ContourSection>();
		}

		public bool IsValid() {return sections != null && sections.Count > 0;}
		public ContourSection GetFirstSection() {return sections.GetFirst();}
		public ContourSection GetLastSection() {return sections.GetLast();}
		public Vector2 GetLocalStartPoint() {return sections.GetFirst().startPoint;}
		public Vector2 GetLocalEndPoint() {return sections.GetLast().endPoint;}
		public Vector2 GetWorldStartPoint() {return transform.TransformPoint(sections.GetFirst().startPoint);}
		public Vector2 GetWorldEndPoint() {return transform.TransformPoint(sections.GetLast().endPoint);}

		public float GetDistLength() {
			return GetEndDist() - GetStartDist();
		}

		public float GetStartDist() {
			return GetFirstSection().distStart;
		}

		public float GetEndDist() {
			return GetLastSection().distEnd;
		}

		public Vector2 GetLastSectionDirection() {
			return GetLastSection().GetDirection();
		}

		public float GetLastSectionSlope() {
			return GetLastSection().slope;
		}

		public void ResetTerrain() {
			RemoveAllSections();
		}

		public Vector2 GetPointAtX(float x) {
			ContourSection section = GetSectionAtX(x);
			if (section == null) return Vector2.zero;
			return section.GetWorldPointAtWorldX(x);
		}

		public Vector2 GetPointAtDist(float dist) {
			ContourSection section = GetSectionAtDist(dist);
			if (section == null) return Vector2.zero;
			else return section.GetPointAtDist(dist);
		}

		public Vector2 GetSurfacePointAtDist(float dist) {
			ContourSection section = GetSectionAtDist(dist);
			if (section == null) return Vector2.zero;
			else return section.GetSurfacePointAtDist(dist);
		}

		public List<Vector2> GetPointsLocal() {
			List<Vector2> points = new List<Vector2>();
			for (int i = 0; i < sections.Count; i++) {
				ContourSection section = sections[i];
				points.Add(section.startPoint);
				foreach (Vector2 point in section.midPoints) points.Add(point);
				if (i == sections.Count - 1) points.Add(section.endPoint);
			}
			return points;
		}

		public bool DistIsWithinDistThreshold(float dist) {
			float distToEnd = GetEndDist() - dist;
			return distToEnd < ContourPairAttributes.instance.playerDistThreshold;
		}

		private void Update() {
			RemoveOffScreenSections();
		}

		private ContourSection GetSectionAtDist(float dist) {
			if (sections.Count == 0) return null;

			for (int i = 0; i < sections.Count; i++) {
				ContourSection section = sections[i];

				if (i == 0) {
					if (dist < section.distEnd) return section;
				}
				else if (i == sections.Count - 1) {
					if (dist > section.distStart) return section;
				}

				if (section.ContainsDist(dist)) return section;
			}

			return null;
		}

		private ContourSection GetSectionAtX(float x) {
			if (sections.Count == 0) return null;
			for (int i = 0; i < sections.Count; i++) {
				ContourSection section = sections[i];
				if (i == 0) {
					if (x < section.GetWorldStartPoint().x) return section;
				}
				else if (i == sections.Count - 1) {
					if (x > section.GetWorldEndPoint().x) return section;
				}

				if (section.ContainsWorldX(x)) return section;
			}
			return null;
		}

		private void RemoveOffScreenSections() {
			while (ShouldRemoveFirstSection()) RemoveFirstSection();
		}

		private bool ShouldRemoveFirstSection() {
			float distLength = GetDistLength();
			return distLength > ContourPairAttributes.instance.drawDistWidth;
		}

		private void OnChange() {
			if (SignalTerrainChanged != null) SignalTerrainChanged();
		}

		private void AddFirstSection(Vector2 startPoint) {
			AddSection(sectionGenerator.GenerateSection(this, startPoint, 0.0f, 100.0f, 0.0f, true));
		}

		private void AddSection(ContourSection sectionToAdd) {
			sections.Add(sectionToAdd);
			if (SignalTerrainSectionAdded != null) SignalTerrainSectionAdded(sectionToAdd);
			OnChange();
			RemoveOffScreenSections();
		}

		private void AddSections(List<ContourSection> sectionsToAdd) {
			foreach (ContourSection section in sectionsToAdd) AddSection(section);
		}

		private void RemoveFirstSection() {
			ContourSection firstSection = sections[0];
			RemoveSection(firstSection);
		}

		private void RemoveSection(ContourSection section) {
			sections.Remove(section);
			if (SignalTerrainSectionRemoved != null) SignalTerrainSectionRemoved(section);
			GameObject.Destroy(section.gameObject, 0.5f);
			OnChange();
		}

		private void RemoveAllSections() {
			for (int i = sections.Count - 1; i >= 0; i--) {
				ContourSection section = sections[i];
				RemoveSection(section);
			}
		}
	}
}