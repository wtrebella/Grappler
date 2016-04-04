using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WhitTerrainLine : MonoBehaviour {
	public List<WhitTerrainLineSection> sections {get; private set;}

	[SerializeField] private WhitTerrainLineSectionAttributes sectionAttributes;
	[SerializeField] private int maxSections = 5;

	private WhitTerrainLineSectionGroupGenerator sectionGenerator;

	public void AddPart(float slope) {
		AddSection(sectionGenerator.GenerateSection(GetLastSection(), 30.0f, slope));
	}

	public void ClampSectionCount() {
		while (sections.Count > maxSections) RemoveFirstSection();
	}

	public bool HasSections() {return sections != null && sections.Count > 0;}
	public WhitTerrainLineSection GetFirstSection() {return sections.GetFirstItem();}
	public WhitTerrainLineSection GetLastSection() {return sections.GetLastItem();}
	public Vector2 GetFirstPointLocal() {return sections.GetFirstItem().startPoint;}
	public Vector2 GetLastPointLocal() {return sections.GetLastItem().endPoint;}
	public Vector2 GetFirstPoint() {return transform.TransformPoint(sections.GetFirstItem().startPoint);}
	public Vector2 GetLastPoint() {return transform.TransformPoint(sections.GetLastItem().endPoint);}

	public Vector2 GetAveragePointAtX(float x) {
		Vector2 firstPoint = GetFirstPoint();
		Vector2 lastPoint = GetLastPoint();

		Vector2 totalVector = lastPoint - firstPoint;
		float width = totalVector.x;
		float relativeX = x - firstPoint.x;
		float percent = relativeX / width;
		Vector2 point = firstPoint + percent * totalVector;
		return point;
	}

	public List<Vector2> GetPoints() {
		List<Vector2> points = new List<Vector2>();
		for (int i = 0; i < sections.Count; i++) {
			WhitTerrainLineSection section = sections[i];
			points.Add(section.startPoint);
			foreach (Vector2 point in section.midPoints) points.Add(point);
			if (i == sections.Count - 1) points.Add(section.endPoint);
		}
		return points;
	}

	private void Awake() {
		sections = new List<WhitTerrainLineSection>();
		sectionGenerator = new WhitTerrainLineSectionGroupGenerator(sectionAttributes);
		AddFirstSection();
	}

	private void AddFirstSection() {
		AddSection(sectionGenerator.GenerateSection(transform.position, 5.0f, 0.0f));
	}

	private void AddSection(WhitTerrainLineSection sectionToAdd) {
		sections.Add(sectionToAdd);
	}

	private void AddSections(List<WhitTerrainLineSection> sectionsToAdd) {
		foreach (WhitTerrainLineSection section in sectionsToAdd) AddSection(section);
	}

	private void RemoveFirstSection() {
		WhitTerrainLineSection firstSection = sections[0];
		sections.Remove(firstSection);
	}
}