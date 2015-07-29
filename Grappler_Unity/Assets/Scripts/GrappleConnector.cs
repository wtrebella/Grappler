using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpringJoint2D))]
public class GrappleConnector : MonoBehaviour {
	public bool isConnected {get {return connectedAnchorable != null;}}

	private Anchorable connectedAnchorable;
	private SpringJoint2D springJoint;

	private void Awake() {
		springJoint = GetComponent<SpringJoint2D>();
		springJoint.enabled = false;
	}

	public void Release() {
		springJoint.enabled = false;
		if (isConnected) {
			connectedAnchorable.HandleRelease();
			connectedAnchorable = null;
		}
	}
	
	public void Connect(Anchorable anchorable) {
		WhitTools.Assert(!isConnected, "already connected to something else! release before connecting.");

		springJoint.connectedBody = anchorable.rigid;
		springJoint.connectedAnchor = anchorable.GetRandomLocalAnchorPoint();
		springJoint.enabled = true;
		connectedAnchorable = anchorable;
		anchorable.HandleConnected();
	}
}
