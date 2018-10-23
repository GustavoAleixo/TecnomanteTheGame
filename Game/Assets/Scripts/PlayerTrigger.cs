using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour {
	private PlayerBehaviourScript playerScript;
	// Use this for initialization
	void Start () {
		playerScript = GameObject.Find ("Player2").GetComponent<PlayerBehaviourScript> ();
	}
	
	void OnTriggerEnter2D (Collider2D other){
		if (other.CompareTag ("Enemy")) {
			playerScript.DamagePlayer();
		}
	}
}
