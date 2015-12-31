using UnityEngine;
using System.Collections;
using System;

public class SwipeDetector : MonoBehaviour {
	public static SwipeDetector instance;

	public Action<Vector2, float> SignalSwipe;
	public Action SignalTap;

	[SerializeField] private float minSwipeLength = 50;
	[SerializeField] private float maxSwipeDuration = 0.3f;

	private Vector2 beginningSwipePosition;
	private Vector2 endSwipePosition;
	private Vector2 currentSwipeVector;
	private float currentSwipeMagnitude;
	private float beginningSwipeTime;

	private void Awake() {
		instance = this;
	}

	private void Update() {
		if (SystemInfo.deviceType == DeviceType.Handheld) DetectTouchSwipes();
		else if (SystemInfo.deviceType == DeviceType.Desktop) DetectMouseSwipes();
	}

	private float GetSwipeDeltaTime() {
		return Time.time - beginningSwipeTime;
	}

	private void HandleTap() {
		if (SignalTap != null) SignalTap();
	}

	private void HandleSwipe(Vector2 swipeDirection, float swipeMagnitude) {
		if (SignalSwipe != null) SignalSwipe(swipeDirection, swipeMagnitude);
	}

	// TOUCH SWIPES

	private void DetectTouchSwipes() {
		if (Input.touches.Length == 0) return;
		
		Touch touch = Input.GetTouch(0);
		
		if (TouchSwipeBegan(touch)) DoTouchSwipeBeganPhase(touch);
		else if (TouchSwipeEnded(touch)) DoTouchSwipeEndedPhase(touch);
	}

	private bool TouchSwipeBegan(Touch touch) {
		return touch.phase == TouchPhase.Began;
	}

	private bool TouchSwipeEnded(Touch touch) {
		return touch.phase == TouchPhase.Ended && GetSwipeDeltaTime() <= maxSwipeDuration;
	}


	private void DoTouchSwipeBeganPhase(Touch touch) {
		beginningSwipePosition = touch.position;
		beginningSwipeTime = Time.time;
	}
	
	private void DoTouchSwipeEndedPhase(Touch touch) {
		endSwipePosition = touch.position;

		currentSwipeVector = new Vector2(endSwipePosition.x - beginningSwipePosition.x, endSwipePosition.y - beginningSwipePosition.y);
		currentSwipeMagnitude = currentSwipeVector.magnitude;
		if (currentSwipeMagnitude >= minSwipeLength) {
			Vector2 swipeDirection = currentSwipeVector.normalized;
			HandleSwipe(swipeDirection, currentSwipeMagnitude);
		}
		else HandleTap();
	}




	// MOUSE SWIPES

	private void DetectMouseSwipes() {			
		if (MouseSwipeBegan()) DoMouseSwipeBeganPhase();
		else if (MouseSwipeEnded()) DoMouseSwipeEndedPhase();
	}
	
	private bool MouseSwipeBegan() {
		return Input.GetMouseButtonDown(0);
	}
	
	private bool MouseSwipeEnded() {
		return Input.GetMouseButtonUp(0) && GetSwipeDeltaTime() <= maxSwipeDuration;
	}

	private void DoMouseSwipeBeganPhase() {
		beginningSwipePosition = Input.mousePosition.ToVector2();
		beginningSwipeTime = Time.time;
	}

	private void DoMouseSwipeEndedPhase() {
		endSwipePosition = Input.mousePosition.ToVector2();

		currentSwipeVector = new Vector2(endSwipePosition.x - beginningSwipePosition.x, endSwipePosition.y - beginningSwipePosition.y);
		currentSwipeMagnitude = currentSwipeVector.magnitude;
		if (currentSwipeMagnitude >= minSwipeLength) {
			Vector2 swipeDirection = currentSwipeVector.normalized;
			HandleSwipe(swipeDirection, currentSwipeMagnitude);
		}
		else HandleTap();
	}
}
