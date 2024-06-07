using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Day_at_night : MonoBehaviour
{
    [SerializeField] Gradient directionLightGradient;
    [SerializeField] Gradient ambienLightGradient;
    [SerializeField, Range(1,3600)] float TimeinSecond=60;
    [SerializeField, Range(0, 1f)] float TimeCheck;
    [SerializeField] Light sun;
    [SerializeField] Light moon;
    Vector3 defaultVect;
    void Start()
    {
        defaultVect = sun.transform.localEulerAngles;
        defaultVect = moon.transform.localEulerAngles;
    }

 
    void Update()
    {
        if(Application.isPlaying) 
            TimeCheck += Time.deltaTime / TimeinSecond;

        if (TimeCheck > 1f)
            TimeCheck = 0f;

        sun.color =directionLightGradient.Evaluate(TimeCheck);
        moon.color = directionLightGradient.Evaluate(TimeCheck);
        RenderSettings.ambientLight = ambienLightGradient.Evaluate(TimeCheck);

        sun.transform.localEulerAngles = new Vector3(360f * TimeCheck - 90, defaultVect.x, defaultVect.z);
        moon.transform.localEulerAngles = new Vector3(360f * TimeCheck + 90, defaultVect.x, defaultVect.z);
    }
}
