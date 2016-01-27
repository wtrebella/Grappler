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
	public Action SignalLeftTouchDown;
	public Action SignalLeftTouchUp;
	public Action SignalRightTouchDown;
	public Action SignalRightTouchUp;

	private float minSwipeLength = 50;
	private float maxSwipeDuration = 0.3f;

	private Vector2 beginningSwipePosition;
	private Vector2 endSwipePosition;
	private Vector2 currentSwipeVector;
	private float currentSwipeMagnitude;
	private float beginningSwipeTime;
	private int leftTouchID;
	private int rightTouchID;

	private void Update() {
		if (SystemInfo.deviceType == DeviceType.Handheld) DetectTouchInput();
		else if (SystemInfo.deviceType == DeviceType.Desktop) {
			DetectMouseInput();
			DetectKeyboardInput();
		}
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
	
	private void HandleLeftSwipe() 		{if (SignalLeftSwipe != null) SignalLeftSwipe();}
	private void HandleRightSwipe() 	{if (SignalRightSwipe != null) SignalRightSwipe();}
	private void HandleUpSwipe() 		{if (SignalUpSwipe != null) SignalUpSwipe();}	
	private void HandleDownSwipe()		{if (SignalDownSwipe != null) SignalDownSwipe();}
	private void HandleLeftTouchDown() 	{if (SignalLeftTouchDown != null) SignalLeftTouchDown();}
	private void HandleLeftTouchUp() 	{if (SignalLeftTouchUp != null) SignalLeftTouchUp();}
	private void HandleRightTouchDown() {if (SignalRightTouchDown != null) SignalRightTouchDown();}
	private void HandleRightTouchUp() 	{if (SignalRightTouchUp != null) SignalRightTouchUp();}

	private bool IsOnLeftOfScreen(Vector2 position) {
		return position.x < Screen.width / 2f;
	}




	// TOUCH

	private void DetectTouchInput() {
		DetectTouchSwipes();
		DetectTouchDown();
		DetectTouchUp();
	}

	private void DetectTouchDown() {
		for (int i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.GetTouch(i);

			if (TouchIsDown(touch)) {
				HandleTouchDown();

				if (IsOnLeftOfScreen(touch.position)) {
					leftTouchID = touch.fingerId;
					HandleLeftTouchDown();
				}
				else {
					rightTouchID = touch.fingerId;
					HandleRightTouchDown();
				}
			}
		}
	}

	private void DetectTouchUp() {
		for (int i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.GetTouch(i);
		
			if (TouchIsUp(touch)) {
				HandleTouchUp();

				if (touch.fingerId == leftTouchID) HandleLeftTouchUp();
				else if (touch.fingerId == rightTouchID) HandleRightTouchUp();
			}
		}
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
		return touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
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

	private void DetectKeyboardInput() {
		DetectArrowKeysDown();
		DetectArrowKeysUp();
	}

	private void DetectArrowKeysDown() {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			HandleTouchDown();
			HandleLeftTouchDown();
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			HandleTouchDown();
			HandleRightTouchDown();
		}
	}

	private void DetectArrowKeysUp() {
		if (Input.GetKeyUp(KeyCode.LeftArrow)) {
			HandleTouchUp();
			HandleLeftTouchUp();
		}
		else if (Input.GetKeyUp(KeyCode.RightArrow)) {
			HandleTouchUp();
			HandleRightTouchUp();
		}
	}

	private void DetectMouseSwipes() {			
		if (MouseSwipeBegan()) DoMouseSwipeBeganPhase();
		else if (MouseSwipeEnded()) DoMouseSwipeEndedPhase();
	}

	private void DetectMouseUp() {
		if (Input.GetMouseButtonUp(0)) {
			HandleTouchUp();
			HandleLeftTouchUp();
		}
		else if (Input.GetMouseButtonUp(1)) {
			HandleTouchUp();
			HandleRightTouchUp();
		}
	}
	
	private void DetectMouseDown() {
		if (Input.GetMouseButtonDown(0)) {
			HandleTouchDown();
			HandleLeftTouchDown();
		}
		else if (Input.GetMouseButtonDown(1)) {
			HandleTouchDown();
			HandleRightTouchDown();
		}
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
