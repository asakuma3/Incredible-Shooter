using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asakuma
{
    public class Explosion : MonoBehaviour
    {
        ParticleSystem explosionParticle;
        void Start()
        {
            explosionParticle = GetComponent<ParticleSystem>();
        }
        private void Update()
        {
            var particleSystem = GetComponent<ParticleSystem>();
            Destroy(gameObject, particleSystem.main.duration);
        }
    }
}