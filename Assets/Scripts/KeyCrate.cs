using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCrate : MonoBehaviour
{

    [SerializeField] private GameObject hitVFX = null;
    [SerializeField] private GameObject key = null;
    [SerializeField] private Transform origin = null;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeathSequence()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.layer = gameObject.layer;
        vfx.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;

        Instantiate(key, origin.position, origin.rotation);
    }
}
