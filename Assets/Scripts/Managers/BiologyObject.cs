
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Establish the type of object, this will cause different reactions in the interactions.
//
// Last Update: 11.12.2022 By MauricioRB06

using UnityEngine;

namespace Managers
{
    public class BiologyObject : MonoBehaviour
    {
        
        [Header("Slide Settings")][Space(5)]
        [Tooltip("Select the type of object.")]
        [SerializeField] private  BiologyObjectType objectType;
        
        public BiologyObjectType GetObjectType() => objectType;
        
    }
}
