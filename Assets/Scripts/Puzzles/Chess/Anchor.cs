using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    public Piece myPiece;
    public SpriteRenderer myRenderer;
    public int x, y;
    public Chessboard chessboard;

    private void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (InventoryManager.Instance.SelectedItem != null)
        {
            GetComponent<CircleCollider2D>().radius = (InventoryManager.Instance.SelectedItem.GetItemID() == ItemID.ROOK) ? 0.01f : 1.25f;
        }
    }

    private void OnDisable()
    {
        myRenderer.enabled = false;
    }

    public void MovePieceHere()
    {
        if (!IsBestMove(Chessboard.selectedPiece.posX, Chessboard.selectedPiece.posY))
        {
            // apply time penalty?
            CountdownTimer.Instance.AddTimePuzzleComplete(-10);
            return;
        }

        if (myPiece != null)
        {
            chessboard.pieceList.Remove(myPiece);
            Destroy(myPiece.gameObject);
        }

        foreach (Anchor anchor in Chessboard.board)
        {
            if (anchor.myPiece != null)
            {
                
                anchor.myPiece.isSelected = false;
                if (anchor.myPiece == Chessboard.selectedPiece)
                {
                    anchor.myPiece = null;
                }
            }
            
            anchor.myRenderer.enabled = false;
        }

        myPiece = Chessboard.selectedPiece;
        myPiece.targetPos = transform.position;
        myPiece.posX = x; 
        myPiece.posY = y;
        Chessboard.selectedPiece = null;
        // sfx go here
        Chessboard.Instance.MovePieceSFX.Play();
        Chessboard.turn++;

        // black makes their move, if it's black's turn
        if (Chessboard.turn % 2 == 1)
        {
            if (Chessboard.turn >= Chessboard.bestMoves.Length)
            {
                Chessboard.selectedPiece = null;
                StartCoroutine(chessboard.CompletePuzzle());
            }
            else
            {
                StartCoroutine(Chessboard.MoveBlackPiece());
            }
        }

    }

    private void OnMouseDown()
    {
        if (myRenderer.enabled && !Chessboard.puzzleComplete)
        {
            MovePieceHere();
        }
    }

    [ContextMenu("Click me!")]
    public void OnClickAnchor()
    {
        if (myRenderer.enabled)
        {
            MovePieceHere();
        }
    }


    private bool IsBestMove(int startX, int startY)
    {
        string bestMove = Chessboard.bestMoves[Chessboard.turn];
        return (startX == int.Parse(bestMove.Substring(0, 1)) && startY == int.Parse(bestMove.Substring(1, 1)) && x == int.Parse(bestMove.Substring(2, 1)) && y == int.Parse(bestMove.Substring(3, 1)));
    }
}
