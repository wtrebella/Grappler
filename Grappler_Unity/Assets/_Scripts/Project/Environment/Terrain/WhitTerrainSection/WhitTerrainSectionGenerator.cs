using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhitTerrainSectionGenerator {
	private WhitTerrainSectionAttributes attributes;
	private int resolution = 3;

	public WhitTerrainSectionGenerator(WhitTerrainSectionAttributes attributes) {
		this.attributes = attributes;
	}

	public List<WhitTerrainSection> GenerateCurve(Vector2 startPoint, float curveLength, float startSlope, float endSlope) {
		float slopeRange = endSlope - startSlope;
		float deltaSlope = slopeRange / (resolution - 1);

		List<WhitTerrainSection> sections = new List<WhitTerrainSection>();

		Vector2 nextStartPoint = startPoint;

		for (int i = 0; i < resolution; i++) {
			float sectionLength = curveLength / resolution;
			float sectionSlope = startSlope + deltaSlope * i;
			WhitTerrainSection section = GenerateSection(nextStartPoint, sectionLength, sectionSlope);
			nextStartPoint = section.endPoint;
			sections.Add(section);
		}

		return sections;
	}

	public List<WhitTerrainSection> GenerateCurve(WhitTerrainSection previousSection, float sectionLength, float startSlope, float endSlope) {
		return GenerateCurve(previousSection.endPoint, sectionLength, startSlope, endSlope);
	}

	public WhitTerrainSection GenerateSection(Vector2 startPoint, float length, float slope) {
		WhitTerrainSectionConfig config = new WhitTerrainSectionConfig();
		config.startPoint = startPoint;
		config.length = length;
		config.slope = slope;
		return GenerateSection(config);
	}

	public WhitTerrainSection GenerateSection(WhitTerrainSection previousSection, float length, float slope) {
		return GenerateSection(previousSection.endPoint, length, slope);
	}

	private WhitTerrainSection GenerateSection(WhitTerrainSectionConfig config) {
		return new WhitTerrainSection(config, attributes);
	}
}