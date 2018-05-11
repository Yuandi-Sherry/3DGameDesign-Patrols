using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {

	
	void OnCollisionEnter(Collision other) {	
		if(other.gameObject.tag == "Player") {
			Debug.Log("Game over");
			other.gameObject.GetComponent<Animator> ().SetBool("death", true);
			other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.zero);
			other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Singleton<GameEventManager>.Instance.gameOver();
		}
	}

	void OnTriggerEnter(Collider other) {
		//Debug.Log("Grid1");
		if(other.gameObject.tag=="grid") {
			Debug.Log("Grid");
			//other.gameObject.GetComponent<Animator> ().SetBool("death", true);
			//other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.zero);
			//other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			//Singleton<GameEventManager>.Instance.gameOver();
			Singleton<GameEventManager>.Instance.hitWallEvent(this.gameObject);
		}
	}

}
