using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WhitTerrain : MonoBehaviour {
	private static float lengthThreshold = 1.0f;

	public Action SignalTerrainLineChanged;

	public List<WhitTerrainSection> sections {get; private set;}

	[SerializeField] private WhitTerrainSectionAttributes sectionAttributes;
	[SerializeField] private int maxSections = 5;

	private Dictionary<WhitTerrainSection, float> sectionDists;
	private WhitTerrainSectionGenerator sectionGenerator;
	private bool changedThisFrame = false;

	public void AddStraight(float slope, float length) {
		if (length > lengthThreshold) {
			AddSection(sectionGenerator.GenerateSection(this, GetLastSection(), length, slope));
		}
	}

	public void AddCurve(float targetSlope, float radius) {
		float startSlope = GetLastSectionSlope();
		float deltaSlope = targetSlope - startSlope;
		float angle = deltaSlope * WhitTools.Slope2Deg;
		float arcPercent = Mathf.Abs(angle / 360.0f);
		float length = 2 * Mathf.PI * radius * arcPercent;
		if (length > lengthThreshold) {
			AddSections(sectionGenerator.GenerateCurve(this, GetLastSection(), length, startSlope, targetSlope));
		}
	}

	public bool IsValid() {return sections != null && sections.Count > 0;}
	public WhitTerrainSection GetFirstSection() {return sections.GetFirstItem();}
	public WhitTerrainSection GetLastSection() {return sections.GetLastItem();}
	public Vector2 GetFirstPointLocal() {return sections.GetFirstItem().startPoint;}
	public Vector2 GetLastPointLocal() {return sections.GetLastItem().endPoint;}
	public Vector2 GetFirstPoint() {return transform.TransformPoint(sections.GetFirstItem().startPoint);}
	public Vector2 GetLastPoint() {return transform.TransformPoint(sections.GetLastItem().endPoint);}

	public Vector2 GetLastSectionDirection() {
		return GetLastSection().GetDirection();
	}

	public float GetLastSectionSlope() {
		return GetLastSection().slope;
	}

	public Vector2 GetPointAtDist(float dist) {
		WhitTerrainSection section = GetSectionAtDist(dist);
		if (section == null) return Vector2.zero;
		Vector2 position = section.GetPointAtDist(dist);
		return position;
	}

	public Vector2 GetSurfacePointAtDist(float dist) {
		WhitTerrainSection section = GetSectionAtDist(dist);
		if (section == null) return Vector2.zero;
		Vector2 position = section.GetSurfacePointAtDist(dist);
		return position;
	}

	public List<Vector2> GetPoints() {
		List<Vector2> points = new List<Vector2>();
		for (int i = 0; i < sections.Count; i++) {
			WhitTerrainSection section = sections[i];
			points.Add(section.startPoint);
			foreach (Vector2 point in section.midPoints) points.Add(point);
			if (i == sections.Count - 1) points.Add(section.endPoint);
		}
		return points;
	}

	private void Awake() {
		sections = new List<WhitTerrainSection>();
		sectionGenerator = new WhitTerrainSectionGenerator(sectionAttributes);
		sectionDists = new Dictionary<WhitTerrainSection, float>();
		AddFirstSection();
	}

	private void Update() {
		if (changedThisFrame) OnChange();
	}

	private WhitTerrainSection GetSectionAtDist(float dist) {
		// TODO you are getting the section wrong here: shouldn't be turned into an index. needs to be gotten based on its distance
		int index = Mathf.FloorToInt(dist);
		WhitTerrainSection section;
		if (index >= sections.Count) section = GetLastSection();
		else section = sections[index];
		Debug.Log(index + ", " + section.distStart + ", " + dist);
		return section;
	}

	private void ClampSectionCount() {
		while (sections.Count > maxSections) RemoveFirstSection();
	}

	private void OnChange() {
		changedThisFrame = false;
		ClampSectionCount();
		if (SignalTerrainLineChanged != null) SignalTerrainLineChanged();
	}

	private void AddFirstSection() {
		AddSection(sectionGenerator.GenerateSection(this, transform.position, 0.0f, 5.0f, 0.0f));
	}

	private void AddSection(WhitTerrainSection sectionToAdd) {
		sections.Add(sectionToAdd);
		sectionDists.Add(sectionToAdd, sectionToAdd.distStart);
		changedThisFrame = true;
	}

	private void AddSections(List<WhitTerrainSection> sectionsToAdd) {
		foreach (WhitTerrainSection section in sectionsToAdd) AddSection(section);
	}

	private void RemoveFirstSection() {
		WhitTerrainSection firstSection = sections[0];
		sectionDists.Remove(firstSection);
		sections.Remove(firstSection);
		changedThisFrame = true;
	}
}