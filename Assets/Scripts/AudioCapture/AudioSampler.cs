using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSampler : MonoBehaviour
{ 
    [SerializeField]
    bool liveAudio = false;
    [SerializeField]
   
    public static float[] samples = new float[512];
    public static float[] frequencyBand = new float[8];
    public static float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];
    AudioSource audioSource;

    private void Awake() {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
     
        audioSource.Play(); // Play the audio source!
    }

    void Update() {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
    }

    void BandBuffer() {
        for ( int g = 0; g < 8; g++ ) {
            if ( frequencyBand[g] > bandBuffer[g] ) {
                bandBuffer[g] = frequencyBand[g];
                bufferDecrease[g] = .005f;
            }
            if ( frequencyBand[g] < bandBuffer[g] ) {
                bandBuffer[g] -= bufferDecrease[g];
                bufferDecrease[g] *= 1.2f;
            }
        }
    }

    void GetSpectrumAudioSource() {
        audioSource.GetSpectrumData( samples, 0, FFTWindow.Blackman );
    }

    void MakeFrequencyBands() {
        /*
         *  22050 / 512 - 43hz per sample
         *  
         *  20 - 60hz
         *  60 - 250hz
         *  250 - 500hz
         *  2000 - 4000hz
         *  4000 - 6000hz
         *  6000 - 20000hz
         *  
         *  0 - 2 = 86hz
         *  1 - 4 = 172hz - 87-258
         *  2 - 8 = 344hz - 259-602
         *  3 - 16 = 688hz - 603-1290
         *  4 - 32 = 1376hz - 1291-2666
         *  5 - 64 = 2752hz - 2667-5418
         *  6 - 128 = 5504hz - 5419-10922
         *  7 - 256 = 11008hz - 10923-21930
         *  510
         */
        int count = 0;
        for ( int i = 0; i < 8; i++ ) {
            float average = 0;
            int sampleCount = (int)Mathf.Pow( 2, i ) * 2;
            if ( i == 7 ) {
                sampleCount += 2;
            }
            for ( int j = 0; j < sampleCount; j++ ) {
                average += samples[count] * ( count + 1 );
                count++;
            }
            average /= count;
            frequencyBand[i] = average * 10;
        }
    }
}
