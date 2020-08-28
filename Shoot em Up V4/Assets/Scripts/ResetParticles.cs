using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetParticles : MonoBehaviour
{

    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }


    void Update()
    {

        if (ps.isStopped)
        {
            gameObject.SetActive(false);
        }

        if (gameObject.activeSelf == false)
        {
            ps.Clear();
        }

    }
}
