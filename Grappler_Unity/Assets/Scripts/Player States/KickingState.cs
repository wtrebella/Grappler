using UnityEngine;
using System.Collections;

public class KickingState : MonoBehaviour {
	public bool isKicking {get; private set;}

	[SerializeField] private Player player;
	[SerializeField] private Rigidbody2D body;
	[SerializeField] private Rigidbody2D feet;
	[SerializeField] private KinematicSwitcher kinematicSwitcher;

	[SerializeField] private float duration = 0.3f;
	[SerializeField] private float distance = 2;
	[SerializeField] private GoEaseType easeType = GoEaseType.CubicOut;

	public void Kick() {
		kinematicSwitcher.SetKinematic();
		isKicking = true;

		Vector3 currentPosition = transform.position;
		Vector3 postKickPosition = currentPosition + Vector3.right * distance;

		Go.to(player.transform, duration, new GoTweenConfig()
		      .setEaseType(easeType)
		      .position(postKickPosition)
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

	private void KickDone(AbstractGoTween tween) {
		isKicking = false;
		kinematicSwitcher.SetNonKinematic();
		player.SetState(Player.PlayerStates.Falling);
	}
	
	private void Awake() {

	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
