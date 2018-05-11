using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {

	public CCActionManager actionManager;
	public GameObject player;
	private List<GameObject> patrols = new List<GameObject>();
	public PatrolFactory patrolFactory;
	private UserGUI userGUI;
	public int playerSpeed = 1000000;
	public int curAreaSign;
	public Timer timer;
	private int gameJudge; // 0-> ingame, 1-> win, -1->lose
	// public GameObject collider1;
	// Use this for initialization
	void Awake () {
		//Debug.Log("FPS " + Application.targetFrameRate);
		Application.targetFrameRate = 30;
		SSDirector director =  SSDirector.GetInstance();
		director.currentSceneController = this;
		patrolFactory = Singleton<PatrolFactory>.Instance;
		actionManager = gameObject.AddComponent<CCActionManager>() as CCActionManager;
		userGUI =  gameObject.AddComponent<UserGUI>() as UserGUI;
		timer = gameObject.AddComponent<Timer>() as Timer;
		LoadResources();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("xx  " + player.transform.rotation);
		
		if(player.transform.position.y != 0) {
			player.transform.position = new Vector3(player.transform.position.x, 0,player.transform.position.z);
		}

		if(player.transform.rotation.x != 0 || player.transform.rotation.z != 0) {
			//Debug.Log((int)(player.transform.rotation.y*10));
			switch((int)(player.transform.rotation.y*10)) {
				case 0:
					player.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
					break;
				case 7:
					player.transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
				break;
				case 9:
					player.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
				break;
				case -7:
					player.transform.rotation = Quaternion.Euler(new Vector3(0,-90,0));
				break;
			}
		}

		if(gameJudge != 0) {
			for(int i = 0; i < patrols.Count; i++) {
				if(patrols[i].transform.position.y != 0) {
					patrols[i].transform.position = new Vector3(patrols[i].transform.position.x, 0,patrols[i].transform.position.z);
				}

				//Debug.Log("防止倒下") ;
				if(patrols[i].transform.rotation.x != 0 || patrols[i].transform.rotation.z != 0) {
					switch((int)(patrols[i].transform.rotation.y*10)) {
						case 0:
							patrols[i].transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
							break;
						case 7:
							patrols[i].transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
						break;
						case 9:
							patrols[i].transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
						break;
						case -7:
							patrols[i].transform.rotation = Quaternion.Euler(new Vector3(0,-90,0));
						break;
					}
					patrols[i].transform.rotation = Quaternion.Euler(0, patrols[i].transform.rotation.y,0);
				}
			}
		}
	}

	public void LoadResources() {
		player = Instantiate(Resources.Load("Prefabs/Player"), new Vector3(25, 0, -15),  Quaternion.identity) as GameObject;
		patrols = patrolFactory.getPatrols();
		//player.SetActive(false);

		for(int i = 0; i < patrols.Count; i++) {
			actionManager.patrolling(patrols[i]);
		}
	}

	public void movePlayer (float inputX, float inputZ) {
		//Debug.Log(player.transform.position + 100 * new Vector3(inputX, 0, inputZ));
		player.transform.LookAt(player.transform.position + 10000 * new Vector3(inputX, 0, inputZ));
		// player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

		// 如果玩家让player移动，则player的动画为奔跑状态
		if(inputX != 0 || inputZ != 0) {
			//Debug.Log("inputtX" + inputX +"inputZ" + inputZ);
			player.GetComponent<Animator>().SetBool("run", true);
			Vector3 movement = new Vector3(inputX, 0, inputZ);
			player.GetComponent<Rigidbody>().velocity = new Vector3(inputX*playerSpeed, 0, inputZ*playerSpeed) ;
			//player.GetComponent<Rigidbody>().velocity.z = inputZ*playerSpeed;
			//player.GetComponent<Rigidbody>().AddForce(movement*playerSpeed);
			
		}
		/*else if (inputX == 0 || inputZ == 0) {
			player.GetComponent<Animator>().SetBool("run", false);
			//player.GetComponent<Rigidbody>().AddForce(Vector3.zero);
			 = Vector3.zero;
		}
		*/

		//player.GetComponent<Rigidbody>().AddForce(Vector3.zero);
		// 移动玩家
		// Debug.Log("移动玩家");
		
		
		//player.transform.position += new Vector3(inputX * playerSpeed * Time.deltaTime, 0, inputZ * playerSpeed * Time.deltaTime);		
        // player.transform.rotation = Quaternion.Euler(0, 180, 0);

        /**/
	}

	
	void OnEnable() {
		GameEventManager.gameoverChange += gameOverEvent;
		GameEventManager.win += win;
	}


	void gameOverEvent() {
		gameJudge = -1;
		actionManager.DestroyAll();
		for(int i = 0; i < patrols.Count; i++) {
			if(patrols[i].transform.position.y != 0) {
				patrols[i].transform.position = new Vector3(patrols[i].transform.position.x, 0,patrols[i].transform.position.z);
			}
			patrols[i].GetComponent<Animator>().SetBool("run",false);
		}
		timer.End();
		player.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}


	public int getGameState() {
		return gameJudge;
	}

	public void win() {
		gameJudge = 1;
		actionManager.DestroyAll();
		for(int i = 0; i < patrols.Count; i++) {
			if(patrols[i].transform.position.y != 0) {
				patrols[i].transform.position = new Vector3(patrols[i].transform.position.x, 0,patrols[i].transform.position.z);
			}
			patrols[i].GetComponent<Animator>().SetBool("run",false);
		}
		timer.End();
		player.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	public int getTime() {
		return (int)timer.Duration ;
	}

}
