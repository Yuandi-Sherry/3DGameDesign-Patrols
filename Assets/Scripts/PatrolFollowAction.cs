using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFollowAction : SSAction {
	public float speed = 3f;
	private GameObject player;
	private int stepLength;
	private PatrolData data;
	private FirstController sceneController;

	public static PatrolFollowAction getSSAction(GameObject player) {
		// Debug.Log("getSSAction in PatrolFollowAction");
		PatrolFollowAction action = ScriptableObject.CreateInstance<PatrolFollowAction>();
		action.player = player;
		action.stepLength  = Random.Range(3,5);
		return action;
	}

	public override void Update() {
		Debug.Log("update in patrol follow");
		// 防止飞起
		if(this.gameobject.transform.position.y != 0) {
			this.gameobject.transform.position = new Vector3(this.gameobject.transform.position.x, 0,this.gameobject.transform.position.z);
		}

		// 防止倒下
		if(this.gameobject.transform.rotation.x != 0 || this.gameobject.transform.rotation.z != 0) {
			this.gameobject.transform.rotation = Quaternion.Euler(0, this.gameobject.transform.rotation.y,0);
		}

		follow();

		if(sceneController.curAreaSign != data.PatSec || Vector3.Distance(
			this.gameobject.transform.position, sceneController.player.transform.position) >= 7) {
			this.destroy = true;
			this.callback.SSActionEvent(this, 1, this.gameobject);
		}

	}
	public override void Start() {
        // this.gameobject.GetComponent<Animator>().SetBool("run", true);
        data = this.gameobject.GetComponent<PatrolData>();
        sceneController = SSDirector.GetInstance().currentSceneController as FirstController;
        enable = true;

	}

	private void follow() {
		Debug.Log("follow");
		this.gameobject.transform.LookAt(sceneController.player.transform.position);
		this.gameobject.transform.position = Vector3.MoveTowards(gameobject.transform.position, sceneController.player.transform.position, Time.deltaTime*speed);
	}
}
