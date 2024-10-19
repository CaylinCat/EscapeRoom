using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInspector : MonoBehaviour
{
    public SpriteRenderer CoinSR;
    public Sprite SymbolSprite;
    public Sprite NumberSprite;
    private bool _flipped;

    public void FlipCoin()
    {
        _flipped = !_flipped;
        if(_flipped) CoinSR.sprite = NumberSprite;
        else CoinSR.sprite = SymbolSprite;
    }
}
