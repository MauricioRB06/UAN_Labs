
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the liquid with NaCl.
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
    public class G2NaClLiquid : MonoBehaviour
    {
        
        // Function that fills the liquid with NaCl.
        public void FillNaCl() { gameObject.GetComponent<Animator>().Play("Fill Liquid NaCl"); }
        
        // Function that empties the liquid with NaCl.
        public void EmptyNaCl() { gameObject.GetComponent<Animator>().Play("Empty Liquid NaCl"); }

    }
}
