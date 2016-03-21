using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour {
	public GameObject Cursor,Cursor2;
	public GameObject board;
	public Transform point;
	GameManager GM;

	// Use this for initialization
	void Start () {
		GM = board.GetComponent<GameManager> ();
		//Cursor = Resources.Load ("Cursor") as GameObject;
	
	}
	void Update () {

	}

	public void Check(int x,int y){
		if (0 <= x && x < 8 && 0 <= y && y < 8) {
			Cursor.transform.position = point.position + new Vector3 (x, 0, y);
		} else {
			Cursor.transform.position =  new Vector3 (0, 10000, 0);
		}
	}

	public void LastPut(int x,int y){
		if (ScoreScript.TURNCOUNT > 1) {
			Cursor2.transform.position = point.position + new Vector3 (x, 0, y);
		} else {
			Cursor2.transform.position =  new Vector3 (0, 10000, 0);
		}
	}
}
