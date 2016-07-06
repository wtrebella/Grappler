using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WhitTerrain {
	public class ContourSectionGenerator {
		private ContourSectionAttributes attributes;
		private int resolution = 3;

		public ContourSectionGenerator(ContourSectionAttributes attributes) {
			this.attributes = attributes;
		}

		public List<ContourSection> GenerateCurve(Contour terrain, Vector2 startPoint, float dist, float curveLength, float startSlope, float endSlope, bool bumpify) {
			float slopeRange = endSlope - startSlope;
			float deltaSlope = slopeRange / (resolution - 1);

			List<ContourSection> sections = new List<ContourSection>();

			Vector2 nextStartPoint = startPoint;
			float nextDist = dist;

			for (int i = 0; i < resolution; i++) {
				float sectionLength = curveLength / resolution;
				float sectionSlope = startSlope + deltaSlope * i;
				ContourSection section = GenerateSection(terrain, nextStartPoint, nextDist, sectionLength, sectionSlope, bumpify);
				nextStartPoint = section.endPoint;
				nextDist = section.distEnd;
				sections.Add(section);
			}
			return sections;
		}

		public List<ContourSection> GenerateCurve(Contour terrain, ContourSection previousSection, float sectionLength, float startSlope, float endSlope, bool bumpify) {
			return GenerateCurve(terrain, previousSection.endPoint, previousSection.distEnd, sectionLength, startSlope, endSlope, bumpify);
		}

		public ContourSection GenerateSection(Contour terrain, Vector2 startPoint, float dist, float length, float slope, bool bumpify) {
			ContourSectionConfig config = new ContourSectionConfig();
			config.startPoint = startPoint;
			config.length = length;
			config.slope = slope;
			config.distStart = dist;
			config.bumpify = bumpify;
			return GenerateSection(terrain, config);
		}

		public ContourSection GenerateSection(Contour terrain, ContourSection previousSection, float length, float slope, bool bumpify) {
			return GenerateSection(terrain, previousSection.endPoint, previousSection.distEnd, length, slope, bumpify);
		}

		private ContourSection GenerateSection(Contour terrain, ContourSectionConfig config) {
			ContourSection section = new GameObject("Section", typeof(ContourSection)).GetComponent<ContourSection>();
			section.Initialize(terrain, config, attributes);
			return section;
		}
	}
}