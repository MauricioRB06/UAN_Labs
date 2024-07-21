
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the Microspatula.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
//
// Last Update: 23.06.2022 By MauricioRB06

using Managers;
using UnityEngine;

namespace Chemistry.G2
{
    public class G2Microspatula : MonoBehaviour
    {
        
        // Function that activates the microspatula interaction animation.
        public void Interaction() { gameObject.GetComponent<Animator>().Play("Microspatula Interact"); }
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation ).
        private void AnimationFinishTrigger() { StepManager.Instance.UpdateCounter(); }
        
    }
}
