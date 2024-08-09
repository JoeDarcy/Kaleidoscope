using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeInstatiation : MonoBehaviour
{
    public AudioSource audioSource;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    private float clipLoudness;
    private float[] clipSampleData;

    private float minAmplitude = 10.0f;
    private float maxAmplitude = 0.0f;


    void Awake()
    {
        audioSource = GameObject.FindGameObjectWithTag("Test_Audio").GetComponent<AudioSource>();
        clipSampleData = new float[sampleDataLength];
    }

    // Update is called once per frame
    void Update()
    {
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); // I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness = Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; // ClipLoudness is what you are looking for
        }

        //string floatString = (clipLoudness * 10).ToString("0.00");
        //clipLoudness = clipLoudness * 10;

        //Debug.Log("Clip loudness: " + clipLoudness);
        //Debug.Log("Clip loudness: " + floatString);

        if (clipLoudness < minAmplitude)
        {
            minAmplitude = clipLoudness;
        }

        if (clipLoudness > maxAmplitude)
        {
            maxAmplitude = clipLoudness;
        }

        //Debug.Log("Min loudness: " + minAmplitude);
        //Debug.Log("Max loudness: " + maxAmplitude);
    }

}

