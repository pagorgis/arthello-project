using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OthelloGame : MonoBehaviour
{
    public static string currentTurn = "black";             // Keep track of whose turn it is
    public static string[,] board = new string[8, 8];       // Store the board information (which color on which position)
    public static List<string> validMoves = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        board[3, 3] = "white";
        board[3, 4] = "black";
        board[4, 3] = "black";
        board[4, 4] = "white";
        //Debug.Log(board[3, 4]);
        GetValidMoves();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetValidMoves()
    {
        validMoves.Clear();
        for (int z = 0; z < board.GetLength(0); z++)
        {
            for (int x = 0; x < board.GetLength(1); x++)
            {
                //string currentSquarePos = "" + (z + 1) + (x + 1);
                //Debug.Log("CURRENT: " + z + " | " + x);
                if (board[z, x] == "black" || board[z, x] == "white")
                {
                    continue;
                }
                else
                {
                    CollectAdjacentSquares(z, x);
                }
                
            }
        }
        for (int i = 0; i < validMoves.Count; i++)
        {
            Debug.Log(validMoves[i]);
        }
    }

    public void CollectAdjacentSquares(int z, int x)
    {
        List<string> adjacentSquares = new List<string>();
        string concatParams = z + "" + x;
        if (concatParams == "00")
        {
            adjacentSquares.Add("01");
            adjacentSquares.Add("10");
            adjacentSquares.Add("11");
        }
        else if (concatParams == "07")
        {
            adjacentSquares.Add("06");
            adjacentSquares.Add("16");
            adjacentSquares.Add("17");
        }
        else if (concatParams == "70")
        {
            adjacentSquares.Add("60");
            adjacentSquares.Add("61");
            adjacentSquares.Add("71");
        }
        else if (concatParams == "77")
        {
            adjacentSquares.Add("66");
            adjacentSquares.Add("67");
            adjacentSquares.Add("76");
        }
        else if (concatParams == "01" || concatParams == "02" || concatParams == "03" || concatParams == "04" || concatParams == "05" || concatParams == "06")
        {
            adjacentSquares.Add("" + z + (x - 1));
            adjacentSquares.Add("" + (z + 1) + (x - 1));
            adjacentSquares.Add("" + (z + 1) + x);
            adjacentSquares.Add("" + (z + 1) + (x + 1));
            adjacentSquares.Add("" + z + (x + 1));
        }
        else if (concatParams == "71" || concatParams == "72" || concatParams == "73" || concatParams == "74" || concatParams == "75" || concatParams == "76")
        {
            adjacentSquares.Add("" + z + (x - 1));
            adjacentSquares.Add("" + (z - 1) + (x - 1));
            adjacentSquares.Add("" + (z - 1) + x);
            adjacentSquares.Add("" + (z - 1) + (x + 1));
            adjacentSquares.Add("" + z + (x + 1));
        }
        else if (concatParams == "10" || concatParams == "20" || concatParams == "30" || concatParams == "40" || concatParams == "50" || concatParams == "60")
        {
            adjacentSquares.Add("" + (z - 1) + x);
            adjacentSquares.Add("" + (z - 1) + (x + 1));
            adjacentSquares.Add("" + z + (x + 1));
            adjacentSquares.Add("" + (z + 1) + (x + 1));
            adjacentSquares.Add("" + (z + 1) + x);
        }
        else if (concatParams == "17" || concatParams == "27" || concatParams == "37" || concatParams == "47" || concatParams == "57" || concatParams == "67")
        {
            adjacentSquares.Add("" + (z - 1) + x);
            adjacentSquares.Add("" + (z - 1) + (x - 1));
            adjacentSquares.Add("" + z + (x - 1));
            adjacentSquares.Add("" + (z + 1) + (x - 1));
            adjacentSquares.Add("" + (z + 1) + x);
        }
        else
        {
            adjacentSquares.Add("" + (z - 1) + (x - 1));
            adjacentSquares.Add("" + (z - 1) + x);
            adjacentSquares.Add("" + (z - 1) + (x + 1));
            adjacentSquares.Add("" + z + (x + 1));
            adjacentSquares.Add("" + (z + 1) + (x + 1));
            adjacentSquares.Add("" + (z + 1) + x);
            adjacentSquares.Add("" + (z + 1) + (x - 1));
            adjacentSquares.Add("" + z + (x - 1));
        }
        IsValidMove(adjacentSquares, z, x);
    }

    public void IsValidMove(List<string> adjacentSquares, int z, int x)
    {
        for (int i = 0; i < adjacentSquares.Count; i++)
        {
            //Debug.Log(adjacentSquares[i][0] + " " + adjacentSquares[i][1]);
            int adjacentZ = int.Parse(adjacentSquares[i][0].ToString());
            int adjacentX = int.Parse(adjacentSquares[i][1].ToString());
            string direction = GetDirection(z, x, adjacentZ, adjacentX);
            bool validPathExists = CheckPath(direction, z, x);
            if (validPathExists)
            {
                validMoves.Add(z + "" + x);
                break;
            }
        }
    }

    public string GetDirection(int z, int x, int adjacentZ, int adjacentX)
    {
        if (adjacentZ < z && adjacentX == x) return "N";
        if (adjacentZ < z && adjacentX > x) return "NE";
        if (adjacentZ == z && adjacentX > x) return "E";
        if (adjacentZ > z && adjacentX > x) return "SE";
        if (adjacentZ > z && adjacentX == x) return "S";
        if (adjacentZ > z && adjacentX < x) return "SW";
        if (adjacentZ == z && adjacentX < x) return "W";
        if (adjacentZ < z && adjacentX < x) return "NW";
        return "FAIL";
    }

    public bool CheckPath(string direction, int z, int x)
    {
        //Debug.Log("Checkpath: " + z + ", " + x);
        switch (direction)
        {
            case "N":
                if (board[z - 1, x] == GetOppositeColor())
                {
                    for (int i = z - 2; i >= 0; i--)
                    {
                        if (board[i, x] == currentTurn)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                return false;

            case "NE":
                if (board[z - 1, x + 1] == GetOppositeColor())
                {
                    int zCounter = z - 2;
                    int xCounter = x + 2;
                    while (zCounter >= 0 && xCounter < board.GetLength(1))
                    {
                        if (board[zCounter, xCounter] == currentTurn)
                        {
                            return true;
                        }
                        zCounter--;
                        xCounter++;
                    }
                    return false;
                }
                return false;

            case "E":
                if (board[z, x + 1] == GetOppositeColor())
                {
                    for (int i = x + 2; i < board.GetLength(1); i++)
                    {
                        if (board[z, i] == currentTurn)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                return false;

            case "SE":
                if (board[z + 1, x + 1] == GetOppositeColor())
                {
                    int zCounter = z + 2;
                    int xCounter = x + 2;
                    while (zCounter < board.GetLength(0) && xCounter < board.GetLength(1))
                    {
                        if (board[zCounter, xCounter] == currentTurn)
                        {
                            return true;
                        }
                        zCounter++;
                        xCounter++;
                    }
                    return false;
                }
                return false;

            case "S":
                if (board[z + 1, x] == GetOppositeColor())
                {
                    for (int i = z + 2; i < board.GetLength(0); i++)
                    {
                        if (board[i, x] == currentTurn)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                return false;

            case "SW":
                if (board[z + 1, x - 1] == GetOppositeColor())
                {
                    int zCounter = z + 2;
                    int xCounter = x - 2;
                    while (zCounter < board.GetLength(0) && xCounter >= 0)
                    {
                        if (board[zCounter, xCounter] == currentTurn)
                        {
                            return true;
                        }
                        zCounter++;
                        xCounter--;
                    }
                    return false;
                }
                return false;

            case "W":
                if (board[z, x - 1] == GetOppositeColor())
                {
                    for (int i = x - 2; i >= 0; i--)
                    {
                        if (board[z, i] == currentTurn)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                return false;

            case "NW":
                if (board[z - 1, x - 1] == GetOppositeColor())
                {
                    int zCounter = z - 2;
                    int xCounter = x - 2;
                    while (zCounter >= 0 && xCounter >= 0)
                    {
                        if (board[zCounter, xCounter] == currentTurn)
                        {
                            return true;
                        }
                        zCounter--;
                        xCounter--;
                    }
                    return false;
                }
                return false;

            default:
                return false;

        }
    }

    public string GetOppositeColor()
    {
        return currentTurn == "black" ? "white" : "black";
    }
}
