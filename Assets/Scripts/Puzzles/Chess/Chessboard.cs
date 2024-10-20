using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class Chessboard : Puzzle
{
    [HideInInspector] public static bool hasMissingPiece = false;

    public static string positions;

    public GameObject anchorPrefab;
    [HideInInspector] public static Anchor[,] board = new Anchor[8, 8];
    [HideInInspector] public static Piece selectedPiece;
    public GameObject piecePrefab;

    [HideInInspector] public static int turn;
    [HideInInspector] public static List<string> bestMoves = new List<string>();
    [HideInInspector] public static Piece blackKing;

    [SerializeField] public Item scrollItem;
    [SerializeField] public Item photograph;

    [HideInInspector] public List<Piece> pieceList;

    [HideInInspector] public static bool puzzleComplete = false;
    [SerializeField] public SpriteRenderer chessBoardSR;
    [SerializeField] public Sprite completedBoardSprite;


    private void Awake()
    {
        StreamReader reader = new StreamReader("Assets/Scripts/Puzzles/Chess/BestMoves.txt");
        string bestMove;
        while ((bestMove = reader.ReadLine()) != null)
        {
            bestMoves.Add(bestMove);
        }

    }

    private void OnEnable()
    {
        if (puzzleComplete)
        {
            chessBoardSR.sprite = completedBoardSprite;
        }
        else
        {
            turn = 0;

            pieceList = new List<Piece>();

            // populate anchors
            StreamReader anchorReader = new StreamReader("Assets/Scripts/Puzzles/Chess/AnchorPositions.txt");
            List<string> posStrings = new List<string>();

            for (int i = 0; i < 64; i++)
            {
                posStrings.Add(anchorReader.ReadLine());
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    string[] posString = posStrings[i + j * 8].Split();

                    board[i, j] = Instantiate(anchorPrefab, new Vector3(float.Parse(posString[1]), float.Parse(posString[2]) - 2f, -2), Quaternion.identity, transform).GetComponent<Anchor>();
                    board[i, j].x = i;
                    board[i, j].y = j;
                    board[i, j].chessboard = this;
                }
            }

            if (hasMissingPiece)
            {
                AddMissingPiece();
            }


            

            AddPiecesFromFile("Assets/Scripts/Puzzles/Chess/PiecesWhite.txt");
            AddPiecesFromFile("Assets/Scripts/Puzzles/Chess/PiecesBlack.txt");
        }
    }




    public static IEnumerator MoveBlackPiece()
    {
        yield return new WaitForSeconds(0.8f);
        string blackMove = bestMoves[turn];
        selectedPiece = board[int.Parse(blackMove.Substring(0, 1)), int.Parse(blackMove.Substring(1, 1))].myPiece;
        Anchor target = board[int.Parse(blackMove.Substring(2, 1)), int.Parse(blackMove.Substring(3, 1))];
        target.MovePieceHere();
    }

    public IEnumerator CompletePuzzle()
    {
        OnComplete();
        yield return new WaitForSeconds(0.8f);
        blackKing.GetComponent<SpriteRenderer>().sprite = blackKing.fallenKingSprite;
        // play sound effect?
        InventoryManager.Instance.AddItem(photograph);
        yield return new WaitForSeconds(1.0f);
        for (int i = 1; i < 11; i++)
        {
            yield return new WaitForSeconds(0.2f);
            foreach (Piece piece in pieceList)
            {
                piece.GetComponent<SpriteRenderer>().color = Color.white.WithAlpha((10.0f - i) / 10);
                if (i ==  10)
                {
                    Destroy(piece.gameObject);
                }
            }
        }

        chessBoardSR.sprite = completedBoardSprite;
        puzzleComplete = true;

        
    }

    private void AddPieceToBoard(Piece piece, int posX, int posY)
    {
        board[posX, posY].myPiece = piece;
        piece.transform.position = board[posX, posY].transform.position;
        piece.transform.position -= Vector3.forward * 2;
        piece.transform.parent = transform;
        piece.targetPos = piece.transform.position;
        piece.posX = posX;
        piece.posY = posY;
        pieceList.Add(piece);
    }

    private void AddPiecesFromFile(string path)
    {
        StreamReader reader = new StreamReader(path);
        string line;
        bool isPlayerTeam = true;
        while ((line = reader.ReadLine()) != null)
        {
            if (line.Equals("white"))
            {
                isPlayerTeam = true;
                continue;
            }
            else if (line.Equals("black"))
            {
                isPlayerTeam = false;
                continue;
            }

            Piece newPiece = Instantiate(piecePrefab, transform.position, Quaternion.identity).GetComponent<Piece>();
            newPiece.isPlayerTeam = isPlayerTeam;
            newPiece.type = (Piece.PieceType)Enum.Parse(typeof(Piece.PieceType), line.Substring(2));
            AddPieceToBoard(newPiece, int.Parse(line.Substring(0, 1)), int.Parse(line.Substring(1, 1)));
        }
    }

    public void AddMissingPiece()
    {
        Piece missingPiece = Instantiate(piecePrefab, transform.position, Quaternion.identity).GetComponent<Piece>();
        missingPiece.type = Piece.PieceType.Rook;
        missingPiece.isPlayerTeam = true;
        AddPieceToBoard(missingPiece, 2, 4);
        hasMissingPiece = true;
        foreach (Anchor anchor in board)
        {
            anchor.GetComponent<CircleCollider2D>().radius = 1.25f;
        }
    }
}
