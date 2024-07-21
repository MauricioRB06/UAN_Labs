
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// It allows us to Create and Destroy objects that simulate a liquid.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
//
// Last Update: 23.06.2022 By MauricioRB06

using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UniRx;

namespace Liquid
{
    public class LiquidGenerator : MonoBehaviour
    {
        
        [Space(10)]
        [Header("Liquid In")] [Space(5)]
        [Tooltip("Liquid that originates the fall (PaticleSystem).")]
        [SerializeField] private GameObject liquidStart;
        [Tooltip("Continuous flow of the liquid (PaticleSystem).")]
        [SerializeField] private GameObject liquidFlow;
        [Tooltip("Final liquid (PaticleSystem).")]
        [SerializeField] private GameObject liquidEnd;
        [Space(8)]
        [Tooltip("Final liquid (PaticleSystem).")]
        [SerializeField] private GameObject liquidStartOrigin;
        [Tooltip("Final liquid (GameObject).")]
        [SerializeField] private GameObject liquidFlowOrigin;
        [Tooltip("Final liquid (GameObject).")]
        [SerializeField] private GameObject liquidEndOrigin;
        [Space(8)]
        [Tooltip("Final liquid (GameObject).")]
        [SerializeField] private Color liquidInColor1;
        [Tooltip("Final liquid (GameObject).")]
        [SerializeField] private Color liquidInColor2;
        [Space(8)]
        [Tooltip("Final liquid (GameObject).")]
        [SerializeField] private float liquidFlowTimeout = 0.01f;
        [Tooltip("Final liquid (GameObject).")]
        [SerializeField] private float liquidEndTimeout = 0.01f;
        [Space(15)]
        
        [Header("Liquid Out System")] [Space(5)]
        [Tooltip("Liquid coming out of the nozzle (PaticleSystem).")]
        [SerializeField] private GameObject liquidOut;
        [Tooltip("Waves produced by the falling liquid (PaticleSystem).")]
        [SerializeField] public GameObject liquidWaves;
        [Space(8)]
        [Tooltip("Liquid coming out of the nozzle (PaticleSystem).")]
        [SerializeField] private GameObject liquidOutOrigin;
        [Tooltip("Waves produced by the falling liquid (PaticleSystem).")]
        [SerializeField] public GameObject liquidWavesOrigin;
        [Space(8)]
        [Tooltip("Final liquid (PaticleSystem).")]
        [SerializeField] private Color liquidOutColor1;
        [Tooltip("Final liquid (PaticleSystem).")]
        [SerializeField] private Color liquidOutColor2;
        [Tooltip("Final liquid (PaticleSystem).")]
        [SerializeField] private Color liquidWavesColor1;
        [Tooltip("Final liquid (PaticleSystem).")]
        [SerializeField] private Color liquidWavesColor2;
        [Space(15)]
        
        [Tooltip("Final liquid (GameObject).")]
        [Range(1,100)]
        [SerializeField] private int stepToChangeColor = 1;
        [Space(15)]
        
        [Tooltip("Final liquid (GameObject).")]
        [Range(1,100)]
        [SerializeField] private float liquidWavesOffset = 1;
        [Space(15)]
        
        [Tooltip("Final liquid (GameObject).")]
        [Range(1,100)]
        [SerializeField] private List<int> stepsToLiquidIn = new ();
        [Space(15)]
        
        [Tooltip("Final liquid (GameObject).")]
        [Range(1,100)]
        [SerializeField] private List<int> stepsToLiquidOut = new ();
        
        // This variables will be used to store the reference to the particle system just created.
        private ParticleSystem.MainModule _liquidStartParticleSystem;
        private ParticleSystem.MainModule _liquidFlowParticleSystem;
        private ParticleSystem.MainModule _liquidEndParticleSystem;
        private ParticleSystem.MainModule _liquidOutParticleSystem;
        private ParticleSystem.MainModule _liquidWavesParticleSystem;
        
        // Observer subscriptions and Variable initialization (Awake is executed when the object is created).
        private void Awake()
        {
            _liquidStartParticleSystem = liquidStart.gameObject.GetComponent<ParticleSystem>().main;
            _liquidFlowParticleSystem = liquidFlow.gameObject.GetComponent<ParticleSystem>().main;
            _liquidEndParticleSystem = liquidEnd.gameObject.GetComponent<ParticleSystem>().main;
            _liquidOutParticleSystem = liquidOut.gameObject.GetComponent<ParticleSystem>().main;
            _liquidWavesParticleSystem = liquidWaves.gameObject.GetComponent<ParticleSystem>().main;
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepsToLiquidIn.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    var liquidPosition = liquidWavesOrigin.transform.position;
                    
                    if (liquidWavesOrigin != null) {
                        liquidPosition = new Vector3(liquidPosition.x,
                            liquidPosition.y + liquidWavesOffset, liquidPosition.z);
                    }

                    liquidWavesOrigin.transform.position = liquidPosition;
                    LiquidIn();
                });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepsToLiquidOut.Contains(stepTrigger))
                .Subscribe(_ => { LiquidOut(); });
        }
        
        // Function to simulate the liquid falling from the beaker into the burette.
        public void LiquidIn()
        {
            if (liquidStart == null)
            {
                Debug.LogError("Object liquidStart is empty, please add one.");
            }
            else if (liquidFlow == null)
            {
                Debug.LogError("Object liquidFlow is empty, please add one.");
            }
            else if (liquidEnd == null)
            {
                Debug.LogError("Object liquidEnd is empty, please add one.");
            }
            else if (liquidStartOrigin == null)
            {
                Debug.LogError("Object liquidStartOrigin is empty, please add one.");
            }
            else if (liquidFlowOrigin == null)
            {
                Debug.LogError("Object liquidFlowOrigin is empty, please add one.");
            }
            else if (liquidEndOrigin == null)
            {
                Debug.LogError("Object liquidEndOrigin is empty, please add one.");
            }
            else
            {
                _liquidStartParticleSystem.startColor = StepManager.Instance.Counter.Value < 
                                                        stepToChangeColor ? liquidInColor1 : liquidInColor2;
                
                _liquidFlowParticleSystem.startColor = StepManager.Instance.Counter.Value <
                                                       stepToChangeColor ? liquidInColor1 : liquidInColor2;
                
                _liquidEndParticleSystem.startColor = StepManager.Instance.Counter.Value < 
                                                      stepToChangeColor ? liquidInColor1 : liquidInColor2;

                StartCoroutine(LiquidInSimulation());
            }
        }
        
        // Function to instantiate the liquid that simulates going from the burette to the beaker and changes its color.
        public void LiquidOut()
        {
            if (liquidOut == null)
            {
                Debug.LogError("Object liquidOut is empty, please add one.");
            }
            else if (liquidWaves == null)
            {
                Debug.LogError("Object liquidWaves is empty, please add one.");
            }
            else if (liquidOutOrigin == null)
            {
                Debug.LogError("Object liquidOutOrigin is empty, please add one.");
            }
            else if (liquidWavesOrigin == null)
            {
                Debug.LogError("Object liquidWavesOrigin is empty, please add one.");
            }
            else
            {
                _liquidOutParticleSystem.startColor = StepManager.Instance.Counter.Value < 
                                                        stepToChangeColor ? liquidOutColor1 : liquidOutColor2;
                
                _liquidWavesParticleSystem.startColor = StepManager.Instance.Counter.Value <
                                                       stepToChangeColor ? liquidWavesColor1 : liquidWavesColor2;
                
                Instantiate(liquidOut, liquidOutOrigin.transform.position, liquidOut.transform.rotation);
                Instantiate(liquidWaves, liquidWavesOrigin.transform.position, liquidWaves.transform.rotation);
            }
        }
        
        // Coroutine simulating the beaker liquid falling into the burette.
        private IEnumerator LiquidInSimulation()
        {
            Instantiate(liquidStart, liquidStartOrigin.transform.position, liquidStart.transform.rotation);
            yield return new WaitForSeconds(liquidFlowTimeout);
            Instantiate(liquidFlow, liquidFlowOrigin.transform.position, liquidFlow.transform.rotation);
            yield return new WaitForSeconds(liquidEndTimeout);
            Instantiate(liquidEnd, liquidEndOrigin.transform.position, liquidEnd.transform.rotation);
        }

    }
}
