using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSizer : MonoBehaviour
{
    float x, y, z, rs;
    public static bool Condition;
    public AudioClip Clip;

    private void Awake()
    {
    }

    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }


    void Update()
    {
        if (Movescript.Flash != true && Condition == false)
        {
            if (Input.GetKeyDown(KeyCode.F) || Movescript.OnPlayermove && Movescript.OnPlayermove != true)
            {
                AudioSource SoundConds;
                SoundConds = gameObject.GetComponent<AudioSource>();
                Condition = true;
                SoundConds.PlayOneShot(Clip);
            }
        }

        if (Condition)
        {
            OnFlashLight();
        }
    }

    public void OnFlashLight() // Codigo desordenado pero funcional
    {
        if (Condition)
        {
            rs += Time.deltaTime;
            transform.localScale = new Vector3(x, y, z);
            if (x < 0.2875451f && y < 0.2875451f && z < 0.2875451f && rs < 3)
            {
                x += Time.deltaTime;
                y += Time.deltaTime;
                z += Time.deltaTime;
            }
            else if (x > 0.2875451f && y > 0.2875451f && z > 0.2875451f && rs < 3)
            {
                x = 0.2875451f;
                y = 0.2875451f;
                z = 0.2875451f;
            }
            else if (rs > 3 && x >= 0 && y >= 0 && z >= 0)
            {
                x -= Time.deltaTime;
                y -= Time.deltaTime;
                z -= Time.deltaTime;
            }
            else if (rs > 3 && x <= 0 && y <= 0 && z <= 0)
            {
                x = 0;
                y = 0;
                z = 0;
                rs = 0;
                transform.localScale = new Vector3(x, y, z);
                Condition = false;
            }
        }
    }
}