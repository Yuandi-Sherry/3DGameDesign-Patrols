using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController
{
    //加载场景资源
    void LoadResources();
}

public enum SSActionEventType:int { Started, Competeted }  

public interface ISSActionCallback {
	void SSActionEvent(SSAction source,
		int intParam = 0,   
		GameObject objectParam = null);
}

public interface IUserAction {
	void movePlayer(float inputX, float inputZ);
	int getGameState();
	int getTime();
}