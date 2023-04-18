using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCollision : MonoBehaviour
{
    protected TrailRenderer trailComp;
    protected EdgeCollider2D trail;
    [SerializeField] protected int damageDealt;
    // Start is called before the first frame update
    void Start()
    {
        trailComp = this.GetComponent<TrailRenderer>();
        EdgeCollider2D trailCol = new EdgeCollider2D();
        trailCol.isTrigger = true;
        trail = trailCol.GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SetColliderPointsFromTrail(trailComp, this.GetComponent<EdgeCollider2D>());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damageDealt);
        }
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
                points.Add(trail.GetPosition(position));
            }
        collider.SetPoints(points);
    }
}
