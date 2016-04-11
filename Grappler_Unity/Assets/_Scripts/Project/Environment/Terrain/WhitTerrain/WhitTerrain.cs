using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WhitTerrain : MonoBehaviour {
	private static float lengthThreshold = 1.0f;

	public Action SignalTerrainChanged;
	public Action<WhitTerrainSection> SignalTerrainSectionAdded;
	public Action<WhitTerrainSection> SignalTerrainSectionRemoved;

	public List<WhitTerrainSection> sections {get; private set;}

	[SerializeField] private WhitTerrainSectionAttributes sectionAttributes;

	private WhitTerrainSectionGenerator sectionGenerator;

	public void Initialize(Vector2 startPoint) {
		sections = new List<WhitTerrainSection>();
		sectionGenerator = new WhitTerrainSectionGenerator(sectionAttributes);
		AddFirstSection(startPoint);
	}

	public void AddStraight(float slope, float length) {
		if (length > lengthThreshold) {
			var newSection = sectionGenerator.GenerateSection(this, GetLastSection(), length, slope);
			AddSection(newSection);
		}
	}

	public void AddCurve(float targetSlope, float radius) {
		float startSlope = GetLastSectionSlope();
		float deltaSlope = targetSlope - startSlope;
		float angle = deltaSlope * WhitTools.Slope2Deg;
		float arcPercent = Mathf.Abs(angle / 360.0f);
		float length = 2 * Mathf.PI * radius * arcPercent;
		if (length > lengthThreshold) {
			var newSections = sectionGenerator.GenerateCurve(this, GetLastSection(), length, startSlope, targetSlope);
			AddSections(newSections);
		}
	}

	public bool IsValid() {return sections != null && sections.Count > 0;}
	public WhitTerrainSection GetFirstSection() {return sections.GetFirstItem();}
	public WhitTerrainSection GetLastSection() {return sections.GetLastItem();}
	public Vector2 GetStartPointLocal() {return sections.GetFirstItem().startPoint;}
	public Vector2 GetEndPointLocal() {return sections.GetLastItem().endPoint;}
	public Vector2 GetStartPoint() {return transform.TransformPoint(sections.GetFirstItem().startPoint);}
	public Vector2 GetEndPoint() {return transform.TransformPoint(sections.GetLastItem().endPoint);}

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

	public Vector2 GetPointAtX(float x) {
		WhitTerrainSection section = GetSectionAtX(x);
		if (section == null) return Vector2.zero;
		return section.GetWorldPointAtWorldX(x);
	}

	public Vector2 GetPointAtDist(float dist) {
		WhitTerrainSection section = GetSectionAtDist(dist);
		if (section == null) return Vector2.zero;
		else return section.GetPointAtDist(dist);
	}

	public Vector2 GetSurfacePointAtDist(float dist) {
		WhitTerrainSection section = GetSectionAtDist(dist);
		if (section == null) return Vector2.zero;
		else return section.GetSurfacePointAtDist(dist);
	}

	public List<Vector2> GetPointsLocal() {
		List<Vector2> points = new List<Vector2>();
		for (int i = 0; i < sections.Count; i++) {
			WhitTerrainSection section = sections[i];
			points.Add(section.startPoint);
			foreach (Vector2 point in section.midPoints) points.Add(point);
			if (i == sections.Count - 1) points.Add(section.endPoint);
		}
		return points;
	}

	public bool DistIsWithinDistThreshold(float dist) {
		float distToEnd = GetEndDist() - dist;
		return distToEnd < WhitTerrainPairAttributes.instance.playerDistThreshold;
	}

	private void Update() {
		RemoveOffScreenSections();
	}

	private WhitTerrainSection GetSectionAtDist(float dist) {
		if (sections.Count == 0) return null;

		for (int i = 0; i < sections.Count; i++) {
			WhitTerrainSection section = sections[i];

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

	private WhitTerrainSection GetSectionAtX(float x) {
		if (sections.Count == 0) return null;
		for (int i = 0; i < sections.Count; i++) {
			WhitTerrainSection section = sections[i];
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
		return distLength > WhitTerrainPairAttributes.instance.drawDistWidth;
	}

	private void OnChange() {
		if (SignalTerrainChanged != null) SignalTerrainChanged();
	}

	private void AddFirstSection(Vector2 startPoint) {
		AddSection(sectionGenerator.GenerateSection(this, startPoint, 0.0f, 5.0f, 0.0f));
	}

	private void AddSection(WhitTerrainSection sectionToAdd) {
		sections.Add(sectionToAdd);
		if (SignalTerrainSectionAdded != null) SignalTerrainSectionAdded(sectionToAdd);
		OnChange();
		RemoveOffScreenSections();
	}

	private void AddSections(List<WhitTerrainSection> sectionsToAdd) {
		foreach (WhitTerrainSection section in sectionsToAdd) AddSection(section);
	}

	private void RemoveFirstSection() {
		WhitTerrainSection firstSection = sections[0];
		sections.Remove(firstSection);
		if (SignalTerrainSectionRemoved != null) SignalTerrainSectionRemoved(firstSection);
		OnChange();
	}
}