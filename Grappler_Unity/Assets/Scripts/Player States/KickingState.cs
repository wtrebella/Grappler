using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KickingState : MonoBehaviour {
	[SerializeField] private Player player;
	[SerializeField] private Rigidbody2D body;
	[SerializeField] private Rigidbody2D feet;

	[SerializeField] private float duration = 0.3f;
	[SerializeField] private float distance = 2;
	[SerializeField] private Vector3 direction = new Vector3(1, 0.5f);
	[SerializeField] private GoEaseType easeType = GoEaseType.CubicOut;

	private Vector3 kickStart;
	private Vector3 kickEnd;
	private Vector3 kickDirection;

	public void Kick() {
		PrepareKick();
		SliceIntersectingSprites();
		MovePlayer();
	}
	
	public float GetKickDuration() {
		return duration;
	}

	private void MovePlayer() {
		Go.to(body.transform, duration, new GoTweenConfig()
		      .setEaseType(easeType)
		      .position(kickEnd)
		      .onComplete(HandleKickDone));
		
		Go.to(body.transform, duration / 4, new GoTweenConfig()
		      .setEaseType(easeType)
		      .rotation(Vector3.zero));
		
		Go.to(feet.transform, duration / 4, new GoTweenConfig()
		      .setEaseType(easeType)
		      .rotation(Vector3.zero));
	}

	private List<GameObject> IntersectWithKick() {
		int layerMask = ~(1 << LayerMask.NameToLayer("Player"));
		var results = Physics2D.RaycastAll(kickStart, kickEnd, distance, layerMask);

		List<GameObject> sprites = new List<GameObject>();
		foreach (RaycastHit2D result in results) {
			if (result.rigidbody) sprites.Add(result.rigidbody.gameObject);
		}
		return sprites;
	}

	private void SliceIntersectingSprites() {
		StartCoroutine(SliceIntersectingSpritesCoroutine());
	}

	private IEnumerator SliceIntersectingSpritesCoroutine() {
		var intersectingSprites = IntersectWithKick();

		while (intersectingSprites.Count > 0) {
			var spritesToSlice = new List<GameObject>();
			foreach (GameObject sprite in intersectingSprites) {
				if (body.transform.position.x > sprite.transform.position.x) spritesToSlice.Add(sprite);
			}
			
			foreach (GameObject sprite in spritesToSlice) {
				Rigidbody2D rigid = sprite.GetComponent<Rigidbody2D>();
				rigid.gravityScale = 1;
				rigid.constraints = RigidbodyConstraints2D.None;
				Vector3 sliceStart = kickStart - kickDirection * 10;
				Vector3 sliceEnd = kickEnd + kickDirection * 10;
				SpriteSlicer2D.SliceSprite(sliceStart, sliceEnd, sprite);
				intersectingSprites.Remove(sprite);
			}
			yield return null;
		}
	}

	private void HandleKickDone(AbstractGoTween tween) {
		player.kinematicSwitcher.SetNonKinematic();
		player.SetState(Player.PlayerStates.Falling);
	}

	private void PrepareKick() {
		player.kinematicSwitcher.SetKinematic();
		
		kickStart = body.transform.position;
		kickEnd = body.transform.position + direction * distance;
		kickDirection = (kickEnd - kickStart).normalized;
	}
	
	private void Awake() {
		direction.Normalize();
	}

	private void Start() {
	
	}
}
