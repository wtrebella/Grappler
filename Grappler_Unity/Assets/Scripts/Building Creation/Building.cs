using UnityEngine;
using System.Collections;

public enum BuildingArtStyle {
	Sprite,
	Cube
}

[RequireComponent(typeof(tk2dSlicedSprite))]
public class Building : MonoBehaviour {
	[SerializeField] private Transform buildingCubeHolder;
	[SerializeField] private BuildingArtStyle artStyle = BuildingArtStyle.Cube;

	public Vector2 topLeftCorner {
		get {
			AssertAttributesHaveBeenSet();
			return new Vector2(transform.position.x, transform.position.y + attributes.size.y);
		}
	}

	public Vector2 topRightCorner {
		get {
			AssertAttributesHaveBeenSet();
			return new Vector2(transform.position.x + attributes.size.x, transform.position.y + attributes.size.y);
		}
	}

	public Vector2 bottomLeftCorner {
		get {
			AssertAttributesHaveBeenSet();
			return new Vector2(transform.position.x, transform.position.y);
		}
	}

	public Vector2 bottomRightCorner {
		get {
			AssertAttributesHaveBeenSet();			
			return new Vector2(transform.position.x + attributes.size.x, transform.position.y);
		}
	}

	public Vector2 size {
		get {
			AssertAttributesHaveBeenSet();
			return attributes.size;
		}
	}

	public float width {
		get {
			AssertAttributesHaveBeenSet();
			return attributes.size.x;
		}
	}

	public float height {
		get {
			AssertAttributesHaveBeenSet();
			return attributes.size.y;
		}
	}

	private tk2dSlicedSprite buildingSprite;
	private BuildingAttributes attributes;

	public void SetBuildingAttributes(BuildingAttributes buildingAttributes) {
		SetColor(buildingAttributes.color);
		SetSize(buildingAttributes.size);
		transform.position = buildingAttributes.position;
		attributes = buildingAttributes;
	}

	public bool IsOffLeftOfScreen() {
		return GameScreen.instance.IsOffLeftOfScreenWithMargin(bottomRightCorner.x);
	}

	public bool IsOffRightOfScreen() {
		return GameScreen.instance.IsOffRightOfScreenWithMargin(bottomRightCorner.x);
	}

	private void Awake() {
		buildingSprite = GetComponentInChildren<tk2dSlicedSprite>();
		if (ArtStyleIsCube()) {
			buildingSprite.gameObject.SetActive(false);
			buildingCubeHolder.gameObject.SetActive(true);
		}
		else if (ArtStyleIsSprite()) {
			buildingSprite.gameObject.SetActive(true);
			buildingCubeHolder.gameObject.SetActive(false);
		}
	}
	
	private void SetColor(Color color) {
		if (ArtStyleIsSprite()) buildingSprite.color = color;
		else if (ArtStyleIsCube()) buildingCubeHolder.GetComponentInChildren<MeshRenderer>().material.color = color;
	}

	private void SetSize(Vector2 size) {
		float xInPixels = size.x * WhitTools.UnitsToPixels;
		float yInPixels = size.y * WhitTools.UnitsToPixels;
		if (ArtStyleIsSprite()) buildingSprite.dimensions = new Vector2(xInPixels, yInPixels);
		else if (ArtStyleIsCube()) buildingCubeHolder.localScale = new Vector3(size.x, size.y, size.x);
	}

	private void AssertAttributesHaveBeenSet() {
		WhitTools.Assert(attributes != null, "no attributes set!");
	}

	private bool ArtStyleIsCube() {
		return artStyle == BuildingArtStyle.Cube;
	}

	private bool ArtStyleIsSprite() {
		return artStyle == BuildingArtStyle.Sprite;
	}
}
