using UnityEngine;
using System.Collections;

public class PlayerCollisionHandler : CollisionHandler {
	private CollisionHandler[] collisionHandlers;

	protected override void BaseAwake() {
		base.BaseAwake();
		collisionHandlers = GetComponentsInChildren<CollisionHandler>();
	}

	public void HandleCollisionEnter(PlayerBodyPart bodyPart, Collision2D collision) {
		foreach (CollisionHandler handler in collisionHandlers) {
			if (handler.HasNoLayer() || handler.ObjectIsInLayer(collision.gameObject)) {
				handler.HandleCollisionEnter(bodyPart.rigid, collision);
			}
		}
	}

	public void HandleCollisionStay(PlayerBodyPart bodyPart, Collision2D collision) {
		foreach (CollisionHandler handler in collisionHandlers) {
			if (handler.HasNoLayer() || handler.ObjectIsInLayer(collision.gameObject)) {
				handler.HandleCollisionStay(bodyPart.rigid, collision);
			}
		}
	}

	public void HandleCollisionExit(PlayerBodyPart bodyPart, Collision2D collision) {
		foreach (CollisionHandler handler in collisionHandlers) {
			if (handler.HasNoLayer() || handler.ObjectIsInLayer(collision.gameObject)) {
				handler.HandleCollisionExit(bodyPart.rigid, collision);
			}
		}
	}

	public void HandleTriggerEnter(PlayerBodyPart bodyPart, Collider2D collider) {
		foreach (CollisionHandler handler in collisionHandlers) {
			if (handler.HasNoLayer() || handler.ObjectIsInLayer(collider.gameObject)) {
				handler.HandleTriggerEnter(bodyPart.rigid, collider);
			}
		}
	}

	public void HandleTriggerStay(PlayerBodyPart bodyPart, Collider2D collider) {
		foreach (CollisionHandler handler in collisionHandlers) {
			if (handler.HasNoLayer() || handler.ObjectIsInLayer(collider.gameObject)) {
				handler.HandleTriggerStay(bodyPart.rigid, collider);
			}
		}
	}

	public void HandleTriggerExit(PlayerBodyPart bodyPart, Collider2D collider) {
		foreach (CollisionHandler handler in collisionHandlers) {
			if (handler.HasNoLayer() || handler.ObjectIsInLayer(collider.gameObject)) {
				handler.HandleTriggerExit(bodyPart.rigid, collider);
			}
		}
	}
}
