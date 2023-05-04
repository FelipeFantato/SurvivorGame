using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projctle : MonoBehaviour
{

    [SerializeField] private float bulletVelocity;
    [SerializeField] private AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bulletVelocity *Time.deltaTime);
    }
}
