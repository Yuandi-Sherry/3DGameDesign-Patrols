using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour {
	public delegate void GameOverEvent();
	public static event GameOverEvent gameoverChange;

	public delegate void winEvent();
	public static event winEvent win;

	public delegate void HitWall(GameObject a);
	public static event HitWall hitWall;

	public void gameOver() {
		if(gameoverChange != null) {
			gameoverChange();
		}
	}

	public void hitWallEvent(GameObject a) {
		if(hitWall != null) {
			hitWall(a);
		}
	}

	public void winning() {
		if(win != null) {
			win();
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
