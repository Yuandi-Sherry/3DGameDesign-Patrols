using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

	IUserAction userAction;

	// Use this for initialization
	GUIStyle labelStyle;
	GUIStyle labelStyle2;
	void Start () {
//		Debug.Log("add UserGUI");
		userAction = SSDirector.GetInstance().currentSceneController as IUserAction;
		labelStyle = new GUIStyle("label");
		labelStyle.alignment = TextAnchor.MiddleCenter;
		labelStyle.fontSize = Screen.height/15;
		labelStyle.normal.textColor = Color.black;
		labelStyle2 = new GUIStyle("label");
		labelStyle2.alignment = TextAnchor.MiddleCenter;
		labelStyle2.fontSize = Screen.height/20;
		labelStyle2.normal.textColor = Color.black;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(userAction.getGameState() == 0) {
			//Debug.Log("add UserGUI");
			float inputX = Input.GetAxis("Horizontal");
	    	float inputZ = Input.GetAxis("Vertical");
	    	//移动玩家
	    	userAction.movePlayer(inputX, inputZ);
		}
		
	}

	void OnGUI() {
		if(userAction.getGameState() == 1) {
			GUI.Label(new Rect(Screen.width/2 - Screen.width/12, Screen.height/5 + Screen.height/16, Screen.width/2, Screen.height/2), "YOU WIN! ", labelStyle);
			GUI.Label(new Rect(Screen.width/2 - Screen.width/12, Screen.height/5 - Screen.height/12, Screen.width/6, Screen.height/6), "Time Used: " + userAction.getTime() + " s", labelStyle);
			//Timer.

		}

		else if(userAction.getGameState() == -1) {
			GUI.Label(new Rect(Screen.width/2 - Screen.width/4, Screen.height/5 + Screen.height/4, Screen.width/2, Screen.height/2), "You're caught by patrols.", labelStyle);
			GUI.Label(new Rect(Screen.width/2 - Screen.width/4, Screen.height/5 - Screen.height/4, Screen.width/2, Screen.height/2), "Time Used: " + userAction.getTime(), labelStyle);
		}

		else {
			GUI.Label(new Rect(Screen.width/10,  Screen.height/10, Screen.width/6, Screen.height/6), "Time Used: " + userAction.getTime()  + " s", labelStyle2);
		}
	}
}
