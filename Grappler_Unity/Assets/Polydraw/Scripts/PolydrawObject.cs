using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using WhitDataTypes;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Polydraw {
	public class PolydrawObject : MonoBehaviour
	{
	#region Classes

		public struct UVSettings
		{
			public Vector2 offset;
			public Vector2 scale;
			public float rotation;
		}
	#endregion

	#region Members

		public List<PolydrawPoint2> points = new List<PolydrawPoint2>();
		public DrawSettings drawSettings = new DrawSettings();
		public DrawStyle drawStyle = DrawStyle.Point;

		private bool _isEditable = true;
		public bool isEditable { get { return _isEditable; } }

		// interface settings
		public bool t_showSideSettings = true;
		public bool t_showTextureSettings = true;
		public bool t_showCollisionSettings = false;

		#if UNITY_EDITOR
		public int lastIndex = -1;
		public bool isDraggingPoint = false;
		public Vector2 handleOffset = Vector2.zero;
		#endif
	#endregion

	#region Query

		public bool isValid 
		{
			get
			{
				return points.Count > 2;
			}
		}
	#endregion

	#region Constructors

		public static PolydrawObject CreateInstance()
		{
			GameObject go = new GameObject();
			go.name = "Polydraw"+go.GetInstanceID();
			go.AddComponent<MeshFilter>();
			go.AddComponent<MeshRenderer>();

			return go.AddComponent<PolydrawObject>();
		}

		void OnValidate()
		{
			Refresh();
		}

		private void Awake() 
		{
			Refresh();
		}

		public void SetEditable(bool yayornay)
		{
			_isEditable = yayornay;
		}
	#endregion

	#region Object Editing

		public int AddPoint(Vector2 point, int insertPoint)
		{
			if(insertPoint < 0 || insertPoint > points.Count-1)
				points.Add(new PolydrawPoint2(transform.InverseTransformPoint(point.ToVector3(drawSettings.axis, drawSettings.zPosition)).ToVector2(drawSettings.axis)));
			else
				points.Insert(insertPoint, new PolydrawPoint2(transform.InverseTransformPoint(point.ToVector3(drawSettings.axis, drawSettings.zPosition)).ToVector2(drawSettings.axis)));

			#if UNITY_EDITOR
				EditorUtility.SetDirty(this);
			#endif

			return (insertPoint < 0 || insertPoint > points.Count-1) ? points.Count-1 : insertPoint;
		}

		public void SetPoint(int index, Vector2 point)
		{
			if(index > -1 && index < points.Count) {
				PolydrawPoint2 currentPoint = points[index];
				PolydrawPoint2 newPoint = new PolydrawPoint2(transform.InverseTransformPoint(point.ToVector3(drawSettings.axis, drawSettings.zPosition)).ToVector2(drawSettings.axis));
				newPoint.borderIgnored = currentPoint.borderIgnored;
				points[index] = newPoint;
			}

			#if UNITY_EDITOR
				EditorUtility.SetDirty(this);
			#endif
		}

		public void RemovePointAtIndex(int index)
		{
			if(index < points.Count && index > -1)
				points.RemoveAt(index);

			#if UNITY_EDITOR
				EditorUtility.SetDirty(this);
			#endif
		}

		public void ReverseVertOrder() {
			List<PolydrawPoint2> newList = new List<PolydrawPoint2>();
			for (int i = points.Count - 1; i >= 0; i--) newList.Add(points[i]);
			points = newList;

			#if UNITY_EDITOR
				EditorUtility.SetDirty(this);
			#endif
		}

		public void ToggleBorderIgnoredForPointAtIndex(int index) 
		{
			if(index < points.Count && index > -1) {
				PolydrawPoint2 point = points[index];
				point.borderIgnored = !point.borderIgnored;
			}

			#if UNITY_EDITOR
				EditorUtility.SetDirty(this);
			#endif
		}

		public void ClearPoints()
		{
			points.Clear();

			#if UNITY_EDITOR
				EditorUtility.SetDirty(this);
			#endif
		}

		public List<Vector2> GetWorldBorderPoints() 
		{
			List<Vector2> v = GetLocalBorderPoints();
			for (int i = 0; i < v.Count; i++) {
				v[i] = transform.TransformPoint(v[i]);
			}
			return v;
		}

		public List<Vector2> GetLocalBorderPoints() 
		{
			List<Vector2> v = new List<Vector2>();
			foreach (PolydrawPoint2 point in points) {
				if (point.borderIgnored) continue;
				v.Add(point.vector);
			}
			return v;
		}

		public void Refresh()
		{
			if(points.Count < 1)
				return;
			
			Mesh m, c;
			Draw.PolygonType convexity;
			
			if(!DrawUtility.MeshWithPoints(GetPointVectors(), drawSettings, out m, out c, out convexity))
			{
				if(m) DestroyImmediate(m);
				if(c) DestroyImmediate(c);
				return;
			}

			gameObject.SetMesh(m);

			if(drawSettings.colliderType == Draw.ColliderType.MeshCollider)
			{
				if( GetComponent<PolygonCollider2D>() )
					DestroyImmediate( GetComponent<PolygonCollider2D>() );
					
				gameObject.SetMeshCollider(c);
			}

			MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();		
			if(mr == null) mr = gameObject.AddComponent<MeshRenderer>();

			if(drawSettings.generateSide && drawSettings.generateBackFace)
			{
				mr.sharedMaterials = new Material[] {
						drawSettings.frontMaterial,
						drawSettings.sideMaterial,
						drawSettings.frontMaterial
					};
			}	
			else if(drawSettings.generateSide)
			{
				mr.sharedMaterials = new Material[] {
						drawSettings.frontMaterial,
						drawSettings.sideMaterial,
					};
			}
			else if(drawSettings.generateBackFace)
			{
				mr.sharedMaterials = new Material[] {
						drawSettings.frontMaterial,
						drawSettings.frontMaterial
					};	
			}
			else
			{
				mr.sharedMaterials = new Material[] { drawSettings.frontMaterial };
			}

	#if UNITY_EDITOR
			UnityEditor.EditorApplication.delayCall += this.RefreshCollisions;
	#else
			RefreshCollisions();
	#endif
		}

		public void DestroyMesh()
		{
			MeshFilter mf = transform.GetComponent<MeshFilter>();
			if(mf != null)
				DestroyImmediate(mf.sharedMesh);

			RemoveCollisions();
		}
	#endregion

	#region collisions

		public void RefreshCollisions()
		{
			Mesh c = gameObject.GetComponent<MeshCollider>() ? gameObject.GetComponent<MeshCollider>().sharedMesh : null;
			c = PolydrawExtensions.CopyCollisionMesh(c);

			bool isTrigger = gameObject.GetComponent<Collider>() == null ? false : gameObject.GetComponent<Collider>().isTrigger;
			bool isConvex = gameObject.GetComponent<MeshCollider>() == null ? false : gameObject.GetComponent<MeshCollider>().convex;
			
			RemoveCollisions();

			Rigidbody oldRigidbody = gameObject.GetComponent<Rigidbody>();
			bool hasRigidbody = (oldRigidbody != null);
			if(hasRigidbody) 
			{
				CopyRigidbodySettings();
				DestroyImmediate(gameObject.GetComponent<Rigidbody>());
			}

			switch(drawSettings.colliderType)
			{
				case Draw.ColliderType.MeshCollider:
					gameObject.SetMeshCollider(c);
					break;
				case Draw.ColliderType.BoxCollider:
					BuildBoxCollisions();
					if(c != null)
						DestroyImmediate(c);
					break;
				case Draw.ColliderType.PolygonCollider2d:
					BuildPoly2dCollisions();
					if(c != null)
						DestroyImmediate(c);
					break;
				default:
					DestroyImmediate(c);
					break;
			}	


			if(hasRigidbody)
			{
				if(!gameObject.GetComponent<Rigidbody>()) gameObject.AddComponent<Rigidbody>();
				PasteRigidbodySettings();
			}

			if(gameObject.GetComponent<Collider>())
			{
				gameObject.GetComponent<Collider>().isTrigger = isTrigger;
				if(gameObject.GetComponent<MeshCollider>())
					gameObject.GetComponent<MeshCollider>().convex = isConvex;
			}
		}

		/**
		 * Remove all colliders associated with this gameobject.
		 */
		private void RemoveCollisions()
		{
			Collider[] collisions = gameObject.GetComponents<Collider>();
			
			foreach(Collider col in collisions)
			{
				if(col.GetType() == typeof(MeshCollider)) DestroyImmediate( ((MeshCollider)col).sharedMesh);
				{
					#if UNITY_EDITOR
						if( PrefabUtility.GetPrefabType( gameObject ) == PrefabType.Prefab )
							DestroyImmediate(col, true);
					#else
						DestroyImmediate(col);
					#endif
				}
			}

			Collider2D[] collisions2d = gameObject.GetComponents<Collider2D>();

			foreach(Collider2D col2 in collisions2d)
			{
				DestroyImmediate(col2, true);
			}
			
			// remove old box collisions, if any
			if(transform.childCount > 0)
				foreach(BoxCollider bc in transform.GetComponentsInChildren<BoxCollider>())
					DestroyImmediate(bc.gameObject);

		}

		public void SetPointVectors(List<Vector2> pointVectors) {
			ClearPoints();
			foreach (Vector2 point in pointVectors) {
				points.Add(new PolydrawPoint2(point));
			}
		}

		public List<Vector2> GetPointVectors() {
			List<Vector2> pointVectors = new List<Vector2>();
			foreach (PolydrawPoint2 point in points) pointVectors.Add(point.vector);
			return pointVectors;
		}

		private void BuildPoly2dCollisions()
		{
			PolygonCollider2D poly = gameObject.AddComponent<PolygonCollider2D>();
			poly.sharedMaterial = drawSettings.physicsMaterial;
			poly.points = GetPointVectors().ToArray();
		}

		private void BuildBoxCollisions()
		{	
			float zPos_collider = 0f;

			switch(drawSettings.colAnchor)
			{
				case Draw.Anchor.Front:
					zPos_collider = drawSettings.zPosition + drawSettings.faceOffset + drawSettings.colDepth/2f;
					break;
				case Draw.Anchor.Back:
					zPos_collider = drawSettings.zPosition + drawSettings.faceOffset - drawSettings.colDepth/2f;
					break;
				default:
				 	zPos_collider = drawSettings.zPosition + drawSettings.faceOffset;
					break;
			}

			for(int i = 0; i < points.Count; i++)
			{
				float x1, x2, y1, y2;
				x1 = points[i].x;
				y1 = points[i].y;

				if(i > points.Count-2) {
					x2 = points[0].x;
					y2 = points[0].y;
				}
				else {
					x2 = points[i+1].x;
					y2 = points[i+1].y;			
				}

				GameObject boxColliderObj = new GameObject();
				boxColliderObj.name = "BoxCollider" + i;
				boxColliderObj.AddComponent<BoxCollider>();
				
				float x = ((x1+x2)/2f);
				float y = ((y1+y2)/2f);

				Vector2 perp = new Vector2(-(y2-y1), x2-x1).normalized * (drawSettings.boxColliderSize/2f);

				Vector2 vectorLength = new Vector2( Mathf.Abs(x1 - x2),  Mathf.Abs(y1 - y2) );		

				float length = Mathf.Sqrt( ( Mathf.Pow((float)vectorLength.x, 2f) + Mathf.Pow(vectorLength.y, 2f) ) );
				float angle = Mathf.Atan2(y2 - y1, x2 - x1) * Mathf.Rad2Deg;

				boxColliderObj.transform.position = (new Vector2(x,y)-perp).ToVector3(drawSettings.axis, zPos_collider);
				
				boxColliderObj.transform.localScale = new Vector2(length, drawSettings.boxColliderSize).ToVector3(drawSettings.axis, drawSettings.colDepth);
				
				Vector3 rota;
				switch(drawSettings.axis)	
				{
					case Axis.Up:
						rota = new Vector3(0f, -angle, 0f);
						break;
					case Axis.Right:
						rota = new Vector3(-angle, 0f, 0f);
						break;
					default:
						rota = new Vector3(0f, 0f, angle);
						break;
				}
				boxColliderObj.transform.rotation = Quaternion.Euler( rota );
				boxColliderObj.transform.position += transform.position;
				boxColliderObj.transform.parent = transform;

				// someday, we should move the collider to the edges.  to do so, they would need to be re-positioned
				// to account for the new center
				// boxColliderObj.transform.position += ((drawSettings.boxColliderSize/2f) * new Vector3( -(x2-x1), (y2-y1), 0f ).normalized );
			}
		}
	#endregion

	#region Rigidbody

		private bool t_isTrigger;
		private bool t_isConvex;
		private Vector3 t_velocity;
		private Vector3 t_angularVelocity;
		private float t_drag;
		private float t_angularDrag;
		private float t_mass;
		private bool t_useGravity;
		private bool t_isKinematic;
		private bool t_freezeRotation;
		private RigidbodyConstraints t_constraints;
		private CollisionDetectionMode t_collisionDetectionMod;
		private Vector3 t_centerOfMass;
		private Quaternion t_inertiaTensorRotation;
		private Vector3 t_inertiaTensor;
		private bool t_detectCollisions;
		private bool t_useConeFriction;
		private Vector3 t_position;
		private Quaternion t_rotation;
		private RigidbodyInterpolation t_interpolation;
		private int t_solverIterationCount;
		#if UNITY_5
		private float t_sleepThreshold;
		#else
		private float t_sleepVelocity;
		private float t_sleepAngularVelocity;
		#endif
		private float t_maxAngularVelocity;

		private void CopyRigidbodySettings()
		{
			Rigidbody oldRb = gameObject.GetComponent<Rigidbody>();
			if(oldRb == null) return;

			t_velocity				= oldRb.velocity;
			t_angularVelocity		= oldRb.angularVelocity;
			t_drag					= oldRb.drag;
			t_angularDrag			= oldRb.angularDrag;
			t_mass					= oldRb.mass;
			t_useGravity			= oldRb.useGravity;
			t_isKinematic			= oldRb.isKinematic;
			t_freezeRotation		= oldRb.freezeRotation;
			t_constraints			= oldRb.constraints;
			t_collisionDetectionMod	= oldRb.collisionDetectionMode;
			t_centerOfMass			= oldRb.centerOfMass;
			t_inertiaTensorRotation	= oldRb.inertiaTensorRotation;
			t_inertiaTensor			= oldRb.inertiaTensor;
			t_detectCollisions		= oldRb.detectCollisions;
			t_useConeFriction		= oldRb.useConeFriction;
			t_position				= oldRb.position;
			t_rotation				= oldRb.rotation;
			t_interpolation			= oldRb.interpolation;
			t_solverIterationCount	= oldRb.solverIterationCount;
			#if UNITY_5
			t_sleepThreshold		= oldRb.sleepThreshold;
			#else
			t_sleepVelocity			= oldRb.sleepVelocity;
			t_sleepAngularVelocity	= oldRb.sleepAngularVelocity;
			#endif
			t_maxAngularVelocity	= oldRb.maxAngularVelocity;
		}

		private void PasteRigidbodySettings()
		{
			Rigidbody newRb = gameObject.GetComponent<Rigidbody>();
			if(!newRb) return;

			newRb.velocity				= t_velocity;
			newRb.angularVelocity		= t_angularVelocity;
			newRb.drag					= t_drag;
			newRb.angularDrag			= t_angularDrag;
			newRb.mass					= t_mass;
			newRb.useGravity			= t_useGravity;
			newRb.isKinematic			= t_isKinematic;
			newRb.freezeRotation		= t_freezeRotation;
			newRb.constraints			= t_constraints;
			newRb.collisionDetectionMode= t_collisionDetectionMod;
			newRb.centerOfMass			= t_centerOfMass;
			newRb.inertiaTensorRotation	= t_inertiaTensorRotation;
			newRb.inertiaTensor			= t_inertiaTensor;
			newRb.detectCollisions		= t_detectCollisions;
			newRb.useConeFriction		= t_useConeFriction;
			newRb.position				= t_position;
			newRb.rotation				= t_rotation;
			newRb.interpolation			= t_interpolation;
			newRb.solverIterationCount	= t_solverIterationCount;
			#if UNITY_5
			newRb.sleepThreshold		= t_sleepThreshold;
			#else
			newRb.sleepVelocity			= t_sleepVelocity;
			newRb.sleepAngularVelocity	= t_sleepAngularVelocity;
			#endif
			newRb.maxAngularVelocity	= t_maxAngularVelocity;
		}
	#endregion

	#region Debug

		void OnDrawGizmosSelected()
		{
			if(!drawSettings.drawNormals) return;

			Mesh msh = GetComponent<MeshFilter>().sharedMesh;

			Vector3[] vec = msh.vertices;
			Vector3[] nrm = msh.normals;

			Color col = new Color(0f, 0f, 0f, 1f);

			for(int i = 0; i < vec.Length; i++)
			{
				col.r = Mathf.Abs(nrm[i].x);
				col.g = Mathf.Abs(nrm[i].y);
				col.b = Mathf.Abs(nrm[i].z);

				Gizmos.color = col;

				Gizmos.DrawLine(transform.TransformPoint(vec[i]), transform.TransformPoint(vec[i]) + transform.TransformDirection(nrm[i]) * drawSettings.normalLength);	
			}
		}
	#endregion
	}
}