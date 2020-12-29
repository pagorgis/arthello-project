using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OthelloGame : MonoBehaviour
{
    public static string currentTurn = "black";                         // Keep track of whose turn it is
    public static string[,] board = new string[8, 8];                   // Store the board information (which color on which position)
    public static List<string> validMoves = new List<string>();         // Store info about what moves are possible currently for the player
    public static bool changedValidMoves = false;                       // Check to see if validMoves needs to be updated
    public static List<string> piecesToConvert = new List<string>();    // Store info about what moves are possible currently for the player
    static readonly object Lock = new object();

    // Start is called before the first frame update
    void Start()
    {
        board[3, 3] = "white";
        board[3, 4] = "black";
        board[4, 3] = "black";
        board[4, 4] = "white";
        //board[4, 5] = "white";
        //board[4, 6] = "white";
        GetValidMoves();
        //PlacePiece("54");
        //GetValidMoves();
        //PlacePiece("42");
        //PlacePiece("23");
        //PlacePiece("47");
        //GetValidMoves();
        for (int z = 0; z < board.GetLength(0); z++)
        {
            string wack = "";
            for (int x = 0; x < board.GetLength(1); x++)
            {
                if (board[z, x] == "black" || board[z, x] == "white")
                {
                    wack = wack + board[z, x] + " ";
                }
                else
                {
                    wack = wack + "empty ";
                }
            }
            Debug.Log(wack);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (changedValidMoves)
        {
            GetValidMoves();
            changedValidMoves = false;
        }
    }

    // Main method for updating validMoves with new values
    public static void GetValidMoves()
    {
        validMoves.Clear();
        for (int z = 0; z < board.GetLength(0); z++)
        {
            for (int x = 0; x < board.GetLength(1); x++)
            {
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

    // Checks what squares are adjacent to the current square investigated and adds them to a temporary list
    // Called by GetValidMoves()
    static void CollectAdjacentSquares(int z, int x)
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

    // Checks if the current square has at least one path in any direction that would lead to the move being valid/correct
    // Called by CollectAdjacentSquares()
    static void IsValidMove(List<string> adjacentSquares, int z, int x)
    {
        for (int i = 0; i < adjacentSquares.Count; i++)
        {
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

    // Returns the direction to the adjacent square from the current square
    // Called by IsValidMove() and is used in CheckPath()
    static string GetDirection(int z, int x, int adjacentZ, int adjacentX)
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

    // Returns true if the path of pieces from the current square, in a specific direction, is a valid move.
    // Adjacent square has to start with the opposite piece color, then it has to reach the same color before extending 
    // board limits to be a valid move.
    // Called by IsValidMove(), direction from GetDirection()
    static bool CheckPath(string direction, int z, int x)
    {
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

    public static void PlacePiece(string position)
    {
        piecesToConvert.Clear();
        int z = int.Parse(position[0].ToString());
        int x = int.Parse(position[1].ToString());
        List<string> possibleDirections = GetPossibleDirections(position);
        DeterminePiecesToConvert(possibleDirections, z, x);
        board[z, x] = currentTurn;
        foreach (string piecePosition in piecesToConvert)
        {
            int convertZ = int.Parse(piecePosition[0].ToString());
            int convertX = int.Parse(piecePosition[1].ToString());
            if (board[convertZ, convertX] == GetOppositeColor())
            {
                board[convertZ, convertX] = currentTurn;
            }
        }
        currentTurn = GetOppositeColor();
        changedValidMoves = true;

        string conc = "PIECES TO CONVERT: ";
        foreach (string p in piecesToConvert)
        {
            conc = conc + p + " ";
        }
        Debug.Log(conc);
    }

    static List<string> GetPossibleDirections(string position)
    {
        if (position == "00")
        {
            return new List<string> { "E", "S", "SE" };
        }
        else if (position == "07")
        {
            return new List<string> { "W", "SW", "S" };
        }
        else if (position == "70")
        {
            return new List<string> { "N", "NE", "E" };
        }
        else if (position == "77")
        {
            return new List<string> { "NW", "N", "W" };
        }
        else if (position == "01" || position == "02" || position == "03" || position == "04" || position == "05" || position == "06")
        {
            return new List<string> { "W", "SW", "S", "SE", "E" };
        }
        else if (position == "71" || position == "72" || position == "73" || position == "74" || position == "75" || position == "76")
        {
            return new List<string> { "W", "NW", "N", "NE", "E" };
        }
        else if (position == "10" || position == "20" || position == "30" || position == "40" || position == "50" || position == "60")
        {
            return new List<string> { "N", "NE", "E", "SE", "S" };
        }
        else if (position == "17" || position == "27" || position == "37" || position == "47" || position == "57" || position == "67")
        {
            return new List<string> { "N", "NW", "W", "SW", "S" };
        }
        else
        {
            return new List<string> { "NW", "N", "NE", "E", "SE", "S", "SW", "W" };
        }
        return new List<string>();
    }

    static void DeterminePiecesToConvert(List<string> possibleDirections, int z, int x)
    {
        foreach (string direction in possibleDirections)
        {
            List<string> tempConvert = new List<string>();
            switch (direction)
            {
                case "N":
                    if (board[z - 1, x] == GetOppositeColor())
                    {
                        tempConvert.Add((z - 1) + "" + x);
                        for (int i = z - 2; i >= 0; i--)
                        {
                            if (board[i, x] == currentTurn)
                            {
                                piecesToConvert.AddRange(tempConvert);
                                break;
                            }
                            tempConvert.Add(i + "" + x);
                        }
                    }
                    break;

                case "NE":
                    if (board[z - 1, x + 1] == GetOppositeColor())
                    {
                        tempConvert.Add((z - 1) + "" + (x + 1));
                        int zCounter = z - 2;
                        int xCounter = x + 2;
                        while (zCounter >= 0 && xCounter < board.GetLength(1))
                        {
                            if (board[zCounter, xCounter] == currentTurn)
                            {
                                piecesToConvert.AddRange(tempConvert);
                                break;
                            }
                            tempConvert.Add(zCounter + "" + xCounter);
                            zCounter--;
                            xCounter++;
                        }
                    }
                    break;

                case "E":
                    if (board[z, x + 1] == GetOppositeColor())
                    {
                        tempConvert.Add(z + "" + (x + 1));
                        for (int i = x + 2; i < board.GetLength(1); i++)
                        {
                            if (board[z, i] == currentTurn)
                            {
                                piecesToConvert.AddRange(tempConvert);
                                break;
                            }
                            tempConvert.Add(z + "" + i);
                        }
                    }
                    break;

                case "SE":
                    if (board[z + 1, x + 1] == GetOppositeColor())
                    {
                        tempConvert.Add((z + 1) + "" + (x + 1));
                        int zCounter = z + 2;
                        int xCounter = x + 2;
                        while (zCounter < board.GetLength(0) && xCounter < board.GetLength(1))
                        {
                            if (board[zCounter, xCounter] == currentTurn)
                            {
                                piecesToConvert.AddRange(tempConvert);
                                break;
                            }
                            tempConvert.Add(zCounter + "" + xCounter);
                            zCounter++;
                            xCounter++;
                        }
                    }
                    break;

                case "S":
                    if (board[z + 1, x] == GetOppositeColor())
                    {
                        tempConvert.Add((z + 1) + "" + x);
                        for (int i = z + 2; i < board.GetLength(0); i++)
                        {
                            if (board[i, x] == currentTurn)
                            {
                                piecesToConvert.AddRange(tempConvert);
                                break;
                            }
                            tempConvert.Add(i + "" + x);
                        }
                    }
                    break;

                case "SW":
                    if (board[z + 1, x - 1] == GetOppositeColor())
                    {
                        tempConvert.Add((z + 1) + "" + (x - 1));
                        int zCounter = z + 2;
                        int xCounter = x - 2;
                        while (zCounter < board.GetLength(0) && xCounter >= 0)
                        {
                            if (board[zCounter, xCounter] == currentTurn)
                            {
                                piecesToConvert.AddRange(tempConvert);
                                break;
                            }
                            tempConvert.Add(zCounter + "" + xCounter);
                            zCounter++;
                            xCounter--;
                        }
                    }
                    break;

                case "W":
                    if (board[z, x - 1] == GetOppositeColor())
                    {
                        tempConvert.Add(z + "" + (x - 1));
                        for (int i = x - 2; i >= 0; i--)
                        {
                            if (board[z, i] == currentTurn)
                            {
                                piecesToConvert.AddRange(tempConvert);
                                break;
                            }
                            tempConvert.Add(z + "" + i);
                        }
                    }
                    break;

                case "NW":
                    if (board[z - 1, x - 1] == GetOppositeColor())
                    {
                        tempConvert.Add((z - 1) + "" + (x - 1));
                        int zCounter = z - 2;
                        int xCounter = x - 2;
                        while (zCounter >= 0 && xCounter >= 0)
                        {
                            if (board[zCounter, xCounter] == currentTurn)
                            {
                                piecesToConvert.AddRange(tempConvert);
                                break;
                            }
                            tempConvert.Add(zCounter + "" + xCounter);
                            zCounter--;
                            xCounter--;
                        }
                    }
                    break;

                default:
                    break;
            }
        }
    }

    public static void ApplyConvertOfPiece(string position)
    {
        lock (Lock)
        {
            string conc = "APPLYCONVERTOFPIECE: ";
            foreach (string p in piecesToConvert)
            {
                conc = conc + p + " ";
            }
            Debug.Log(conc);
            for (int i = 0; i < piecesToConvert.Count; i++)
            {
                if (piecesToConvert[i] == position)
                {
                    piecesToConvert.RemoveAt(i);
                    break;
                }
            }
        }
    }

    // Returns the opposite color of current player
    static string GetOppositeColor()
    {
        return currentTurn == "black" ? "white" : "black";
    }
}
