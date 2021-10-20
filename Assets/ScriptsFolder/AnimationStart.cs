using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStart : MonoBehaviour
{
    [SerializeField] private float AnimationTime = 8f;
    float Times;
    private Animation AnimationSt;
    private GameObject AnimationFinished;
    private void Awake()
    {
        Movescript.OnAnimationStart = true;
        AnimationSt = this.gameObject.GetComponent<Animation>();
        AnimationSt.Play();
        AnimationFinished = GameObject.Find("Final Animation");
        AnimationFinished.SetActive(false);
    }
    void Update()
    {
        Times += Time.deltaTime;
        if (Times >= AnimationTime)
        {
            Camera CameraAnimation;
            CameraAnimation = this.gameObject.GetComponentInChildren<Camera>();
            CameraAnimation.enabled = false;
            Movescript.OnAnimationStart = false;
            Destroy(this.gameObject);
        } 
    }
}
