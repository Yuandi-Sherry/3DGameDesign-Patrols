using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollider : MonoBehaviour {

	public int sign;
	FirstController sceneController;
	void Start () {
		
		sceneController = (FirstController)SSDirector.GetInstance().currentSceneController;
		//Debug.Log(SSDirector.GetInstance().currentSceneController == null);
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider collider) {
		// Debug.Log(sceneController == null);
		if(collider.gameObject.tag == "Player") {
			// Debug.Log("enter this area " + sign);
			sceneController.curAreaSign = sign;
		}
	}
}
