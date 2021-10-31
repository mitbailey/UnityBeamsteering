using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe : MonoBehaviour
{
    [Range(0, 100)]
    public int unity_time_scale = 1;

    [Range(0, 500)]
    public int sim_time_multiplier = 1;

    public float CurrentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = unity_time_scale;
        CurrentTime += sim_time_multiplier * Time.deltaTime;
    }
}
