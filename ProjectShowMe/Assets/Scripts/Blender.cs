using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Humans;
public class Blender : MonoBehaviour
{
    private Animator BlendAnimator { get; set; }

    private void Start()
    {
        if (!BlendAnimator)
            BlendAnimator = GetComponent<Animator>();

        EventManager<int>.AddHandler(EVENT.gameUpdateEvent, StartBlending);
        EventManager<List<HumanType>>.AddHandler(EVENT.blendEvent, StopBlending);
    }

    public void StartBlending(int ready)
    {
        BlendAnimator.SetBool("Filled", true);
    }

    public void StopBlending(List<HumanType> none)
    {
        BlendAnimator.SetBool("Filled", false);
    }
}
