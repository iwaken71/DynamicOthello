using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject[,] Stones = new GameObject[8,8]; //石のオブジェクトを64個準備する
	public Transform BasePoint;
	public GameObject CursorManager;
	AudioSource oto;
	const int N = 8; //オセロの辺の長さ
	int[,] board = new int[8,8]; //boardの情報 黒が1 白が2 何もないと0
	private int blackputcount,whiteputcount; //黒が置ける数と白が置ける数
	//private AI ai; //AIのスクリプト
	GameObject stone;
	private AI ai;
	float timer;
	public GameObject BGMManager;
	BGMmanager bgmM;

	
	
	void Awake(){
//		ai = AIManeger.GetComponent<AI> ();
		stone = Resources.Load("Stone") as GameObject;
		ai = CursorManager.GetComponent<AI> ();
		timer = 0;
		oto = GetComponent<AudioSource> ();
		bgmM = BGMManager.GetComponent<BGMmanager> ();
	}
	
	void Start () {
		for (int i = 0; i < 64; i++) {
			Vector3 position = BasePoint.position + new Vector3(i/8,0,i%8);
			GameObject tr = Instantiate(Resources.Load("Trigger"),position,Quaternion.identity) as GameObject;
			tr.GetComponent<TriggerScript>().SetPoint(i/8,i%8);
		}
		GameStart ();
		Count ();
	}
	
	
	void Update () {
		if (ScoreScript.TURNCOLOR != PlayerController.mycolor) {
			if(ScoreScript.GAME){
				timer += Time.deltaTime;
		
				if (timer > 2.0f) {
					int point = ai.RandomAnswer (GetBoard (), ScoreScript.TURNCOLOR);
					Putstone (ScoreScript.TURNCOLOR, point / 8, point % 8);
					oto.Play ();
					timer = 0;
				}
			}
		}
		
	}
	
	//ゲームの初期化
	void GameStart(){
		for(int i = 0; i < N; i++){
			for(int j = 0; j < N; j++){
				board[i,j] = 0;
			}
		}
		
		
		
		board [3, 3] = 1;
		board [4, 4] = 1;
		
		RealPut (3, 3, 1);
		RealPut (4, 4, 1);
		
		board [3, 4] = 2;
		board [4, 3] = 2;
		
		RealPut (3, 4, 2);
		RealPut (4, 3, 2);
		
		ScoreScript.TURNCOLOR = 1;
		ScoreScript.TURNCOUNT = 1;
	}
	// 石を置いた時のメソッド
	public void Putstone(int color,int x,int y){
		// colorは黒か白、位置は0から7の間
		if ((color == 1 || color == 2) && x >= 0 && x < 8 && y >= 0 && y < 8) {
			//color1=自分の石 color2=相手の石 
			int color1 = color;
			int color2;
			if (color1 == 1)
				color2 = 2;
			else
				color2 = 1;
			
			//その場所に石を置けるかどうか
			if (canPut (color, x, y)) {
				board [x, y] = color1;
				RealPut (x,y,color1);
				ScoreScript.TURNCOUNT++;
				ScoreScript.TURNCOLOR = color2;
				CursorManager.GetComponent<CursorScript>().LastPut(x,y);
				for (int j = y-1; j <= y+1; j++) {
					for (int i = x-1; i <= x+1; i++) {
						if (i >= 0 && i < 8 && j >= 0 && j < 8 && !(x == i && y == j)) {
							if (board [i, j] == color2) {
								reverse (color, i, j, i - x, j - y);
							}
						}
					}
				}
			}
		}
		Count ();
	}
	private void RealPut(int x,int y,int color){
		Vector3 position = BasePoint.position + new Vector3 (x,0.05f,y);
		Stones[x,y] = GameObject.Instantiate(stone,position,Quaternion.Euler(0,0,0))as GameObject;
		StoneController script = Stones[x,y].GetComponent<StoneController>();
		script.SetColor(color);
	}


	private void reverse(int color,int x,int y,int vx,int vy){
		bool ans = false;
		int color1 = color;
		int color2;
		if (color1 == 1)
			color2 = 2;
		else
			color2 = 1;
		int nowx = x;
		int nowy = y;
		int[,] boardcopy = new int[8,8];
		for(int j = 0; j < N; j++){
			for(int i = 0; i < N; i++){
				boardcopy[i,j] = board[i,j];
			}
		}
		while (true) {
			board[nowx,nowy] = color;
			nowx += vx;
			nowy += vy;
			if(!(nowx >= 0 && nowx < 8 && nowy >= 0 && nowy < 8)){
				ans = false;
				break;
			}else if(board[nowx,nowy] == color){
				ans = true;
				break;
			}else if(board[nowx,nowy] == 0){
				ans = false;
				break;
			}
		}
		if (!ans) {
			for(int j = 0; j < N; j++){
				for(int i = 0; i < N; i++){
					board[i,j] = boardcopy[i,j];
				}
			}
		}
		for(int j = 0; j < N; j++){
			for(int i = 0; i < N; i++){
				if(board[i,j] + boardcopy[i,j]==3){
					Stones[i,j].GetComponent<StoneController>().Reverse();
				}
			}
		}
		
	}
	
	public bool canPut(int color,int x,int y){
		// もし置いてたらfalse
		if (board [x, y] != 0) {
			return false;
		}
		bool ans = false;
		int color1 = color;
		int color2;
		if (color1 == 1)
			color2 = 2;
		else
			color2 = 1;

		// 
		for (int j = y-1; j <= y+1; j++) {
			for (int i = x-1; i <= x+1; i++) {
				if(i >= 0 && i < 8 && j >= 0 && j < 8 && !(x == i && y == j)){
					if(board[i,j] == color2){
						if(Trace (color,i,j,i-x,j-y)){
							return true;
						}
					}
				}
			}
		}
		return false;
	}
	private bool Trace(int color,int x,int y,int vx,int vy){
		int nowx = x;
		int nowy = y;
		nowx += vx;
		nowy += vy;
		if (!(nowx >= 0 && nowx < 8 && nowy >= 0 && nowy < 8)) {
			return false;
		} else if (board [nowx, nowy] == 0) {
			return false;
		} else if (board [nowx, nowy] == color) {
			return true;
		} else {
			return Trace (color,nowx,nowy,vx,vy);
		}
	}
	private void Count(){
		int b_put = 0;
		int w_put = 0;
		int b_count = 0;
		int w_count = 0;
		for (int j = 0; j < N; j++) {
			for (int i = 0; i < N; i++) {
				if(board[i,j] == 0){
					if(this.canPut(1,i,j)){
						b_put++;
					}if(this.canPut(2,i,j)){
						w_put++;
					}
				}
				if(board[i,j] == 1)
					b_count++;
				else if(board[i,j] == 2)
					w_count++;
			}
		}
		blackputcount = b_put;
		whiteputcount = w_put;
		ScoreScript.BLACKCOUNT = b_count;
		ScoreScript.WHITECOUNT = w_count;
		
		if (b_put == 0 && w_put == 0) {
			int mycolor = PlayerController.mycolor;
			ScoreScript.GAME = false;
			if(b_count>=w_count){
				if(mycolor == 1){
					bgmM.WinBGMPlay();
				}else{
					bgmM.LoseBGMPlay();
				}
			}else{
				if(mycolor == 1){
					bgmM.LoseBGMPlay();
				}else{
					bgmM.WinBGMPlay();
				}

			}
		} else if (b_put == 0) {
			ScoreScript.TURNCOLOR = 2;
		} else if (w_put == 0) {
			ScoreScript.TURNCOLOR = 1;
		}
	}
	public void Pass(){
		this.Count ();
		if (ScoreScript.TURNCOLOR == 1 && blackputcount == 0) {
			ScoreScript.TURNCOLOR = 2;
		} else if(ScoreScript.TURNCOLOR == 2 && whiteputcount == 0){
			ScoreScript.TURNCOLOR = 1;
		}
	}
	public void ReStart(){
		GameStart ();
	}

	public int[,] GetBoard(){
		int[,] b = new int[N,N]; 
		for (int i = 0; i < N; i++) {
			for (int j = 0; j < N; j++) {
				b[i,j] = board[i,j];
			}
		}
		return b;
	}

	
	
	
}
