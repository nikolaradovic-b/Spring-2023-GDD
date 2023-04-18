using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCol : MonoBehaviour
{
    TrailRenderer myTrail;
    EdgeCollider2D myCollider;
    [SerializeField] protected string tagToAvoid = "Enemy";
    [SerializeField] protected string tagToAvoid2 = "Bullet";
    [SerializeField] protected int damageDealt = 1;

    static List<EdgeCollider2D> unusedColliders = new List<EdgeCollider2D>();

    void Awake()
    {
        myTrail = this.GetComponent<TrailRenderer>();
        //myCollider = GetValidCollider();
        myCollider = this.GetComponent<EdgeCollider2D>();
    }

    void Update()
    {
        SetColliderPointsFromTrail(myTrail, myCollider);
        myCollider.offset.Set(-myTrail.transform.position.x, -myTrail.transform.position.y);
        //myCollider.transform.position = myCollider.transform.position - transform.position;
    }

    //Gets from unused pool or creates one if none in pool
    EdgeCollider2D GetValidCollider()
    {
        EdgeCollider2D validCollider;
        if (unusedColliders.Count > 0)
        {
            validCollider = unusedColliders[0];
            validCollider.enabled = true;
            unusedColliders.RemoveAt(0);
        }
        else
        {
            validCollider = new GameObject("TrailCollider", typeof(EdgeCollider2D)).GetComponent<EdgeCollider2D>();
        }
        return validCollider;
    }

    void SetColliderPointsFromTrail(TrailRenderer trail, EdgeCollider2D collider)
    {
        List<Vector2> points = new List<Vector2>();
        //avoid having default points at (-.5,0),(.5,0)
        if (trail.positionCount == 0)
        {
            points.Add(transform.position);
            points.Add(transform.position);
        }
        else for (int position = 0; position < trail.positionCount; position++)
            {
                //ignores z axis when translating vector3 to vector2
                //Debug.Log(trail.GetPosition(position));
                points.Add(trail.GetPosition(position) - myCollider.transform.position);
            }
        //Debug.Log(string.Join("", points));
        collider.SetPoints(points);
    }

    void OnDestroy()
    {
        if (myCollider != null)
        {
            myCollider.enabled = false;
            unusedColliders.Add(myCollider);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagToAvoid || collision.gameObject.tag == tagToAvoid2) { return; }
        //GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        //vfx.layer = gameObject.layer;
        //vfx.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;

        if (collision.gameObject.GetComponent<Health>())
        {
            Debug.Log("ENTERED");
            collision.gameObject.GetComponent<Health>().TakeDamage(damageDealt);
        }


    }
}
