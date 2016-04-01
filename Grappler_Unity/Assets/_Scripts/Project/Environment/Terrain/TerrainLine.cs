using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TerrainLine : MonoBehaviour {
	public List<TerrainLineSection> sections {get; private set;}

	[SerializeField] private TerrainLineSectionAttributes sectionAttributes;

	private TerrainLineSectionGroupGenerator sectionGenerator;

	public bool HasSections() {
		return sections != null && sections.Count > 0;
	}

	private void Awake() {
		sections = new List<TerrainLineSection>();
		sectionGenerator = new TerrainLineSectionGroupGenerator(sectionAttributes);

		AddSectionsToLine(sectionGenerator.GenerateCurve(Vector2.zero, 10.0f, 0.1f, 0.9f));
		TerrainLineSection lastSection = sections.GetLastItem();
		AddSectionToLine(sectionGenerator.GenerateSection(lastSection.endPoint, 30.0f, 0.2f));
	}

	private void AddSectionToLine(TerrainLineSection sectionToAdd) {
		sections.Add(sectionToAdd);
	}

	private void AddSectionsToLine(List<TerrainLineSection> sectionsToAdd) {
		foreach (TerrainLineSection section in sectionsToAdd) sections.Add(section);
	}

	private void RemoveFirstSection() {
		sections.RemoveAt(0);
	}
}