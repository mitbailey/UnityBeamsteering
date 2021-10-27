using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, -Constants.EARTH_ROTATION_SPEED * (180 / Mathf.PI) * Time.deltaTime * Constants.TIME_MULTIPLIER, 0f), Space.World);
    }
}
