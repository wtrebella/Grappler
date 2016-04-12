using UnityEngine;
using System.Collections;

public abstract class PickupEffect : MonoBehaviour {
	public PickupEffectType pickupType {get; protected set;}

	[SerializeField] private float duration = 5.0f;

	private Timer timer;

	public virtual void Run() {
		CreateAndStartTimer();
		ImplementEffect();
	}

	private void Awake() {
		Initialize();
	}

	protected virtual void Initialize() {

	}
		
	protected virtual void ImplementEffect() {

	}

	protected virtual void RemoveEffect() {

	}

	private void OnReachedGoalTime() {
		RemoveEffect();
		DestroyTimer();
	}

	private void CreateAndStartTimer() {
		timer = WhitTools.CreateGameObjectWithComponent<Timer>("Pickup Timer");
		timer.SignalReachedGoalTime += OnReachedGoalTime;
		timer.SetUpdateType(WhitUpdateType.Update);
		timer.SetGoalTime(duration);
		timer.StartTimer();
	}

	private void DestroyTimer() {
		Destroy(timer.gameObject);
	}
}
