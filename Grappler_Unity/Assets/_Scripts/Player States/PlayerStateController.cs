using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Player))]
public class PlayerStateController : WhitStateController {
	protected Player player;

	private void Awake() {
		BaseAwake();
	}

	protected override void BaseAwake() {
		base.BaseAwake();
		player = GetComponent<Player>();
	}
}
