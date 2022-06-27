
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the material behavior of NaCl.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
//
// Last Update: 23.06.2022 By MauricioRB06

using UnityEngine;

namespace Chemistry.G2
{
    public class G2NaClMat : MonoBehaviour
    {
        // Variable to control which animation to play.
        private int _materialAnimation;
        
        // Function that performs the NaCl filling animation.
        public void NaClFillMaterial()
        {
            switch (_materialAnimation)
            {
                case 0:
                    gameObject.GetComponent<Animator>().Play("NaCl Material Fill 1");
                    _materialAnimation++;
                    break;
                case 1:
                    gameObject.GetComponent<Animator>().Play("NaCl Material Fill 2");
                    _materialAnimation++;
                    break;
                case 2:
                    gameObject.GetComponent<Animator>().Play("NaCl Material Fill 3");
                    _materialAnimation++;
                    break;
                case 3:
                    gameObject.GetComponent<Animator>().Play("NaCl Material Empty");
                    break;
            }
        }
        
    }
}
