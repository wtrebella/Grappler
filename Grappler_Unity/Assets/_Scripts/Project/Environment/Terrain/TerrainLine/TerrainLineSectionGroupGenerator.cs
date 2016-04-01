using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainLineSectionGroupGenerator {
	private TerrainLineSectionAttributes attributes;
	private int resolution = 3;

	public TerrainLineSectionGroupGenerator(TerrainLineSectionAttributes attributes) {
		this.attributes = attributes;
	}

	public List<TerrainLineSection> GenerateCurve(Vector2 startPoint, float sectionLength, float startSlope, float endSlope) {
		float slopeRange = endSlope - startSlope;
		float deltaSlope = slopeRange / (resolution - 1);

		List<TerrainLineSection> sectionGroup = new List<TerrainLineSection>();

		Vector2 nextStartPoint = startPoint;

		for (int i = 0; i < resolution; i++) {
			TerrainLineSection section = GenerateSection(nextStartPoint, sectionLength / resolution, startSlope + deltaSlope * i);
			nextStartPoint = section.endPoint;
			sectionGroup.Add(section);
		}

		return sectionGroup;
	}

	public List<TerrainLineSection> GenerateCurve(TerrainLineSection previousSection, float sectionLength, float startSlope, float endSlope) {
		return GenerateCurve(previousSection.endPoint, sectionLength, startSlope, endSlope);
	}

	public TerrainLineSection GenerateSection(Vector2 startPoint, float length, float slope) {
		TerrainLineSectionConfig config = new TerrainLineSectionConfig();
		config.startPoint = startPoint;
		config.length = length;
		config.slope = slope;
		return GenerateSection(config);
	}

	public TerrainLineSection GenerateSection(TerrainLineSection previousSection, float length, float slope) {
		return GenerateSection(previousSection.endPoint, length, slope);
	}

	private TerrainLineSection GenerateSection(TerrainLineSectionConfig config) {
		return new TerrainLineSection(config, attributes);
	}
}