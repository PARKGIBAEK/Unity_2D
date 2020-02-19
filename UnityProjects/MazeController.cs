using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MazeController : MonoBehaviour
{
    int[,] mazeArr = new int[20, 20];//[y,x] 배열 생성
    int[,] mazeArrCopy = new int[20, 20];
    
    int x;
    int y;
    bool[,] visitLog = new bool[20, 20];
    System.Random rand = new System.Random();
    Tuple<int, int> startPoint = new Tuple<int, int>(1, 1);
    Tuple<int, int> endPoint = new Tuple<int, int>(18, 18);
    
    //Stack<KeyValuePair<int, int>> way = new Stack<KeyValuePair<int, int>>();

    int[] dy = { -1, 0, 1, 0 };//방향이동을 위한 배열
    int[] dx = { 0, 1, 0, -1 };//[y-1,x+0]으로 North로 이동, [y+0,x+1]로 East로 이동,[y+1,x+0]으로 South로 이동,[y+0,x-1]로 West로 이동


    enum blockType
    {
        pathway,//갈수 있는 길
        wall,//벽
        visited,//방문한 위치
        backtraced,//방문했다 되돌아 나온 위치
    }
    void Start()
    {
        makeMaze(mazeArr);
        checkMaze(mazeArr);
    }

    void Update()
    {

    }
    void makeMaze(int[,] maze)
    {//가장자리는 벽, 시작점은 [1,1] 도착점은[18,18]
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                if (i == 0 || i == 19 || j == 0 || j == 19)
                    maze[i, j] = 1;
                else if ((i == 1 && j == 1) || (i == 18 && j == 18))
                    maze[i, j] = 0;
                else
                    maze[i, j] = rand.Next(2);
            }
        }
    }
    void checkMaze(int[,] maze)
    {
        for(int i=1;i<19;i++)
        {
            for(int j=1;j<19;j++)
            {

            }
        }
    }
}
