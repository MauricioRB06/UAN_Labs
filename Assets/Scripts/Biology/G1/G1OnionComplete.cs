
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// It establishes the behavior of the onion.
//
// Documentation and References:
//
// C# Interfaces: https://docs.microsoft.com/en-us/dotnet/csharp/language
//
// Last Update: 11.12.2022 By MauricioRB06

using Interfaces;
using Managers;
using ModelShark;
using UnityEngine;

namespace Biology.G1
{
    // Components required for this Script to work.
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(TooltipTrigger))]
    public class G1OnionComplete : MonoBehaviour, IInteractable
    {
        public void Interaction()
        {
            BiologyStepManager.instance.UpdateCounter();
            Destroy(gameObject);
        }
        
    }
}
