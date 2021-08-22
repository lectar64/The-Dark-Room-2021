using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycollider : MonoBehaviour
{
    //ParticleSystem KeyParticle;
    private Casting GetAnimator;// ObtenerAnimacion
 
    private void OnCollisionEnter(Collision collision)
    {  if (collision.gameObject.name == "Key")
        {
            GetAnimator = FindObjectOfType<Casting>().GetComponent<Casting>();
            Casting.Llave = true;
            GetAnimator.OnBoxhand = false;
            Destroy(collision.gameObject); 
        }
        
    }
}
