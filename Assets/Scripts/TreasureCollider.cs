using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureCollider : MonoBehaviour {

	void OnCollisionEnter (Collision collider) {
		if(collider.gameObject.tag == "Player") {
			this.GetComponent<Animator>().SetBool("open", true);
			Singleton<GameEventManager>.Instance.winning();
		}
	}

}
