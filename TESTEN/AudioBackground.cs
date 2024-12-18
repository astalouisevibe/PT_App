﻿using System.Diagnostics;
using Plugin.Maui.Audio;

namespace PT_App.TESTEN;

public class Audio
{
    private readonly IAudioManager _audioManager;
    private IAudioPlayer _audioPlayer;
    private bool _isPlaying = false; // Flyttet til klasse-niveau for at holde tilstand

    public Audio(IAudioManager audioManager)
    {
        _audioManager = audioManager; 
    }

    public async Task PlaySoundAsync()
    {
        try
        {
            _audioPlayer = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("pust.m4a"));
       //     _audioPlayer.Play();
       
            _isPlaying = true; // Markér som afspiller
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Fejl i afspilning af lydklip: {ex.Message}");
        }
    }

    public void StopSound()
    {
        _audioPlayer?.Stop();
        _isPlaying = false; // Markér som stoppet
    }

    public async void HandlePlaySound()
    {
        
           await PlaySoundAsync();
      
    }
}
