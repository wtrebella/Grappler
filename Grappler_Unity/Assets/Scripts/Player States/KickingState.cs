using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KickingState : MonoBehaviour {
	public bool isKicking {get; private set;}

	[SerializeField] private Player player;
	[SerializeField] private Rigidbody2D body;
	[SerializeField] private Rigidbody2D feet;
	[SerializeField] private KinematicSwitcher kinematicSwitcher;

	[SerializeField] private float duration = 0.3f;
	[SerializeField] private float distance = 2;
	[SerializeField] private float extensionDistance = 4;
	[SerializeField] private float vertical = 0.5f;
	[SerializeField] private GoEaseType easeType = GoEaseType.CubicOut;

	private string playerLayerName = "Player";
	private Vector3 kickStart;
	private Vector3 kickEnd;
	private Vector3 extendedStart;
	private Vector3 extendedEnd;
	private List<GameObject> spritesIntersectingKickPath;

	public void Kick() {
		kinematicSwitcher.SetKinematic();
		isKicking = true;

		Vector3 direction = new Vector3(1, vertical, 0);
		direction.Normalize();
		kickStart = body.transform.position;
		kickEnd = body.transform.position + direction * distance;
		Vector3 kickDirection = (kickEnd - kickStart).normalized;
		extendedStart = kickStart - kickDirection * extensionDistance;
		extendedEnd = kickEnd + kickDirection * extensionDistance;

		var sprites = IntersectWithKick(extendedStart, extendedEnd);
		foreach (GameObject sprite in sprites) spritesIntersectingKickPath.Add(sprite);
		StartCoroutine(SliceIntersectingSprites());

		Debug.DrawLine(extendedStart, extendedEnd, Color.red, 2);

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
		float kickDistance = (endPos - startPos).magnitude;
		List<GameObject> sprites = new List<GameObject>();
		var results = Physics2D.RaycastAll(startPos, endPos, kickDistance, ~(1 << LayerMask.NameToLayer(playerLayerName)));
		foreach (RaycastHit2D result in results) {
			if (result.rigidbody) sprites.Add(result.rigidbody.gameObject);
		}
		return sprites;
	}

	private IEnumerator SliceIntersectingSprites() {
		while (spritesIntersectingKickPath.Count > 0) {
			var spritesToSlice = new List<GameObject>();
			foreach (GameObject sprite in spritesIntersectingKickPath) {
				if (body.transform.position.x > sprite.transform.position.x) spritesToSlice.Add(sprite);
			}
			
			foreach (GameObject sprite in spritesToSlice) {
				Rigidbody2D rigid = sprite.GetComponent<Rigidbody2D>();
				rigid.gravityScale = 1;
				rigid.constraints = RigidbodyConstraints2D.None;
				Vector3 kickDirection = (kickEnd - kickStart).normalized;
				Vector3 sliceStart = extendedStart - kickDirection * 10;
				Vector3 sliceEnd = extendedEnd + kickDirection * 10;
				SpriteSlicer2D.SliceSprite(sliceStart, sliceEnd, sprite);
				spritesIntersectingKickPath.Remove(sprite);
			}
			yield return null;
		}
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
}
