using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TerrainLine : MonoBehaviour {
	public List<TerrainLineSection> sections {get; private set;}

	[SerializeField] private TerrainLineAttributes attributes;

	public bool HasSections() {
		return sections != null && sections.Count > 0;
	}

	private void Awake() {
		sections = new List<TerrainLineSection>();
		AddSection();
	}

	private void AddSection() {
		TerrainLineSection section1 = new TerrainLineSection(Vector2.zero, 0.5f, attributes);
		sections.Add(section1);

		TerrainLineSection section2 = new TerrainLineSection(section1.endPoint, 0.75f, attributes);
		sections.Add(section2);

		TerrainLineSection section3 = new TerrainLineSection(section2.endPoint, 0.25f, attributes);
		sections.Add(section3);
	}

	private void RemoveFirstSection() {
		sections.RemoveAt(0);
	}
}