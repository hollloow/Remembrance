using System;
using UnityEngine;

public class MagicMissil : BaseMagic
{
    private int damage = 20;
    private float travelSpeed =4;
    private float lifeTime = 5;
    private float living;

    public override void ApplyMagicEffect(float direction)
    {
        travelSpeed *= direction;

        if (direction > 0)
        {
            GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipY = false;
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.down * travelSpeed * Time.fixedDeltaTime);
        lifeTime -= Time.fixedDeltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("damager"))
        {
            other.gameObject.GetComponent<EnemyBase>().Damaged(damage);
        }
        Destroy(gameObject);
    }
}
