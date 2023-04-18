using Cainos.PixelArtTopDown_Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected GameObject hitVFX = null;
    [SerializeField] protected string tagToAvoid = "Player";
    [SerializeField] protected string tagToAvoid2 = "Bullet";
    [SerializeField] protected int damageDealt = 1;

    protected GameObject player;

    protected virtual void Start()
    {
        // Check scene index, if it's boss level, enable collider and disable trigger
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            // It's boss!
            GetComponent<Collider2D>().isTrigger = false;
        }
        else
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

        Destroy(gameObject, 5f);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagToAvoid || collision.gameObject.tag == tagToAvoid2 || collision.gameObject.GetComponent<LayerTrigger>()) { return; }
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.layer = gameObject.layer;
        vfx.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
        if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damageDealt);
        }

        Destroy(vfx, 1f);
        Destroy(gameObject);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == tagToAvoid || collision.gameObject.tag == tagToAvoid2 || collision.gameObject.GetComponent<LayerTrigger>()) { return; }
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.layer = gameObject.layer;
        vfx.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
        if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damageDealt, BulletType.Bullet);
        }

        Destroy(vfx, 1f);
        Destroy(gameObject);
    }
}
