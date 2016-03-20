using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WhitStateMachine : MonoBehaviour {
	[HideInInspector] public Enum lastState;

	public class State {
		public Action UpdateState = DoNothing;
		public Action FixedUpdateState = DoNothing;
		public Action EnterState = DoNothing;
		public Action ExitState = DoNothing;
		public Action LeftSwipe = DoNothing;
		public Action RightSwipe = DoNothing;
		public Action UpSwipe = DoNothing;
		public Action DownSwipe = DoNothing;
		public Action Tap = DoNothing;
		public Action TouchUp = DoNothing;
		public Action TouchDown = DoNothing;
		public Action LeftTouchDown = DoNothing;
		public Action LeftTouchUp = DoNothing;
		public Action RightTouchDown = DoNothing;
		public Action RightTouchUp = DoNothing;

		public Enum currentState;
	}

	public Enum currentState {
		get {return state.currentState;}
		set {
			if (state.currentState == value) return;

			ChangingState();
			state.currentState = value;
			ConfigureCurrentState();
		}
	}

	public WhitStateController GetStateController(Enum state) {
		WhitStateController controller;

		if (!stateControllersDictionary.TryGetValue(state, out controller)) {
			Debug.LogError("no state controller attached that corresponds to state: " + state.ToString());
		}

		return controller;
	}

	public WhitStateController GetCurrentStateController() {
		return GetStateController(currentState);
	}

	protected float timeEnteredState;

	private State state = new State();
	private Dictionary<Enum, WhitStateController> stateControllersDictionary;
	private Dictionary<Enum, Dictionary<string, Delegate>> allStateDelegates = new Dictionary<Enum, Dictionary<string, Delegate>>();
	private bool swipeDetectionInitiated = false;
	private bool stateControllersInitiated = false;

	private void HandleLeftSwipe() {state.LeftSwipe();}
	private void HandleRightSwipe() {state.RightSwipe();}
	private void HandleUpSwipe() {state.UpSwipe();}
	private void HandleDownSwipe() {state.DownSwipe();}
	private void HandleLeftTouchDown() {state.LeftTouchDown();}
	private void HandleLeftTouchUp() {state.LeftTouchUp();}
	private void HandleRightTouchDown() {state.RightTouchDown();}
	private void HandleRightTouchUp() {state.RightTouchUp();}
	private void HandleTap() {state.Tap();}
	private void HandleTouchUp() {state.TouchUp();}
	private void HandleTouchDown() {state.TouchDown();}

	private void InitStateControllers() {
		WhitStateController[] stateControllers = GetComponentsInChildren<WhitStateController>();
		stateControllersDictionary = new Dictionary<Enum, WhitStateController>();

		foreach (WhitStateController controller in stateControllers) {
			stateControllersDictionary.Add(controller.state, controller);
		}

		stateControllersInitiated = true;
	}

	private void Update() {
		PreUpdateState();
		state.UpdateState();
		PostUpdateState();
	}

	private void FixedUpdate() {
		PreFixedUpdateState();
		state.FixedUpdateState();
		PostFixedUpdateState();
	}

	private void ChangingState() {
		lastState = state.currentState;
		timeEnteredState = Time.time;
	}

	private void ConfigureCurrentState() {
		if (!stateControllersInitiated) InitStateControllers();
		if (!swipeDetectionInitiated) InitiateSwipeDetection();

		if (state.ExitState != null) state.ExitState();

		WhitStateController target = GetCurrentStateController();

		state.UpdateState = 		ConfigureDelegate<Action>("UpdateState", target, DoNothing);
		state.FixedUpdateState = 	ConfigureDelegate<Action>("FixedUpdateState", target, DoNothing);
		state.EnterState = 			ConfigureDelegate<Action>("EnterState", target, DoNothing);
		state.ExitState = 			ConfigureDelegate<Action>("ExitState", target, DoNothing);
		state.LeftSwipe = 			ConfigureDelegate<Action>("LeftSwipe", target, DoNothing);
		state.RightSwipe = 			ConfigureDelegate<Action>("RightSwipe", target, DoNothing);
		state.UpSwipe = 			ConfigureDelegate<Action>("UpSwipe", target, DoNothing);
		state.DownSwipe = 			ConfigureDelegate<Action>("DownSwipe", target, DoNothing);
		state.Tap = 				ConfigureDelegate<Action>("Tap", target, DoNothing);
		state.TouchUp = 			ConfigureDelegate<Action>("TouchUp", target, DoNothing);
		state.TouchDown = 			ConfigureDelegate<Action>("TouchDown", target, DoNothing);
		state.LeftTouchDown = 		ConfigureDelegate<Action>("LeftTouchDown", target, DoNothing);
		state.LeftTouchUp = 		ConfigureDelegate<Action>("LeftTouchUp", target, DoNothing);
		state.RightTouchDown = 		ConfigureDelegate<Action>("RightTouchDown", target, DoNothing);
		state.RightTouchUp = 		ConfigureDelegate<Action>("RightTouchUp", target, DoNothing);

		if (state.EnterState != null) state.EnterState();
	}

	private T ConfigureDelegate<T>(string methodName, object target, T Default) where T : class {
		Dictionary<string, Delegate> currentStateDelegates;
		Delegate currentStateDelegate;

		if (!allStateDelegates.TryGetValue(state.currentState, out currentStateDelegates)) {
			allStateDelegates[state.currentState] = currentStateDelegates = new Dictionary<string, Delegate>();
		}

		if (!currentStateDelegates.TryGetValue(methodName, out currentStateDelegate)) {
			var method = GetType().GetMethod(methodName,
				System.Reflection.BindingFlags.Instance |
				System.Reflection.BindingFlags.Public |
				System.Reflection.BindingFlags.NonPublic |
				System.Reflection.BindingFlags.InvokeMethod);

			if (method != null) currentStateDelegate = Delegate.CreateDelegate(typeof(T), target, method);
			else currentStateDelegate = Default as Delegate;

			currentStateDelegates[methodName] = currentStateDelegate;
		}
		return currentStateDelegate as T;
	}

	protected virtual void PreUpdateState() {}
	protected virtual void PostUpdateState() {}

	protected virtual void PreFixedUpdateState() {}
	protected virtual void PostFixedUpdateState() {}

	private void InitiateSwipeDetection() {
		if (InputManager.instance == null) return;

		swipeDetectionInitiated = true;

		InputManager.instance.SignalTap += HandleTap;
		InputManager.instance.SignalLeftSwipe += HandleLeftSwipe;
		InputManager.instance.SignalRightSwipe += HandleRightSwipe;
		InputManager.instance.SignalUpSwipe += HandleUpSwipe;
		InputManager.instance.SignalDownSwipe += HandleDownSwipe;
		InputManager.instance.SignalTouchDown += HandleTouchDown;
		InputManager.instance.SignalTouchUp += HandleTouchUp;
		InputManager.instance.SignalLeftTouchDown += HandleLeftTouchDown;
		InputManager.instance.SignalLeftTouchUp += HandleLeftTouchUp;
		InputManager.instance.SignalRightTouchDown += HandleRightTouchDown;
		InputManager.instance.SignalRightTouchUp += HandleRightTouchUp;
	}

	private void RemoveSwipeDetection() {
		if (InputManager.instance == null) return;

		InputManager.instance.SignalTap -= HandleTap;
		InputManager.instance.SignalLeftSwipe -= HandleLeftSwipe;
		InputManager.instance.SignalRightSwipe -= HandleRightSwipe;
		InputManager.instance.SignalUpSwipe -= HandleUpSwipe;
		InputManager.instance.SignalDownSwipe -= HandleDownSwipe;
		InputManager.instance.SignalTouchDown -= HandleTouchDown;
		InputManager.instance.SignalTouchUp -= HandleTouchUp;
		InputManager.instance.SignalLeftTouchDown -= HandleLeftTouchDown;
		InputManager.instance.SignalLeftTouchUp -= HandleLeftTouchUp;
		InputManager.instance.SignalRightTouchDown -= HandleRightTouchDown;
		InputManager.instance.SignalRightTouchUp -= HandleRightTouchUp;
	}

	static void DoNothing() {}

	private void OnDestroy() {
		RemoveSwipeDetection();
	}
}
