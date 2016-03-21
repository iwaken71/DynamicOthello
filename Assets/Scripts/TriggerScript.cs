using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {
	
	private int x,y;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetPoint(int x1,int y1){
		this.x = x1;
		this.y = y1;
	}
	public int GetX(){
		return this.x;
	}
	public int GetY(){
		return this.y;
	}
	
}
