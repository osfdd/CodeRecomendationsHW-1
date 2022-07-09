using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    private bool _isAlarmWorking = false;
    private bool _isOnTrigger = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Crook")
        {
            _isOnTrigger = true;

            if (_isAlarmWorking == false)
            {
                _isAlarmWorking = true; StopAllCoroutines();

                var playAlarmSound = StartCoroutine(PlaySound());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Crook")
        {
            _isOnTrigger = false;
            _isAlarmWorking = false;
            StopAllCoroutines();
            var playAlarmSound = StartCoroutine(PlaySound());
        }
    }

    private IEnumerator PlaySound()
    {
        var AudioSource = GetComponent<AudioSource>();

        while (AudioSource.volume <= 1 && AudioSource.volume >= 0)
        {
            AudioSource.Play();

            if (_isOnTrigger == true)
                AudioSource.volume += 0.05f;
            else
                AudioSource.volume -= 0.05f;

            yield return new WaitForSeconds(3f);
        }
    }
}
