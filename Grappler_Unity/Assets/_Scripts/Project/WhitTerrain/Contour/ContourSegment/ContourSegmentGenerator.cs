using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WhitTerrain {
	public class ContourSegmentGenerator {
		private ContourSegmentAttributes attributes;
		private int resolution = 3;

		public ContourSegmentGenerator(ContourSegmentAttributes attributes) {
			this.attributes = attributes;
		}

		public List<ContourSegment> GenerateCurve(Contour terrain, Vector2 startPoint, float dist, float curveLength, float startSlope, float endSlope, bool bumpify) {
			float slopeRange = endSlope - startSlope;
			float deltaSlope = slopeRange / (resolution - 1);

			List<ContourSegment> sections = new List<ContourSegment>();

			Vector2 nextStartPoint = startPoint;
			float nextDist = dist;

			for (int i = 0; i < resolution; i++) {
				float sectionLength = curveLength / resolution;
				float sectionSlope = startSlope + deltaSlope * i;
				ContourSegment section = GenerateSection(terrain, nextStartPoint, nextDist, sectionLength, sectionSlope, bumpify);
				nextStartPoint = section.endPoint;
				nextDist = section.distEnd;
				sections.Add(section);
			}
			return sections;
		}

		public List<ContourSegment> GenerateCurve(Contour terrain, ContourSegment previousSection, float sectionLength, float startSlope, float endSlope, bool bumpify) {
			return GenerateCurve(terrain, previousSection.endPoint, previousSection.distEnd, sectionLength, startSlope, endSlope, bumpify);
		}

		public ContourSegment GenerateSection(Contour terrain, Vector2 startPoint, float dist, float length, float slope, bool bumpify) {
			ContourSegmentConfig config = new ContourSegmentConfig();
			config.startPoint = startPoint;
			config.length = length;
			config.slope = slope;
			config.distStart = dist;
			config.bumpify = bumpify;
			return GenerateSection(terrain, config);
		}

		public ContourSegment GenerateSection(Contour terrain, ContourSegment previousSection, float length, float slope, bool bumpify) {
			return GenerateSection(terrain, previousSection.endPoint, previousSection.distEnd, length, slope, bumpify);
		}

		private ContourSegment GenerateSection(Contour terrain, ContourSegmentConfig config) {
			ContourSegment section = new GameObject("Section", typeof(ContourSegment)).GetComponent<ContourSegment>();
			section.Initialize(terrain, config, attributes);
			return section;
		}
	}
}