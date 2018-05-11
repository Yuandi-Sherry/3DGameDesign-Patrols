using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager {

	public void patrolling(GameObject patrol) {
		Vector3 patrollCenter = new Vector3(patrol.GetComponent<PatrolData>().rangeX + 10 , 0,patrol.GetComponent<PatrolData>().rangeZ + 10 );
		PatrolAction action = PatrolAction.getSSAction(patrollCenter);
		this.RunAction(patrol, action, this);
	}
	
	public void DestroyAllAction() {
		DestroyAll();
	}


}
