using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private int damage;
    private float knockback;

    // this list keeps track of the thing we already collided with so we don't deal damage in the same swing multiple times
    private List<Collider> alreadyCollidedWith = new List<Collider>();

    // because we're turning on and off this gameobject whenever we start or stop a swing 
    private void OnEnable()
    {
        // reset the list
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        // make sure we don't deal damage to ourselves
        if (other == myCollider) { return; }

        // check what we haven't collided with the other gameObject/collider
        if (alreadyCollidedWith.Contains(other)) { return; }

        // add to list if we haven't collided with other yet
        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }

        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
        }
    }

    public void SetAttack(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
}
