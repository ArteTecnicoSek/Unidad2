using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDraw : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform[] columns=new Transform[512];
    public GameObject cubePrefab;
public int band;
public float scaleSize,startScale;
    public float[] frequency = new float[8];
    public Color[] colors = new Color[8];


    [Header("Band Sample")]
    public int higherValue = 0;
    public float testValue = 0;

    Vector3 sizeDefault=Vector3.one;
     private void OnEnable() 
     {
         Vector3 columnPosition=Vector3.right;
         for(int i=0;i<columns.Length;i++)
         {
             columns[i]=Instantiate(cubePrefab,
                                    columnPosition * i,
                                    Quaternion.identity).transform;
         }    
        
    }
    void Start()
    {
        
    }

    ////esto es lo relevante
    void DrawBand()
    {
       
        frequency = AudioSampler.frequencyBand;
        higherValue = 0;
        testValue = 0;
        for (int i = 0; i < frequency.Length; i++)
        {
            if(frequency[i]>testValue)
            {
                higherValue = i;
                testValue = frequency[i];
            }
        }
         

        Shader.SetGlobalFloat("ColorFrequency", higherValue);
    }

    // Update is called once per frame
    void Update()
    {

        DrawBand();
        
         for (int i=0;i<columns.Length;i++)
         {
             sizeDefault.y=(AudioSampler.samples[i]*scaleSize)+startScale;

             columns[i].localScale=sizeDefault;

         }   
    }
}
