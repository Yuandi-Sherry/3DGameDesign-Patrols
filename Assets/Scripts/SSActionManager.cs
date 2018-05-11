using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SSActionManager: MonoBehaviour, ISSActionCallback {
	protected Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
	protected List<SSAction> waitingAdd = new List<SSAction>();
	protected List<int> waitingDelete = new List<int>();
	public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager) {
		action.gameobject = gameobject;
		action.callback = manager;
		waitingAdd.Add(action);
		action.Start();
	}

	protected virtual void Start() { }
	public void SSActionEvent(SSAction source,  
		int intParam,  
		GameObject objectParam) {
		if(intParam == 0) {
			PatrolFollowAction follow = PatrolFollowAction.getSSAction(objectParam);
			this.RunAction(objectParam, follow, this);
		}
		else {
			PatrolAction move = PatrolAction.getSSAction(objectParam.transform.position);
			this.RunAction(objectParam, move, this);
		}
	}	

	protected void Update () {
		
		foreach (SSAction ac in waitingAdd) 
			actions[ac.GetInstanceID()] = ac;
		waitingAdd.Clear();

		foreach(KeyValuePair <int, SSAction> kv in actions) {
			SSAction ac = kv.Value;
			if(ac.destroy) {
				waitingDelete.Add(ac.GetInstanceID());
			}
			else if (ac.enable) {
				ac.Update();
			}
		}
		foreach(int key in waitingDelete) {
			SSAction ac = actions[key];
			actions.Remove(key);
			DestroyObject(ac);
		}
		waitingDelete.Clear();
	}

	public void DestroyAll() {
		foreach(KeyValuePair<int, SSAction> kv in actions) {
			SSAction ac = kv.Value;
			ac.destroy = true;
		}
	}


}