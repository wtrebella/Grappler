using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhitTerrainSectionGenerator {
	private WhitTerrainSectionAttributes attributes;
	private int resolution = 3;

	public WhitTerrainSectionGenerator(WhitTerrainSectionAttributes attributes) {
		this.attributes = attributes;
	}

	public List<WhitTerrainSection> GenerateCurve(WhitTerrain terrain, Vector2 startPoint, float dist, float curveLength, float startSlope, float endSlope) {
		float slopeRange = endSlope - startSlope;
		float deltaSlope = slopeRange / (resolution - 1);

		List<WhitTerrainSection> sections = new List<WhitTerrainSection>();

		Vector2 nextStartPoint = startPoint;
		float nextDist = dist;

		for (int i = 0; i < resolution; i++) {
			float sectionLength = curveLength / resolution;
			float sectionSlope = startSlope + deltaSlope * i;
			WhitTerrainSection section = GenerateSection(terrain, nextStartPoint, nextDist, sectionLength, sectionSlope);
			nextStartPoint = section.endPoint;
			nextDist = section.distEnd;
			sections.Add(section);
		}
		return sections;
	}

	public List<WhitTerrainSection> GenerateCurve(WhitTerrain terrain, WhitTerrainSection previousSection, float sectionLength, float startSlope, float endSlope) {
		return GenerateCurve(terrain, previousSection.endPoint, previousSection.distEnd, sectionLength, startSlope, endSlope);
	}

	public WhitTerrainSection GenerateSection(WhitTerrain terrain, Vector2 startPoint, float dist, float length, float slope) {
		WhitTerrainSectionConfig config = new WhitTerrainSectionConfig();
		config.startPoint = startPoint;
		config.length = length;
		config.slope = slope;
		config.distStart = dist;
		return GenerateSection(terrain, config);
	}

	public WhitTerrainSection GenerateSection(WhitTerrain terrain, WhitTerrainSection previousSection, float length, float slope) {
		return GenerateSection(terrain, previousSection.endPoint, previousSection.distEnd, length, slope);
	}

	private WhitTerrainSection GenerateSection(WhitTerrain terrain, WhitTerrainSectionConfig config) {
		WhitTerrainSection section = new GameObject("Section", typeof(WhitTerrainSection)).GetComponent<WhitTerrainSection>();
		section.Initialize(terrain, config, attributes);
		return section;
	}
}