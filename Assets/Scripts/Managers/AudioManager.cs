
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Sets the behavior of the Sound manager.
//
// Last Update: 11.12.2022 By MauricioRB06

using UniRx;
using UnityEngine;

namespace Managers
{
    
    // Component required for this script work.
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        
        // Singleton Pattern Instance.
        public static AudioManager Instance { get; private set; }
        
        [Header("Sfx Settings")][Space(5)]
        [Tooltip("Place the sound effect for the buttons here.")]
        [SerializeField] private AudioClip buttonSfx;
        [Tooltip("Place here the sound effect for when a guide is finished.")]
        [SerializeField] private AudioClip finishGuide;
        
        // Used to store the reference to the AudioSource.
        private AudioSource _audioSource;
        
        // Singleton Pattern implementation.
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            _audioSource = GetComponent<AudioSource>();
        }
        
        private void Start()
        {
            BiologyStepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == BiologyStepManager.instance.Steps)
                .Subscribe(_ => { FinishGuide(); });
        }
        
        public void UIButton() { _audioSource.PlayOneShot(buttonSfx); }
        private void FinishGuide() { _audioSource.PlayOneShot(finishGuide); }
        
        public void PlaySfx(AudioClip sfx) { _audioSource.PlayOneShot(sfx); }
        
    }
}
