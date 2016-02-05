using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
	[SerializeField] private Transform buildingMeshHolder;
	[SerializeField] private BuildingMeshCreator buildingMeshCreator;
	[SerializeField] private MeshRenderer meshRenderer;

	private BuildingAttributes _attributes;
	public BuildingAttributes attributes {
		get {
			AssertAttributesHaveBeenSet();
			return _attributes;
		} 
	}

	public Vector2 topLeftCorner {
		get {
			AssertAttributesHaveBeenSet();
			return _attributes.quad.topLeft;
		}
	}

	public Vector2 topRightCorner {
		get {
			AssertAttributesHaveBeenSet();
			return _attributes.quad.topRight;
		}
	}

	public Vector2 bottomLeftCorner {
		get {
			AssertAttributesHaveBeenSet();
			return _attributes.quad.bottomLeft;
		}
	}

	public Vector2 bottomRightCorner {
		get {
			AssertAttributesHaveBeenSet();			
			return _attributes.quad.bottomRight;
		}
	}

	public Vector2 size {
		get {
			AssertAttributesHaveBeenSet();
			return _attributes.quad.bounds.size;
		}
	}

	public float width {
		get {
			AssertAttributesHaveBeenSet();
			return _attributes.quad.bounds.size.x;
		}
	}

	public float height {
		get {
			AssertAttributesHaveBeenSet();
			return _attributes.quad.bounds.size.y;
		}
	}

	public void SetBuildingAttributes(BuildingAttributes newAttributes) {
		_attributes = newAttributes;
		buildingMeshCreator.InitMesh(_attributes);
		meshRenderer.material.color = _attributes.color;
		meshRenderer.sortingOrder = newAttributes.sortingOrder;
	}

	public bool IsOffLeftOfScreen() {
		AssertAttributesHaveBeenSet();
		return GameScreen.instance.IsOffLeftOfScreenWithMargin(attributes.quad.bounds.max.x);
	}

	public bool IsOffRightOfScreen() {
		AssertAttributesHaveBeenSet();
		return GameScreen.instance.IsOffRightOfScreenWithMargin(attributes.quad.bounds.max.x);
	}

	private void AssertAttributesHaveBeenSet() {
		WhitTools.Assert(_attributes != null, "no attributes set!");
	}
}
