
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the liquid H2O in the corresponding Beakers.
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
    public class G2H2OLiquid : MonoBehaviour
    {
        
        // Function that empties the beaker 1.
        public void EmptyBeaker1() { gameObject.GetComponent<Animator>().Play("Empty H2O Beaker 1"); }
        
        // Function that empties the beaker 2.
        public void EmptyBeaker2() { gameObject.GetComponent<Animator>().Play("Empty H2O Beaker 2"); }
        
    }
}
