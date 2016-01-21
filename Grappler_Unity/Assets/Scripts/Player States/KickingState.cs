using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KickingState : MonoBehaviour {
	public bool isKicking {get; private set;}

	[SerializeField] private Player player;
	[SerializeField] private Rigidbody2D body;
	[SerializeField] private Rigidbody2D feet;
	[SerializeField] private KinematicSwitcher kinematicSwitcher;

	[SerializeField] private Vector3 kickStart;
	[SerializeField] private Vector3 kickEnd;
	[SerializeField] private float duration = 0.3f;
	[SerializeField] private float distance = 2;
	[SerializeField] private GoEaseType easeType = GoEaseType.CubicOut;

	private List<GameObject> spritesIntersectingKickPath;

	public void Kick() {
		kinematicSwitcher.SetKinematic();
		isKicking = true;

		kickStart = body.position;
		kickEnd = kickStart + Vector3.right * distance;

		var sprites = IntersectWithKick(kickStart, kickEnd);
		Debug.Log(sprites.Count);
		Debug.DrawLine(kickStart, kickEnd, Color.red, 5);
		foreach (GameObject sprite in sprites) spritesIntersectingKickPath.Add(sprite);

		Go.to(body.transform, duration, new GoTweenConfig()
		      .setEaseType(easeType)
		      .position(kickEnd)
		      .onComplete(KickDone));

		Go.to(body.transform, duration / 4, new GoTweenConfig()
		      .setEaseType(easeType)
		      .rotation(Vector3.zero));

		Go.to(feet.transform, duration / 4, new GoTweenConfig()
		      .setEaseType(easeType)
		      .rotation(Vector3.zero));
	}

	public float GetKickDuration() {
		return duration;
	}

	private List<GameObject> IntersectWithKick(Vector2 startPos, Vector2 endPos) {
		List<GameObject> sprites = new List<GameObject>();
		var results = Physics2D.RaycastAll(startPos, endPos, distance);
		foreach (RaycastHit2D result in results) {
			if (result.rigidbody) sprites.Add(result.rigidbody.gameObject);
		}
		return sprites;
	}

	private float GetDistanceTraveled() {
		return (transform.position - kickStart).magnitude;
	}

	private void KickDone(AbstractGoTween tween) {
		isKicking = false;
		kinematicSwitcher.SetNonKinematic();
		player.SetState(Player.PlayerStates.Falling);
	}
	
	private void Awake() {
		spritesIntersectingKickPath = new List<GameObject>();
	}

	private void Start() {
	
	}
	
	private void Update() {
		if (isKicking) {
			float distanceTraveled = GetDistanceTraveled();
			var spritesToSlice = new List<GameObject>();
			foreach (GameObject sprite in spritesIntersectingKickPath) {
				float spriteDistance = (sprite.transform.position - kickStart).magnitude;
				if (distanceTraveled > spriteDistance) spritesToSlice.Add(sprite);
			}
			foreach (GameObject sprite in spritesToSlice) {
				Rigidbody2D rigid = sprite.GetComponent<Rigidbody2D>();
				rigid.gravityScale = 1;
				rigid.constraints = RigidbodyConstraints2D.None;
				var infoList = new List<SpriteSlicer2DSliceInfo>();
				SpriteSlicer2D.SliceSprite(kickStart, kickEnd, sprite, true, ref infoList);
				foreach (SpriteSlicer2DSliceInfo info in infoList) {
					var childObjects = info.ChildObjects;
					foreach (GameObject childObject in childObjects) {
						childObject.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * 2, ForceMode2D.Impulse);
					}
				}
				spritesIntersectingKickPath.Remove(sprite);
			}
		}
	}
}
