using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FixedJoint))]
public class FixedJointDebugger : WadeBehaviour
{
#pragma warning disable 414
    FixedJoint _fixedJoint = null;
    FixedJoint fixedJoint
    {
        get 
        {
            if (!_fixedJoint)
                _fixedJoint = GetComponent<FixedJoint>();

            return _fixedJoint;
        }
    }

    [SerializeField, Tooltip("Position of the anchor around which the joint's motion is constrained.")]
    Vector3 _anchor = Vector3.zero;

    [SerializeField, Tooltip("Direction of the axis around which the body is constrained")]
    Vector3 _axis = Vector3.zero;

    [SerializeField, Tooltip("Position of the anchor relative to the connected Rigidbody")]
    Vector3 _connectedAnchor = Vector3.zero;

    void OnValidate()
    {
        _axis.Normalize();

        if (_anchor != fixedJoint.anchor)
            fixedJoint.anchor = _anchor;

        if (_axis != fixedJoint.axis)
            fixedJoint.axis = _axis;

        if (_connectedAnchor != fixedJoint.connectedAnchor)
            fixedJoint.connectedAnchor = _connectedAnchor;
    }

    void FixedUpdate()
    {
        _anchor = fixedJoint.anchor;
        _axis = fixedJoint.axis;
        _connectedAnchor = fixedJoint.connectedAnchor;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + _axis * transform.localScale.x);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(fixedJoint.connectedBody.position + fixedJoint.connectedBody.rotation * (_connectedAnchor * transform.localScale.x), transform.localScale.x * 0.3f);
    }
#pragma warning restore 414
}
