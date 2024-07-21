
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the Volumetric Ball Liquid 3.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
//
// Last Update: 23.06.2022 By MauricioRB06

using System.Collections.Generic;
using Managers;
using UniRx;
using UnityEngine;

namespace Chemistry.G2
{
    public class G2VolumetricBallLiquid3 : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Step Settings")]
        [Space(5)]
        [Tooltip("Set the step at which the liquid will play the filling animation.")]
        [SerializeField] private List<int> stepsToFill;
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            StepManager.Instance.Counter
                .Where(stepTrigger => stepsToFill.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    switch (StepManager.Instance.Counter.Value)
                    {
                        case 42: gameObject.GetComponent<Animator>().Play("VolumetricBall Liquid 3 Fill 1");
                            break;
                        case 44: gameObject.GetComponent<Animator>().Play("VolumetricBall Liquid 3 Fill 2");
                            break;
                        case 46: gameObject.GetComponent<Animator>().Play("VolumetricBall Liquid 3 Fill 3");
                            break;
                        case 48: gameObject.GetComponent<Animator>().Play("VolumetricBall Liquid 3 Fill 4");
                            break;
                    }
                });
        }
        
    }
}
