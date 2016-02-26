using UnityEngine;
using System.Collections;

public class RigidbodyDebugger : WadeBehaviour 
{
#pragma warning disable 414
    [SerializeField]
    Vector2 _velocity = Vector2.zero;

    Vector2 _prevVelocity = Vector2.zero;

    [SerializeField]
    Vector2 _velocityDelta = Vector2.zero;

    [SerializeField]
    float _magnitude = 0f;

    [SerializeField]
    float _magnitudeDelta = 0f;

	private Rigidbody2D rigid;

	void Awake() {
		rigid = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        _prevVelocity = _velocity;
		_velocity = rigid.velocity;

        _velocityDelta = _velocity - _prevVelocity;

        _magnitude = _velocity.magnitude;
        _magnitudeDelta = _velocityDelta.magnitude;
    }
#pragma warning restore 414
}
