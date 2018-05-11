using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : SSAction {

	private enum Direction {backward, right, forward, left};
	private float posX, posZ;
	private float stepLength;
	public float speed = 5;
	private bool regular = true; // ??
	private int rotatePeriod = 100;
	private int countFrame = 0;
	private Direction direction = Direction.forward;
	private PatrolData data;
	private FirstController sceneController;
	private bool handle;
	//private Vector3 towards = new Vector3(0,0,0);

	public static PatrolAction getSSAction(Vector3 curLoc) {
		PatrolAction action = ScriptableObject.CreateInstance<PatrolAction>();
		action.posX = curLoc.x;
		action.posZ = curLoc.z;
		action.stepLength = Random.Range(14,20);		
		return action;
	}

	public override void Start()
    {
        this.gameobject.GetComponent<Animator>().SetBool("run", true);
        data = this.gameobject.GetComponent<PatrolData>();
        sceneController = SSDirector.GetInstance().currentSceneController as FirstController;
        enable = true;
        handle = false;
    }

	// Update is called once per frame
	public override void Update () {
		Debug.Log("update in patrol ac");
		// 防止飞起
		if(this.gameobject.transform.position.y != 0) {
			this.gameobject.transform.position = new Vector3(this.gameobject.transform.position.x, 0,this.gameobject.transform.position.z);
		}

		// 防止倒下
		if(this.gameobject.transform.rotation.x != 0 || this.gameobject.transform.rotation.z != 0) {
			this.gameobject.transform.rotation = Quaternion.Euler(0, this.gameobject.transform.rotation.y,0);
		}
		patrolling();
		/* 
		 * 如果当前巡逻兵正在追踪玩家且和被追踪玩家在同一区域
		 */
		// Debug.Log("this.gameobject.transform.position" + Vector3.Distance( sceneController.player.transform.position, this.gameobject.transform.position));
		if(sceneController.curAreaSign == this.gameobject.GetComponent<PatrolData>().PatSec && 
			Vector3.Distance( sceneController.player.transform.position, this.gameobject.transform.position) < 7) {
			//Debug.Log("same sec" + sceneController.curAreaSign);
			this.destroy = true;
			// this.enable = false;
			// data.player = sceneController.player;
			this.callback.SSActionEvent(this,0,this.gameobject);

		}
	}
	
	private void patrolling() {
		if(!handle) {
			if(regular) {
				Debug.Log("转");
				switch(direction) {

					case Direction.forward:
						gameobject.transform.rotation = Quaternion.Euler(0, 0, 0);
						posZ -= stepLength;
						break;
					case Direction.backward:
						posZ += stepLength;
						gameobject.transform.rotation = Quaternion.Euler(0, 180,0);
						break;
					case Direction.left:
						posX -= stepLength;
						gameobject.transform.rotation = Quaternion.Euler(0, -90,0);
						break;
					case Direction.right:
						posX += stepLength;
						gameobject.transform.rotation = Quaternion.Euler(0, 90,0);
						break;
				}
				regular = false;
				
			}
			
			gameobject.transform.LookAt(new Vector3(posX, 0, posZ));
			// 如果巡逻兵走远了，就以一个随机步长向巡逻中心靠拢
			if (Vector3.Distance(gameobject.transform.position, new Vector3 (posX, 0, posZ)) > 1) {
				Debug.Log("走远");
				gameobject.transform.position = Vector3.MoveTowards(gameobject.transform.position, new Vector3(posX,0,posZ), Time.deltaTime*speed);	
			}
			else {
				Debug.Log("拐弯");
				direction = direction + 1;
				if(direction > Direction.left)
					direction = Direction.backward;
				regular = true;
			}
		}
		//Debug.Log("CountFrame " + countFrame);
		// int rangeX = this.gameobject.GetComponent<PatrolData>().rangeX;
		// int rangeZ = this.gameobject.GetComponent<PatrolData>().rangeZ;
		

		//}
		
		/*if(countFrame >= rotatePeriod) {
			countFrame = 0;
			//Debug.Log("CountFrame >  " + countFrame);
			double rand = Random.Range(0.0f, 4.0f);
			if(rand < 1.0f)
				direction = Direction.forward;
			else if (rand < 2.0f)
				direction = Direction.backward;
			else if (rand < 3.0f)
				direction = Direction.left;
			else
				direction = Direction.right; 

			
			switch(direction) {
				case Direction.forward:
					gameobject.transform.rotation = Quaternion.Euler(0, 0,0);
					towards.x = 0;
					towards.z = 1;
					break;
				case Direction.backward:
					towards.x = 0;
					towards.z = -1;
					gameobject.transform.rotation = Quaternion.Euler(0, 180,0);
					break;
				case Direction.left:
					towards.x = -1;
					towards.z = 0;
					gameobject.transform.rotation = Quaternion.Euler(0, -90,0);
					break;
				case Direction.right:
					towards.x = 1;
					towards.z = 0;
					gameobject.transform.rotation = Quaternion.Euler(0, 90,0);
					break;
			}
			
		}
		countFrame++;*/


		// 防止旋转


		/*if(this.gameobject.transform.position.x < this.gameobject.GetComponent<PatrolData>().rangeX + 1) {
			gameobject.transform.position = new Vector3(this.gameobject.GetComponent<PatrolData>().rangeX + 1, 0, gameobject.transform.position.z);
		}

		if(this.gameobject.transform.position.z < this.gameobject.GetComponent<PatrolData>().rangeZ + 1) {
			gameobject.transform.position = new Vector3(gameobject.transform.position.x, 0, this.gameobject.GetComponent<PatrolData>().rangeZ + 1);
		}
		if( this.gameobject.transform.position.x > this.gameobject.GetComponent<PatrolData>().rangeX + 19) {
			gameobject.transform.position = new Vector3(this.gameobject.GetComponent<PatrolData>().rangeX + 19, 0, gameobject.transform.position.z);
		}
		if (this.gameobject.transform.position.z > this.gameobject.GetComponent<PatrolData>().rangeZ + 19) {
			gameobject.transform.position = new Vector3(gameobject.transform.position.x, 0, this.gameobject.GetComponent<PatrolData>().rangeZ + 19);
		}

		
		if(regular) {

			
			//regular = false;
		}*/

		// 绕行巡逻
		
		/*if(countFrame >= rotatePeriod) {
			countFrame = 0;
			//Debug.Log("CountFrame >  " + countFrame);
			double rand = Random.Range(0.0f, 4.0f);
			if(rand < 1.0f)
				direction = Direction.forward;
			else if (rand < 2.0f)
				direction = Direction.backward;
			else if (rand < 3.0f)
				direction = Direction.left;
			else
				direction = Direction.right; 
		}
		else {

		}*/

		
	}

	void OnEnable() {
		GameEventManager.hitWall += handleHitWall;
	}

	void handleHitWall(GameObject a) {
		//Debug.Log("掉1头");
		//handle = true;
		if(a == gameobject) {
			/*Direction temp = direction;
			double rand = Random.Range(0.0f, 4.0f);
			if(rand < 1.0f)
				direction = Direction.forward;
			else if (rand < 2.0f)
				direction = Direction.backward;
			else if (rand < 3.0f)
				direction = Direction.left;
			else
				direction = Direction.right; */
		    //if(direction == temp ) {
				//stepLength = Random.Range(3, 10);
				switch(direction) {
					case Direction.backward:
						direction = Direction.forward;
						break;
					case Direction.forward:
						direction = Direction.backward;
						break;
					case Direction.left:
						direction = Direction.right;
						break;
					case Direction.right:
						direction = Direction.left;
						break;
				}
			//}

				//Debug.Log("掉头");
				/**/


			regular = true;
		}
		//handle = false;

	}
}

