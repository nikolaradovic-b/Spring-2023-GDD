using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitVFX = null;

    private GameObject player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        gameObject.layer = player.layer;

        string name = player.GetComponent<SpriteRenderer>().sortingLayerName;
        GetComponent<SpriteRenderer>().sortingLayerName = name;
        SpriteRenderer[] srs = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in srs)
        {
            sr.sortingLayerName = name;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>()) { return; }
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.layer = gameObject.layer;
        vfx.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
        Destroy(vfx, 5f);
        Destroy(gameObject);
    }
}
