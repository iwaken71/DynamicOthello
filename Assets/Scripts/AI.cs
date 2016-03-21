using UnityEngine;
using System.Collections;
using  System.Collections.Generic;

public class AI : MonoBehaviour {
	const int N = 8;
	public int depth = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	class Undo{
		public int[,] b = new int[8, 8];
		public int turn;
		public int nowcolor;
		public Undo(int [,] board){
			for(int i = 0; i < 8; i++){
				for(int j = 0; j < 8; j++){
					b[i,j] = board[i,j];
				}
			}
			turn = ScoreScript.TURNCOUNT;
			nowcolor = ScoreScript.TURNCOLOR;
		}
	}

	private int BoardValue(int[,] board,int color){
		int [,] val = 
			{{20,-12,0,-1,-1,0,-12,20},
			{-12,-15,-3,-3,-3,-3,-15,-12},
			{0,-3,0,-1,-1,0,-3,0},
			{-1,-3,-1,-1,-1,-1,-3,-1},
			{-1,-3,-1,-1,-1,-1,-3,-1},
			{0,-3,0,-1,-1,0,-3,0},
			{-12,-15,-3,-3,-3,-3,-15,-12},
			{20,-12,0,-1,-1,0,-12,20},};

		int blackval = 0;
		int whiteval = 0;
		int color1 = color;
		int color2;
		if (color == 1) {
			color2 = 2;
		} else {
			color2 = 1;
		}

		int b_put = 0;
		int w_put = 0;
		int b_count = 0;
		int w_count = 0;
		for (int j = 0; j < N; j++) {
			for (int i = 0; i < N; i++) {
				if(board[i,j] == 0){
					if(this.canPut(board,1,i,j)){
						b_put++;
					}if(this.canPut(board,2,i,j)){
						w_put++;
					}
				}
				if(board[i,j] == 1){
					b_count++;
					blackval += val[i,j];
				}else if(board[i,j] == 2){
					w_count++;
					whiteval += val[i,j];
				}
			}
		}
		int ans = 0;
		if (color == 1) {
			ans = blackval - whiteval + b_put - w_put;
		} else {
			ans = whiteval - blackval + w_put - b_put;

		}
		return ans;



		//return CountStone (board,color);
	}

	private int alphaBeta(int[,] board,int color,bool flag,int level,int alpha,int beta){
		int value;
		int childValue;
		int bestX = 0;
		int bestY = 0;
		int alpha1 = alpha;
		int beta1 = beta;
		//int[,] undo = new int[8, 8];

		int color2;
		if(color == 1){
			color2 = 2;
		}else{
			color2 = 1;
		}



		if (level == 0) {
			return this.BoardValue(board,color);
		}
		if (flag) {
			value = -999999;
		} else {
			value = 999999;
		}

		for (int j = 0; j < 8; j++) {
			for(int i = 0; i < 8; i++){

				Undo undo = new Undo(board);
				if(this.canPut(board,color,i,j)){

					this.putstone(board,color,i,j);

					childValue = alphaBeta(board,color2,!flag,level-1,alpha1,beta1);
					if(flag){
						if(childValue > value){
							value = childValue;
							alpha1 = value;
							bestX = i;
							bestY = j;
						}if(value>beta){
							for (int y = 0; y < 8; y++) {
								for(int x = 0; x < 8; x++){
									board[x,y] = undo.b[x,y];

								}
							}

							return value;
						}

					}else{
						if(childValue < value){
							value = childValue;
							beta1 = value;
							bestX = i;
							bestY = j;
						}
						if(value < alpha){
							for (int y = 0; y < 8; y++) {
								for(int x = 0; x < 8; x++){
									board[x,y] = undo.b[x,y];

								}
							}
							return value;
						}
					}
					for (int y = 0; y < 8; y++) {
						for(int x = 0; x < 8; x++){
							board[x,y] = undo.b[x,y];
						}
					}
				}
			}
		}
		if (level == depth) {
			return bestX * 8 + bestY;
		} else {
			return value;
		}
	}



	public int RandomAnswer(int[,] board,int color){
		List<int> Data = new List<int> ();
		List<int> BestData = new List<int> ();
		List<int> BetterData = new List<int> ();
		List<int> BadData = new List<int> ();
		List<int> WorstData = new List<int> ();
		int count = 0;
		for (int j = 0; j < 8; j++) {
			for (int i = 0; i < 8; i++) {
				if (this.canPut (board, color, i, j)) {
					count++;
					if (i == 0 || i == 7) {
						if (j == 0 || j == 7) {
							BestData.Add (i * 8 + j);
						} else if (j == 1 || j == 6) {
							BadData.Add (i * 8 + j);
						} else if (j == 2 || j == 5){
							BetterData.Add (i * 8 + j);
						}else {
							Data.Add (i * 8 + j);
						}
					} else if (i == 1 || i == 6) {
						if (j == 0 || j == 7) {
							BadData.Add (i * 8 + j);
						} else if(j == 1 || j == 6){
							WorstData.Add(i*8+j);
						}else {
							Data.Add (i * 8 + j);
						}
					} else if(i == 2 || i == 5){
						if(j == 0 || j == 7){
							BetterData.Add (i * 8 + j);
						}else{
							Data.Add (i * 8 + j);
						}
					}
					else {
						Data.Add (i * 8 + j);
					}


				}
			}
		}
		if (count == 0) {
			return -1;
		}
		if (BestData.Count > 0) {
			int ran = Random.Range (0, BestData.Count - 1);
			return BestData [ran];
		} else if (BetterData.Count > 0) {
			int ran = Random.Range (0, BetterData.Count - 1);
			return BetterData [ran];
		}else if (Data.Count > 0) {
			int ran = Random.Range (0, Data.Count - 1);
			return Data [ran];
		}else if (BadData.Count > 0) {
			int ran = Random.Range (0, BadData.Count - 1);
			return BadData [ran];
		} else {
			int ran = Random.Range (0, WorstData.Count - 1);
			return WorstData [ran];
		}
	


	}
	public int ABAnswer(int[,] board,int color){
		int a = this.alphaBeta (board,color,true,depth,-99999,99999);
		return a;
		
	}

	private int CountStone(int[,] board,int color){
		int count = 0;
		for (int i = 0; i < 8; i++) {
			for(int j = 0; j < 8; j++){
				if(board[i,j] == color)
					count++;
			}
		}
		return count;
	}

	public bool canPut(int[,] board,int color,int x,int y){
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
		
		for (int j = y-1; j <= y+1; j++) {
			for (int i = x-1; i <= x+1; i++) {
				if(i >= 0 && i < 8 && j >= 0 && j < 8 && !(x == i && y == j)){
					if(board[i,j] == color2){
						if(trace (board,color,i,j,i-x,j-y)){
							return true;
						}
					}
				}
			}
		}
		return false;
	}
	private bool trace(int[,] board,int color,int x,int y,int vx,int vy){
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
			return trace (board,color,nowx,nowy,vx,vy);
		}
	}
	public void putstone(int[,] board,int color,int x,int y){
		if ((color == 1 || color == 2) && x >= 0 && x < 8 && y >= 0 && y < 8) {
			int color1 = color;
			int color2;
			if (color1 == 1)
				color2 = 2;
			else
				color2 = 1;
			
			if (canPut (board,color, x, y)) {
				board [x, y] = color;
				for (int j = y-1; j <= y+1; j++) {
					for (int i = x-1; i <= x+1; i++) {
						if (i >= 0 && i < 8 && j >= 0 && j < 8 && !(x == i && y == j)) {
							if (board [i, j] == color2) {
								reverse (board,color, i, j, i - x, j - y);
							}
						}
					}
				}
			}
		}
	}
	private void reverse(int[,] board,int color,int x,int y,int vx,int vy){
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
	}
}

