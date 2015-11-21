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
			return buildingAttributes.skewedRect.topLeft;
		}
	}

	public Vector2 topRightCorner {
		get {
			AssertAttributesHaveBeenSet();
			return buildingAttributes.skewedRect.topRight;
		}
	}

	public Vector2 bottomLeftCorner {
		get {
			AssertAttributesHaveBeenSet();
			return buildingAttributes.skewedRect.bottomLeft;
		}
	}

	public Vector2 bottomRightCorner {
		get {
			AssertAttributesHaveBeenSet();			
			return buildingAttributes.skewedRect.bottomRight;
		}
	}

	public Vector2 size {
		get {
			AssertAttributesHaveBeenSet();
			return buildingAttributes.skewedRect.bounds.size;
		}
	}

	public float width {
		get {
			AssertAttributesHaveBeenSet();
			return buildingAttributes.skewedRect.bounds.size.x;
		}
	}

	public float height {
		get {
			AssertAttributesHaveBeenSet();
			return buildingAttributes.skewedRect.bounds.size.y;
		}
	}

	public void SetBuildingAttributes(BuildingAttributes buildingAttributes) {
//		transform.position = buildingAttributes.skewedRect.bounds.min;
		this.buildingAttributes = buildingAttributes;
		buildingMeshCreator.InitMesh(buildingAttributes);
		meshRenderer.material.color = buildingAttributes.color;
	}

	public bool IsOffLeftOfScreen() {
		AssertAttributesHaveBeenSet();
		return GameScreen.instance.IsOffLeftOfScreenWithMargin(buildingAttributes.skewedRect.bounds.max.x);
	}

	public bool IsOffRightOfScreen() {
		return GameScreen.instance.IsOffRightOfScreenWithMargin(buildingAttributes.skewedRect.bounds.max.x);
	}

	private void AssertAttributesHaveBeenSet() {
		WhitTools.Assert(buildingAttributes != null, "no attributes set!");
	}
}
