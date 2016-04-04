using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhitTerrainLineSectionGroupGenerator {
	private WhitTerrainLineSectionAttributes attributes;
	private int resolution = 3;

	public WhitTerrainLineSectionGroupGenerator(WhitTerrainLineSectionAttributes attributes) {
		this.attributes = attributes;
	}

	public List<WhitTerrainLineSection> GenerateCurve(Vector2 startPoint, float sectionLength, float startSlope, float endSlope) {
		float slopeRange = endSlope - startSlope;
		float deltaSlope = slopeRange / (resolution - 1);

		List<WhitTerrainLineSection> sectionGroup = new List<WhitTerrainLineSection>();

		Vector2 nextStartPoint = startPoint;

		for (int i = 0; i < resolution; i++) {
			WhitTerrainLineSection section = GenerateSection(nextStartPoint, sectionLength / resolution, startSlope + deltaSlope * i);
			nextStartPoint = section.endPoint;
			sectionGroup.Add(section);
		}

		return sectionGroup;
	}

	public List<WhitTerrainLineSection> GenerateCurve(WhitTerrainLineSection previousSection, float sectionLength, float startSlope, float endSlope) {
		return GenerateCurve(previousSection.endPoint, sectionLength, startSlope, endSlope);
	}

	public WhitTerrainLineSection GenerateSection(Vector2 startPoint, float length, float slope) {
		WhitTerrainLineSectionConfig config = new WhitTerrainLineSectionConfig();
		config.startPoint = startPoint;
		config.length = length;
		config.slope = slope;
		return GenerateSection(config);
	}

	public WhitTerrainLineSection GenerateSection(WhitTerrainLineSection previousSection, float length, float slope) {
		return GenerateSection(previousSection.endPoint, length, slope);
	}

	private WhitTerrainLineSection GenerateSection(WhitTerrainLineSectionConfig config) {
		return new WhitTerrainLineSection(config, attributes);
	}
}