using UnityEngine;
using System.Collections;
using System;

public class InputManager : MonoBehaviour {
	private static InputManager _instance;
	public static InputManager instance {
		get {
			if (_instance == null) {
				InputManager detector = GameObject.FindObjectOfType<InputManager>();
				if (detector) _instance = detector;
				else {
					GameObject go = new GameObject("Input Manager");
					_instance = go.AddComponent<InputManager>();
					DontDestroyOnLoad(go);
				}
			}
			return _instance;
		}
	}

	public static bool IsInstantiated() {
		return _instance != null;
	}

	public Action SignalTap;
	public Action SignalRightSwipe;
	public Action SignalLeftSwipe;
	public Action SignalUpSwipe;
	public Action SignalDownSwipe;
	public Action SignalTouchDown;
	public Action SignalTouchUp;

	private float minSwipeLength = 50;
	private float maxSwipeDuration = 0.3f;

	private Vector2 beginningSwipePosition;
	private Vector2 endSwipePosition;
	private Vector2 currentSwipeVector;
	private float currentSwipeMagnitude;
	private float beginningSwipeTime;

	private void Update() {
		if (SystemInfo.deviceType == DeviceType.Handheld) DetectTouchInput();
		else if (SystemInfo.deviceType == DeviceType.Desktop) DetectMouseInput();
	}

	private float GetSwipeDeltaTime() {
		return Time.time - beginningSwipeTime;
	}

	private void HandleTap() {
		if (SignalTap != null) SignalTap();
	}

	private void HandleTouchUp() {
		if (SignalTouchUp != null) SignalTouchUp();
	}

	private void HandleTouchDown() {
		if (SignalTouchDown != null) SignalTouchDown();
	}

	private void HandleSwipe(Vector2 swipeDirection, float swipeMagnitude) {
		// horizontal swipe
		if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y)) {
			if (swipeDirection.x > 0) HandleRightSwipe();
			else HandleLeftSwipe();
		}
		// vertical swipe
		else {
			if (swipeDirection.y > 0) HandleUpSwipe();
			else HandleDownSwipe();
		}
	}
	
	private void HandleLeftSwipe() {
		if (SignalLeftSwipe != null) SignalLeftSwipe();
	}
	
	private void HandleRightSwipe() {
		if (SignalRightSwipe != null) SignalRightSwipe();
	}
	
	private void HandleUpSwipe() {
		if (SignalUpSwipe != null) SignalUpSwipe();
	}
	
	private void HandleDownSwipe() {
		if (SignalDownSwipe != null) SignalDownSwipe();
	}

	// TOUCH

	private void DetectTouchInput() {
		DetectTouchSwipes();
		DetectTouchDown();
		DetectTouchUp();
	}

	private void DetectTouchDown() {
		if (Input.touches.Length == 0) return;
		
		Touch touch = Input.GetTouch(0);

		if (TouchIsDown(touch)) HandleTouchDown();
	}

	private void DetectTouchUp() {
		if (Input.touches.Length == 0) return;
		
		Touch touch = Input.GetTouch(0);
		
		if (TouchIsDown(touch)) HandleTouchUp();
	}

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

	private bool TouchIsDown(Touch touch) {
		return touch.phase == TouchPhase.Began;
	}
	
	private bool TouchIsUp(Touch touch) {
		return touch.phase == TouchPhase.Ended && touch.phase == TouchPhase.Canceled;
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




	// MOUSE

	private void DetectMouseInput() {
		DetectMouseSwipes();
		DetectMouseDown();
		DetectMouseUp();
	}

	private void DetectMouseSwipes() {			
		if (MouseSwipeBegan()) DoMouseSwipeBeganPhase();
		else if (MouseSwipeEnded()) DoMouseSwipeEndedPhase();
	}

	private void DetectMouseUp() {
		if (Input.GetMouseButtonUp(0)) HandleTouchUp();
	}
	
	private void DetectMouseDown() {
		if (Input.GetMouseButtonDown(0)) HandleTouchDown();
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
