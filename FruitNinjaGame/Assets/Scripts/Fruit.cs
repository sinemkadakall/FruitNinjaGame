using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject slice;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceParticleSystem;
    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void Slice(Vector3 direction,Vector3 position,float force)
    {
        FindObjectOfType<GameManager>().IncreaseScore();

        whole.SetActive(false);
        slice.SetActive(true);

        fruitCollider.enabled = false;
        juiceParticleSystem.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        slice.transform.rotation = Quaternion.Euler(0f,0f,angle);

        Rigidbody[] slices=slice.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidbody.velocity;
            slice.AddForceAtPosition(direction*force,position,ForceMode.Impulse);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade=other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }

    



}
