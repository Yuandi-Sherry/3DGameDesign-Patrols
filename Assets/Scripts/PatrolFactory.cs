using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFactory : MonoBehaviour {

	private List<GameObject> products = new List<GameObject> ();



	public List<GameObject> getPatrols() {
		// Debug.Log("call get patrols in factory");
		for(int i = 0; i < 6; i++) {
			GameObject newPatrol = Instantiate(Resources.Load<GameObject>("Prefabs/Patrol"), new Vector3(0,0,0),  Quaternion.identity) as GameObject;
			if(i == 2) {
				newPatrol.transform.position = new Vector3(-30 + 20*(i%3) + 10 + Random.Range(-3, 0), 0, -20 +  + 20*(i/3) + 10 + Random.Range(0, 3));
			}
			else
				newPatrol.transform.position = new Vector3(-30 + 20*(i%3) + 10 + Random.Range(-3, 3), 0, -20 +  + 20*(i/3) + 10 + Random.Range(-3, 3));
			
			newPatrol.AddComponent<PatrolData>();
			newPatrol.GetComponent<PatrolData>().startPosition = newPatrol.transform.position;
			newPatrol.GetComponent<PatrolData>().rangeX = -30 + 20*(i%3);
			newPatrol.GetComponent<PatrolData>().rangeZ = -20 + 20*(i/3);
			newPatrol.GetComponent<PatrolData>().PatSec = i+1;

			// Debug.Log("PATROL" + i + " " + newPatrol.GetComponent<PatrolData>().rangeX + " " + newPatrol.GetComponent<PatrolData>().rangeZ);
			//if(i == 2)
			products.Add(newPatrol);


		}
		return products;
	}


	
}
