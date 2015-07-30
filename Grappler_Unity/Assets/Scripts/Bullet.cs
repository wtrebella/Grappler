using UnityEngine;
using System.Collections;

public enum DirectionType {Left, Right}

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {
	[SerializeField] private tk2dSprite sprite;
	[SerializeField] private float minImpulseForceMagnitude;
	[SerializeField] private float maxImpulseForceMagnitude;

	private DirectionType currentDirectionType;
	private Rigidbody2D rigid;

	private void Awake() {
		rigid = GetComponent<Rigidbody2D>();
		rigid.centerOfMass = new Vector2(1, 0);
	}

	private void FixedUpdate() {
		CheckBounds();
	}

	public static Vector2 GetDirection(DirectionType directionType) {
		if (directionType == DirectionType.Left) return -Vector2.right;
		else if (directionType == DirectionType.Right) return Vector2.right;
		Debug.LogError("invalid direction");
		return Vector2.zero;
	}

	public static DirectionType GetRandomDirectionType() {
		DirectionType directionType = Random.value < 0.5f ? DirectionType.Left : DirectionType.Right;
		return directionType;
	}

	public void Shoot(DirectionType directionType) {
		currentDirectionType = directionType;

		Vector2 position = GetBulletSpawnPoint(directionType);
		Vector2 force = GetImpulseForce(directionType);

		SetSpriteScale(directionType);
		transform.position = position;

		rigid.AddForce(force, ForceMode2D.Impulse);
	}

	public Vector2 GetImpulseForce(DirectionType directionType) {
		Vector2 direction = GetDirection(directionType);
		float magnitude = Random.Range(minImpulseForceMagnitude, maxImpulseForceMagnitude);
		return direction * magnitude;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		TurnUpGravity();
	}

	private void TurnUpGravity() {
		rigid.gravityScale = 1;
	}

	private void TurnDownGravity() {
		rigid.gravityScale = 0.1f;
	}

	private void CheckBounds() {
		if (currentDirectionType == DirectionType.Left) {
			if (transform.position.x < GameScreen.instance.minMarginX) Reset();
		}
		else if (currentDirectionType == DirectionType.Right) {
			if (transform.position.x > GameScreen.instance.maxMarginX) Reset();
		}
	}

	private void Reset() {
		TurnDownGravity();
		rigid.velocity = Vector2.zero;
		rigid.angularVelocity = 0;
		gameObject.Recycle();	
	}

	private void SetSpriteScale(DirectionType directionType) {
		if (directionType == DirectionType.Left) sprite.scale = new Vector3(-1, 1, 1);
		else if (directionType == DirectionType.Right) sprite.scale = new Vector3(1, 1, 1);
		else Debug.LogError("invalid direction");
	}

	private Vector2 GetBulletSpawnPoint(DirectionType directionType) {
		if (directionType == DirectionType.Left) return GetRightBulletSpawnPoint();
		else if (directionType == DirectionType.Right) return GetLeftBulletSpawnPoint();
		else Debug.LogError("invalid direction");
		return Vector2.zero;
	}

	private Vector2 GetRightBulletSpawnPoint() {
		float x = GameScreen.instance.lowerRightWithMargin.x;
		float minY = GameScreen.instance.minMarginY;
		float maxY = GameScreen.instance.maxMarginY;
		float y = Random.Range(minY, maxY);
		return new Vector2(x, y);
	}

	private Vector2 GetLeftBulletSpawnPoint() {
		float x = GameScreen.instance.lowerLeftWithMargin.x;
		float minY = GameScreen.instance.minMarginY;
		float maxY = GameScreen.instance.maxMarginY;
		float y = Random.Range(minY, maxY);
		return new Vector2(x, y);
	}
}
