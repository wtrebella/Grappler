using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class GameStatsBridge : MonoBehaviour {
	public static GameStatsBridge instance;

	public UnityEvent SignalCoinCollected;
	public UnityEvent SignalBackflip;
	public UnityEvent SignalFrontFlip;
	public UnityEvent SignalHitGround;
	public UnityEvent SignalHitMountain;
	public UnityEvent SignalHitIcicle;
	public UnityEvent SignalHitTree;
	public UnityEvent SignalHitRockSlide;

	private void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy(gameObject);
		}
	}

	public void OnCoinCollected() {
//		GameStats.OnCoinCollected();
		WhitTools.Invoke(SignalCoinCollected);
	}

	public void OnBackflip() {
//		GameStats.OnBackflip();
		WhitTools.Invoke(SignalBackflip);
	}

	public void OnFrontFlip() {
//		GameStats.OnFrontFlip();
		WhitTools.Invoke(SignalFrontFlip);
	}

	public void OnHitGround() {
//		GameStats.OnHitGround();
		WhitTools.Invoke(SignalHitGround);
	}

	public void OnHitMountain() {
//		GameStats.OnHitMountain();
		WhitTools.Invoke(SignalHitMountain);
	}

	public void OnHitIcicle() {
//		GameStats.OnHitIcicle();
		WhitTools.Invoke(SignalHitIcicle);
	}

	public void OnHitTree() {
//		GameStats.OnHitTree();
		WhitTools.Invoke(SignalHitTree);
	}

	public void OnHitRockSlide() {
//		GameStats.OnHitRockSlide();
		WhitTools.Invoke(SignalHitRockSlide);
	}
}