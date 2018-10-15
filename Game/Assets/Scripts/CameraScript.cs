using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	// Use this for initialization

	public Transform pt;
	public Transform ct;
	public Rigidbody2D pb;

	// Update is called once per frame
	void Update () {
		ct.position = Vector3.Lerp (
			ct.position,
			new Vector3 (pt.position.x + 1, pt.position.y + 1, ct.position.z),
			pt.position.x * pt.position.x);
	}
}
