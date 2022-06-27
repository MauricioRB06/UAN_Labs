
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Control the changes that are made to the application language.
//
// Documentation and References:
//
// Scripting Localization: https://docs.unity3d.com/Packages/com.unity.localization@1.0/manual/Scripting.html
//
// Last Update: 23.06.2022 By MauricioRB06

using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Managers
{
    public class Settings : MonoBehaviour
    {
        
        // Set Default Language to Start Game ( Spanish ).
        private void Awake()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        }
        
        // Set Language App to English.
        public void LanguageEnglish()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        }
        
        // Set Language App to Spanish.
        public void LanguageSpanish()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        }
        
    }
}
