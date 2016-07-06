using UnityEngine;
using System.Collections;
using WhitDataTypes;

public class Cloud : GeneratableItem {
	[SerializeField] private tk2dSprite sprite;
	[SerializeField] private FloatRange alphaRange;
	[SerializeField] private Vector2Range scaleRange;
	[SerializeField] private string[] spriteNames;

	private void Awake() {
		Generate();
	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}

	private void Generate() {
		SetColor();
		SetScale();
		SetRotation();
		SetSprite();
	}

	private void SetColor() {
		float alpha = Random.Range(alphaRange.min, alphaRange.max);
		sprite.color = new Color(1, 1, 1, alpha);
	}

	private void SetScale() {
		float x = Random.Range(scaleRange.min.x, scaleRange.max.x);
		float y = Random.Range(scaleRange.min.y, scaleRange.max.y);
		if (Random.value < 0.5f) x *= -1;
		sprite.scale = new Vector3(x, y, 1);
	}

	private void SetSprite() {
		int spriteNameIndex = Random.Range(0, spriteNames.Length - 1);
		string spriteName = spriteNames[spriteNameIndex];
		sprite.SetSprite(spriteName);
	}

	private void SetRotation() {
		if (Random.value < 0.5f) transform.eulerAngles = Vector3.zero;
		else transform.eulerAngles = new Vector3(0, 0, 180);
	}
}
