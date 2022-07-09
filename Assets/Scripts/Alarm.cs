using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    private bool _isAlarmWorking = false;
    private bool _isOnTrigger = true;
    private Coroutine _playAlarmSound;
    private AudioSource _audioSource;
    private WaitForSeconds sleepTime;

    private void Start()
    {
        sleepTime = new WaitForSeconds(3f);
        _playAlarmSound = null;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Crook>())
        {
            _isOnTrigger = true;

            if (_isAlarmWorking == false)
            {
                _isAlarmWorking = true;

                if (_playAlarmSound != null)
                    StopCoroutine(_playAlarmSound);

                _playAlarmSound = StartCoroutine(PlaySound());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Crook>())
        {
            _isOnTrigger = false;
            _isAlarmWorking = false;

            if (_playAlarmSound != null)
                StopCoroutine(_playAlarmSound);

            _playAlarmSound = StartCoroutine(PlaySound());
        }
    }

    private IEnumerator PlaySound()
    {
        float volumeStep = 0.05f;

        while (_audioSource.volume <= 1 && _audioSource.volume >= 0)
        {
            _audioSource.Play();

            if (_isOnTrigger == true && _audioSource.volume + volumeStep <= 1)
                _audioSource.volume += volumeStep;
            else if (_audioSource.volume - volumeStep >= 0)
                _audioSource.volume -= volumeStep;
            else
                break;

            yield return sleepTime;
        }
    }
}
