using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
	[SerializeField] private Transform buildingMeshHolder;
	[SerializeField] private BuildingMeshCreator buildingMeshCreator;
	[SerializeField] private MeshRenderer meshRenderer;

	private BuildingAttributes buildingAttributes;

	public Vector2 topLeftCorner {
		get {
			AssertAttributesHaveBeenSet();
			return buildingAttributes.quad.topLeft;
		}
	}

	public Vector2 topRightCorner {
		get {
			AssertAttributesHaveBeenSet();
			return buildingAttributes.quad.topRight;
		}
	}

	public Vector2 bottomLeftCorner {
		get {
			AssertAttributesHaveBeenSet();
			return buildingAttributes.quad.bottomLeft;
		}
	}

	public Vector2 bottomRightCorner {
		get {
			AssertAttributesHaveBeenSet();			
			return buildingAttributes.quad.bottomRight;
		}
	}

	public Vector2 size {
		get {
			AssertAttributesHaveBeenSet();
			return buildingAttributes.quad.bounds.size;
		}
	}

	public float width {
		get {
			AssertAttributesHaveBeenSet();
			return buildingAttributes.quad.bounds.size.x;
		}
	}

	public float height {
		get {
			AssertAttributesHaveBeenSet();
			return buildingAttributes.quad.bounds.size.y;
		}
	}

	public void SetBuildingAttributes(BuildingAttributes buildingAttributes) {
		this.buildingAttributes = buildingAttributes;
		buildingMeshCreator.InitMesh(buildingAttributes);
		meshRenderer.material.color = buildingAttributes.color;
	}

	public bool IsOffLeftOfScreen() {
		AssertAttributesHaveBeenSet();
		return GameScreen.instance.IsOffLeftOfScreenWithMargin(buildingAttributes.quad.bounds.max.x);
	}

	public bool IsOffRightOfScreen() {
		return GameScreen.instance.IsOffRightOfScreenWithMargin(buildingAttributes.quad.bounds.max.x);
	}

	private void AssertAttributesHaveBeenSet() {
		WhitTools.Assert(buildingAttributes != null, "no attributes set!");
	}
}
