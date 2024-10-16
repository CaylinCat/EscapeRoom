using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [HideInInspector] public bool isPlayerTeam;
    public enum PieceType
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King,
    }

    [HideInInspector] public PieceType type;

    [HideInInspector] public bool isSelected;
    [HideInInspector] public int posX, posY;
    private List<int[]> moves = new List<int[]>();

    [SerializeField] private Sprite[] sprites;
    [SerializeField] public Sprite fallenKingSprite;

    public float moveSpeed;
    [HideInInspector] public Vector3 targetPos;

    private void Start()
    {
        int spriteIndex = 0;
        if (!isPlayerTeam)
        {
            spriteIndex += 3;

            if (type == PieceType.King)
            {
                Chessboard.blackKing = this;
            }
        }

        switch (type)
        {
            case PieceType.Pawn:
                spriteIndex++;
                break;

            case PieceType.Rook:
                spriteIndex += 2;
                break;
        }

        GetComponent<SpriteRenderer>().sprite = sprites[spriteIndex];
    }
    private void Update()
    {
        if (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }

    public void ShowMoves()
    {
        foreach (int[] move in moves)
        {
            Chessboard.board[move[0], move[1]].myRenderer.enabled = true;
        }
    }
    public void SelectPiece()
    {
        foreach (Anchor anchor in Chessboard.board)
        {
            if (anchor.myPiece != null)
            {
                anchor.myPiece.isSelected = false;
            }
            anchor.myRenderer.enabled = false;
        }

        isSelected = true;
        Chessboard.selectedPiece = this;
        GetMoves();
        ShowMoves();
    }

    public void GetMoves()
    {
        moves = new List<int[]>();

        switch (type)
        {
            case PieceType.Pawn:
                if (posY == (isPlayerTeam ? 7 : 0))
                {
                    break;
                }

                if ((isPlayerTeam && posY == 1) || (!isPlayerTeam && posY == 6)) // if the pawn is on its starting rank, it can move by 2
                {
                    AddMoveToList(posX, posY + (isPlayerTeam ? 2 : -2));
                }


                if (Chessboard.board[posX, posY + (isPlayerTeam ? 1 : -1)].myPiece == null) // straight ahead
                {
                    AddMoveToList(posX, posY + (isPlayerTeam ? 1 : -1));
                }
                if (posX != 7 && IsEnemyPiece(Chessboard.board[posX + 1, posY + (isPlayerTeam ? 1 : -1)].myPiece)) // diagonal right
                {
                    AddMoveToList(posX + 1, posY + (isPlayerTeam ? 1 : -1));
                }
                if (posX != 0 && IsEnemyPiece(Chessboard.board[posX - 1, posY + (isPlayerTeam ? 1 : -1)].myPiece)) // diagonal left
                {
                    AddMoveToList(posX - 1, posY + (isPlayerTeam ? 1 : -1));
                }
                break;

            case PieceType.Knight:
                AddMoveToList(posX + 1, posY + 2);
                AddMoveToList(posX - 1, posY + 2);
                AddMoveToList(posX + 1, posY - 2);
                AddMoveToList(posX - 1, posY - 2);
                AddMoveToList(posX + 2, posY + 1);
                AddMoveToList(posX - 2, posY + 1);
                AddMoveToList(posX + 2, posY - 1);
                AddMoveToList(posX - 2, posY - 1);
                break;

            case PieceType.Bishop:
                for (int i = 1; i < 8; i++) // up right
                {
                    AddMoveToList(posX + i, posY + i);
                    if (IsOutOfBounds(posX + i, posY + i) || Chessboard.board[posX + i, posY + i].myPiece != null)
                    {
                        break;
                    }
                }

                for (int i = 1; i < 8; i++) // up left
                {
                    AddMoveToList(posX - i, posY + i);
                    if (IsOutOfBounds(posX - i, posY + i) || Chessboard.board[posX - i, posY + i].myPiece != null)
                    {
                        break;
                    }
                }

                for (int i = 1; i < 8; i++) // down left
                {
                    AddMoveToList(posX - i, posY - i);
                    if (IsOutOfBounds(posX - i, posY - i) || Chessboard.board[posX - i, posY - i].myPiece != null)
                    {
                        break;
                    }
                }

                for (int i = 1; i < 8; i++) // down right
                {
                    AddMoveToList(posX + i, posY - i);
                    if (IsOutOfBounds(posX + i, posY - i) || Chessboard.board[posX + i, posY - i].myPiece != null)
                    {
                        break;
                    }
                }

                break;

            case PieceType.Rook:
                for (int i = 1; i < 8; i++) // right
                {
                    AddMoveToList(posX + i, posY);
                    if (IsOutOfBounds(posX + i, posY) || Chessboard.board[posX + i, posY].myPiece != null)
                    {
                        break;
                    }
                }

                for (int i = 1; i < 8; i++) // up
                {
                    AddMoveToList(posX, posY + i);
                    if (IsOutOfBounds(posX, posY + i) || Chessboard.board[posX, posY + i].myPiece != null)
                    {
                        break;
                    }
                }

                for (int i = 1; i < 8; i++) // left
                {
                    AddMoveToList(posX - i, posY);
                    if (IsOutOfBounds(posX - i, posY) || Chessboard.board[posX - i, posY].myPiece != null)
                    {
                        break;
                    }
                }

                for (int i = 1; i < 8; i++) // down
                {
                    AddMoveToList(posX, posY - i);
                    if (IsOutOfBounds(posX, posY - i) || Chessboard.board[posX, posY - i].myPiece != null)
                    {
                        break;
                    }
                }
                break;

            case PieceType.Queen:
                for (int i = 1; i < 8; i++) // up right
                {
                    AddMoveToList(posX + i, posY + i);
                    if (IsOutOfBounds(posX + i, posY + i) || Chessboard.board[posX + i, posY + i].myPiece != null)
                    {
                        break;
                    }
                }

                for (int i = 1; i < 8; i++) // up left
                {
                    AddMoveToList(posX - i, posY + i);
                    if (IsOutOfBounds(posX - i, posY + i) || Chessboard.board[posX - i, posY + i].myPiece != null)
                    {
                        break;
                    }
                }

                for (int i = 1; i < 8; i++) // down left
                {
                    AddMoveToList(posX - i, posY - i);
                    if (IsOutOfBounds(posX - i, posY - i) || Chessboard.board[posX - i, posY - i].myPiece != null)
                    {
                        break;
                    }
                }

                for (int i = 1; i < 8; i++) // down right
                {
                    AddMoveToList(posX + i, posY - i);
                    if (IsOutOfBounds(posX + i, posY - i) || Chessboard.board[posX + i, posY - i].myPiece != null)
                    {
                        break;
                    }
                }

                for (int i = 1; i < 8; i++) // right
                {
                    AddMoveToList(posX + i, posY);
                    if (IsOutOfBounds(posX + i, posY) || Chessboard.board[posX + i, posY].myPiece != null)
                    {
                        break;
                    }
                }

                for (int i = 1; i < 8; i++) // up
                {
                    AddMoveToList(posX, posY + i);
                    if (IsOutOfBounds(posX, posY + i) || Chessboard.board[posX, posY + i].myPiece != null)
                    {
                        break;
                    }
                }

                for (int i = 1; i < 8; i++) // left
                {
                    AddMoveToList(posX - i, posY);
                    if (IsOutOfBounds(posX - i, posY) || Chessboard.board[posX - i, posY].myPiece != null)
                    {
                        break;
                    }
                }

                for (int i = 1; i < 8; i++) // down
                {
                    AddMoveToList(posX, posY - i);
                    if (IsOutOfBounds(posX, posY - i) || Chessboard.board[posX, posY - i].myPiece != null)
                    {
                        break;
                    }
                }
                break;

            case PieceType.King:
                AddMoveToList(posX + 1, posY);
                AddMoveToList(posX + 1, posY + 1);
                AddMoveToList(posX, posY + 1);
                AddMoveToList(posX - 1, posY + 1);
                AddMoveToList(posX - 1, posY);
                AddMoveToList(posX - 1, posY - 1);
                AddMoveToList(posX, posY - 1);
                AddMoveToList(posX + 1, posY - 1);
                break;
        }
    }

    private void AddMoveToList(int x, int y)
    {
        if (x >= 0 && x < 8 && y >= 0 && y < 8 && (Chessboard.board[x, y].myPiece == null || IsEnemyPiece(Chessboard.board[x, y].myPiece)))
        {
            moves.Add(new int[] { x, y });
        }
    }

    private bool IsOutOfBounds(int x, int y)
    {
        return (x < 0 || x > 7 || y < 0 || y > 7);
    }

    private bool IsEnemyPiece(Piece piece)
    {
        if (piece == null) return false;

        return (isPlayerTeam && !piece.isPlayerTeam) || (!isPlayerTeam && piece.isPlayerTeam);
    }

    private void OnMouseDown()
    {
        if (!IsEnemyPiece(Chessboard.selectedPiece))
        {
            SelectPiece();
        }
        else if (Chessboard.board[posX, posY].myRenderer.enabled)
        {
            Chessboard.board[posX, posY].MovePieceHere();
        }

    }
}
