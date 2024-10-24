using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class Chessboard : Puzzle
{
    public static Chessboard Instance;
    [HideInInspector] public static bool hasMissingPiece = false;

    public static string positions;

    public GameObject anchorPrefab;
    [HideInInspector] public static Anchor[,] board = new Anchor[8, 8];
    [HideInInspector] public static Piece selectedPiece;
    public GameObject piecePrefab;
    public FMODUnity.StudioEventEmitter MovePieceSFX;
    public HintInteractable ChessHI;
    public Hint ChessHint2;
    public Hint ChessHint3;

    [HideInInspector] public static int turn;
    //[HideInInspector] public static List<string> bestMoves = new List<string>();
    [HideInInspector] public static string[] bestMoves;
    [HideInInspector] public static Piece blackKing;

    [SerializeField] public Item photograph;

    [HideInInspector] public List<Piece> pieceList;

    [HideInInspector] public static bool puzzleComplete = false;
    [SerializeField] public SpriteRenderer chessBoardSR;
    [SerializeField] public Sprite completedBoardSprite;

    [SerializeField] private GameObject photographGrabbable;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("Tried to create more than one instance of the Chessboard singleton!");
            Destroy(this);
        }


        
        /*StreamReader reader = new StreamReader("Assets/Scripts/Puzzles/Chess/BestMoves.txt");
        string bestMove;
        while ((bestMove = reader.ReadLine()) != null)
        {
            bestMoves.Add(bestMove);
        }*/
        bestMoves = Resources.Load<TextAsset>("BestMoves").text.Split("\n");
        hasMissingPiece = false;
        turn = 0;

    }

    private void OnEnable()
    {
        if (puzzleComplete)
        {
            chessBoardSR.sprite = completedBoardSprite;
            if (photographGrabbable)
            {
                photographGrabbable.SetActive(true);
            }
        }
        else
        {
            turn = 0;

            pieceList = new List<Piece>();

            // populate anchors
            /*StreamReader anchorReader = new StreamReader("Assets/Scripts/Puzzles/Chess/AnchorPositions.txt");
            List<string> posStrings = new List<string>();*/
            string[] posStrings = Resources.Load<TextAsset>("AnchorPositions").text.Split("\n");

            /*for (int i = 0; i < 64; i++)
            {
                posStrings.Add(anchorReader.ReadLine());
            }*/

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


            

            AddPiecesFromFile("PiecesWhite");
            AddPiecesFromFile("PiecesBlack");
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
        puzzleComplete = true;
        yield return new WaitForSeconds(0.8f);
        blackKing.GetComponent<SpriteRenderer>().sprite = blackKing.fallenKingSprite;
        // play sound effect?
        photographGrabbable.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        for (int i = 1; i < 101; i++)
        {
            yield return new WaitForSeconds(0.02f);
            foreach (Piece piece in pieceList)
            {
                Color temp = Color.white;
                piece.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, (100.0f - i) / 100); 
                if (i ==  100)
                {
                    Destroy(piece.gameObject);
                }
            }
        }

        ChessHI.UpdateHint(ChessHint3);
        chessBoardSR.sprite = completedBoardSprite;
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
        /*StreamReader reader = new StreamReader(path);
        string line;*/
        bool isPlayerTeam = true;
        string[] pieces = Resources.Load<TextAsset>(path).text.Split("\n");
        foreach (string line in pieces)
        {
            
            if (line.Contains("white"))
            {
                Debug.Log(line);
                isPlayerTeam = true;
                continue;
            }
            else if (line.Contains("black"))
            {
                isPlayerTeam = false;
                continue;
            }

            Piece newPiece = Instantiate(piecePrefab, transform.position, Quaternion.identity).GetComponent<Piece>();
            newPiece.isPlayerTeam = isPlayerTeam;
            newPiece.type = (Piece.PieceType)Enum.Parse(typeof(Piece.PieceType), line.Substring(2));
            AddPieceToBoard(newPiece, int.Parse(line.Substring(0, 1)), int.Parse(line.Substring(1, 1)));
        }

        /*while ((line = reader.ReadLine()) != null)
        {
            
        }*/
    }

    public void AddMissingPiece()
    {
        ChessHI.UpdateHint(ChessHint2);
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
