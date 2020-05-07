using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomIncrease : MonoBehaviour
{
    public ParticleSystem mushroom;

    void Start()
    {
        mushroom = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystemEmissionType.Time = 10;
    }
}
