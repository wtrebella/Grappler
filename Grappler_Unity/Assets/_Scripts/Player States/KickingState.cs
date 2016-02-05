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

	private GameObject intersectingSprite;
	private float initialHorizontalVelocity;
	private Vector3 kickStart;
	private Vector3 kickEnd;

	public void Kick() {
		PrepareKick();
		StartCoroutine(SliceIntersectingSprite());
		MovePlayer();
	}
	
	public float GetKickDuration() {
		return duration;
	}

	private void CancelMove() {
		Go.killAllTweensWithTarget(body.transform);
		Go.killAllTweensWithTarget(feet.transform);
		HandleKickDone(null);
	}

	private void MovePlayer() {
		Vector2 endPos = kickEnd;

		Go.to(body.transform, duration, new GoTweenConfig()
		      .setEaseType(easeType)
		      .position(endPos)
		      .onComplete(HandleKickDone));
		
		Go.to(body.transform, duration / 4, new GoTweenConfig()
		      .setEaseType(easeType)
		      .rotation(Vector3.zero));
		
		Go.to(feet.transform, duration / 4, new GoTweenConfig()
		      .setEaseType(easeType)
		      .rotation(Vector3.zero));
	}

	private GameObject IntersectWithKick() {
		int layerMask = 1 << LayerMask.NameToLayer("Kickable");
		var result = Physics2D.Raycast(kickStart, kickEnd, distance, layerMask);

		if (result.rigidbody) return result.rigidbody.gameObject;
		else return null;
	}

	private IEnumerator SliceIntersectingSprite() {
		intersectingSprite = IntersectWithKick();

		if (intersectingSprite != null) {
			while (body.transform.position.x < intersectingSprite.transform.position.x - 1) yield return null;

			ScreenShaker.instance.ShakeMin();

			Rigidbody2D rigid = intersectingSprite.GetComponent<Rigidbody2D>();
			rigid.gravityScale = 1;
			rigid.constraints = RigidbodyConstraints2D.None;
			SpriteSlicer2D.SliceSprite(kickStart, kickEnd, intersectingSprite);
		}

		yield return null;
	}

	private void HandleKickDone(AbstractGoTween tween) {
		player.kinematicSwitcher.SetNonKinematic();
		player.SetState(Player.PlayerStates.Falling);
		body.GetComponent<Rigidbody2D>().velocity = new Vector3(initialHorizontalVelocity, 0, 0);
		feet.GetComponent<Rigidbody2D>().velocity = new Vector3(initialHorizontalVelocity, 0, 0);;
	}

	private void PrepareKick() {
		initialHorizontalVelocity = body.GetComponent<Rigidbody2D>().velocity.x;

		player.kinematicSwitcher.SetKinematic();
		kickStart = body.transform.position;
		kickEnd = body.transform.position + direction * distance;
	}
	
	private void Awake() {
		direction.Normalize();
	}

	private void Start() {
	
	}

	private void OnDestroy() {
		Go.killAllTweensWithTarget(body.transform);
		Go.killAllTweensWithTarget(feet.transform);
	}
}
