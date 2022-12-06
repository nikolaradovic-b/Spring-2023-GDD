using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterProjectile : Bullet
{
    [SerializeField] private float timeBeforeScattering = 1.0f;
    [SerializeField] private int numberOfScatters = 8;
    [SerializeField] private GameObject scatterProjectile = null;
    [SerializeField] private float scatteredProjectileSpeedReductionFactor = 2.0f;
    [SerializeField] private float scatteredX = 5f;

    protected override void Start()
    {
        StartCoroutine(DelayScatter());
        Destroy(gameObject, 5f);
    }

    private IEnumerator DelayScatter()
    {
        yield return new WaitForSeconds(timeBeforeScattering);
        Scatter();
        Destroy(gameObject);
    }

    private void Scatter()
    {
        for (int i = 0; i < numberOfScatters; ++i)
        {
            var scatterProjectileInstance = Instantiate(scatterProjectile,
                transform.position, transform.rotation);
            var instanceRigidbody = scatterProjectileInstance.GetComponent<Rigidbody2D>();
            var rigidBody = GetComponent<Rigidbody2D>();
            instanceRigidbody.velocity = rigidBody.velocity / scatteredProjectileSpeedReductionFactor;

            if (i > 0)
            {
                var angle = i * 2 * Mathf.PI / numberOfScatters;
                var xComponent = Mathf.Cos(angle) * scatteredX - Mathf.Sin(angle) * rigidBody.velocity.y;
                var yComponent = Mathf.Sin(angle) * scatteredX + Mathf.Cos(angle) * rigidBody.velocity.y;
                instanceRigidbody.velocity = new Vector2(xComponent / scatteredProjectileSpeedReductionFactor,
                    yComponent / scatteredProjectileSpeedReductionFactor);
            }
        }
    }
}
