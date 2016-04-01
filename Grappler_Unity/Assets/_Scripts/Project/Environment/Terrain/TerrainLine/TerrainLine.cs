using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TerrainLine : MonoBehaviour {
	public List<TerrainLineSection> sections {get; private set;}

	[SerializeField] private TerrainLineSectionAttributes sectionAttributes;

	private TerrainLineSectionGroupGenerator sectionGenerator;

	public void AddStraightLine() {
		AddSection(sectionGenerator.GenerateSection(GetLastSection(), 30.0f, 0.1f));
	}

	public bool HasSections() {return sections != null && sections.Count > 0;}
	public TerrainLineSection GetFirstSection() {return sections.GetFirstItem();}
	public TerrainLineSection GetLastSection() {return sections.GetLastItem();}
	public Vector2 GetFirstPoint() {return sections.GetFirstItem().startPoint;}
	public Vector2 GetLastPoint() {return sections.GetLastItem().endPoint;}

	public List<Vector2> GetPoints() {
		if (!AssertHasSections()) return null;

		List<Vector2> points = new List<Vector2>();
		for (int i = 0; i < sections.Count; i++) {
			TerrainLineSection section = sections[i];
			points.Add(section.startPoint);
			foreach (Vector2 point in section.midPoints) points.Add(point);
			if (i == sections.Count - 1) points.Add(section.endPoint);
		}
		return points;
	}

	private void Awake() {
		sections = new List<TerrainLineSection>();
		sectionGenerator = new TerrainLineSectionGroupGenerator(sectionAttributes);
		AddSection(sectionGenerator.GenerateSection(Vector2.zero, 5.0f, 0.0f));
	}

	private void Start() {
//		AddSections(sectionGenerator.GenerateCurve(GameScreen.instance.lowerLeft, 30.0f, 0.1f, 0.9f));
//		AddSection(sectionGenerator.GenerateSection(GetLastSection(), 30.0f, 0.2f));
	}

	private bool AssertHasSections() {
		return WhitTools.Assert(HasSections(), "has no sections!");
	}

	private void AddSection(TerrainLineSection sectionToAdd) {
		sections.Add(sectionToAdd);
	}

	private void AddSections(List<TerrainLineSection> sectionsToAdd) {
		foreach (TerrainLineSection section in sectionsToAdd) sections.Add(section);
	}
}