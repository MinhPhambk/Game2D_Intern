using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class HitVFX : MonoBehaviour
{
    private ParticleSystem ps;

    [System.Obsolete]
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        Destroy(gameObject, ps.duration);
    }
}
