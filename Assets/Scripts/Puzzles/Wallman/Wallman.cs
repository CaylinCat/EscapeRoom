using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallman : Puzzle
{
    [HideInInspector] public static bool canPlayPuzzle = false; // set to true once the player talks to third armor

    [SerializeField] public GameObject[] armorPieces;

    [SerializeField] public Item heartItem;

    public FMODUnity.StudioEventEmitter ArmorHitSFX;
    [SerializeField] private Sprite noHeartSprite;

    [SerializeField] private ParticleSystem particle;

    private int health;

    private bool puzzleComplete;

    private void Awake()
    {
        health = 54;
        puzzleComplete = false;
        transform.position += Vector3.forward;
    }

    public void OnClick()
    {
        if (!canPlayPuzzle)
        {
            return;
        }

        health--;
        ArmorHitSFX.Play();
        // particles and sfx go here
        particle.gameObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        particle.Stop();
        particle.Play();
        if (health % 9 == 0 && !puzzleComplete)
        {
            int index = (54 - health) / 9;
            Rigidbody2D body = armorPieces[index - 1].GetComponent<Rigidbody2D>();
            body.bodyType = RigidbodyType2D.Dynamic;
            body.AddForce(new Vector2(Random.Range(-100f, 100f), Random.Range(-100f, 100f)));
            StartCoroutine(destroyArmorPiece(index - 1));
        }

        if (health <= -1 && !puzzleComplete)
        {
            InventoryManager.Instance.AddItem(heartItem);
            puzzleComplete = true;
            GetComponent<SpriteRenderer>().sprite = noHeartSprite;
            ArmorPuzzle.progress++;
            OnComplete();
        }
    }

    private IEnumerator destroyArmorPiece(int index)
    {
        yield return new WaitForSeconds(5f);
        armorPieces[index].SetActive(false);
    }
}
