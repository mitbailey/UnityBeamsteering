using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(0, 359)]
    public int rotation_offset = 0;
    int last_rotation_offset = 0;

    public Universe universe;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotation_offset != last_rotation_offset)
        {
            transform.Rotate(new Vector3(0f, -(rotation_offset - last_rotation_offset), 0f), Space.World);
            last_rotation_offset = rotation_offset;
        }
    
        transform.eulerAngles = new Vector3(0f, -Constants.EARTH_ROTATION_SPEED * universe.CurrentTime, 0f);

        // transform.Rotate(new Vector3(0f, -Constants.EARTH_ROTATION_SPEED * (180 / Mathf.PI) * Time.deltaTime * universe.sim_time_multiplier, 0f), Space.World);
    }
}
