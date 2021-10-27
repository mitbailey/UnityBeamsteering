using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genesis : MonoBehaviour
{
    public GameObject Planet;
    // public GameObject OrbitalPlane;
    public GameObject GroundStation;
    // public GameObject Satellite;
    // public GameObject MassBody;

    float TIME_SPEED = 1f;

    // Earth's true spin rate in radians per second multiplied by SCALE_ADJ.
    // Unity freezes up when incredibly small torques are applied, therefore we have to operate in slightly larger-than-
    // real values. Divide all timescales by SCALE_ADJ to get the real-life time equivalents.
    // SCALE_ADJ should stay relatively untouched. Use TIME_SPEED for more frequent adjustments.
    const float SCALE_ADJ = 150f;
    const float EARTH_REV_SPEED_ADJ = 0.00007272205216643f * SCALE_ADJ;

    const float PLANET_REV_SPEED = 1f * EARTH_REV_SPEED_ADJ; // radians per second
    const float ORBIT_REV_SPEED = 15.5373327579f * EARTH_REV_SPEED_ADJ; // radians per second
    const float ORBIT_INCL = 51.64f; 

    const float R = 6371; // Planet radius in kilometers

    const float GS_LAT = 42.655808f * (Mathf.PI/180f);
    const float GS_LON = -71.325341f * (Mathf.PI/180f);

    // Start is called before the first frame update
    void Awake()
    {
        // Set the position of the ground station properly.
        float X = R * Mathf.Cos(GS_LAT) * Mathf.Cos(GS_LON);
        float Y = R * Mathf.Cos(GS_LAT) * Mathf.Sin(GS_LON);
        float Z = R * Mathf.Sin(GS_LAT);

        GroundStation.transform.position = new Vector3(X, Z, Y);

        // Bind the satellite to the orbital plane.
        // OrbitalPlane.AddComponent<Joint>
        // ConfigurableJoint cj = OrbitalPlane.AddComponent<ConfigurableJoint>();
        // cj.connectedBody = Satellite.GetComponent<Rigidbody>();
        // cj.xMotion = 

        // Velocity change is in radians/second.
        // Planet.GetComponent<Rigidbody>().AddTorque(new Vector3(0f, -PLANET_REV_SPEED * TIME_SPEED, 0f), ForceMode.VelocityChange);
        
        // OrbitalPlane.GetComponent<Rigidbody>().AddTorque(new Vector3(0f, -ORBIT_REV_SPEED * TIME_SPEED * (Mathf.Cos(51.64f * (Mathf.PI/180f))), ORBIT_REV_SPEED * TIME_SPEED * (Mathf.Sin(51.64f * (Mathf.PI/180f)))), ForceMode.VelocityChange);

        // Time.timeScale = 1f / SCALE_ADJ;

        Debug.Log("Simulation is running at " + TIME_SPEED * SCALE_ADJ + " seconds per second.");
    }



    // // Update is called once per frame
    // void Update()
    // {
    //     // Satellite.transform.rotation = Quaternion.Euler(Satellite.transform.eulerAngles.x, 0, 0);
    //     // Satellite.transform.LookAt(new Vector3(150000000f, 0f, 0f));
    //     // Satellite.transform.Rotate(new Vector3(1f, 0f, 0f));
        

    //     // Vector3 distance = Planet.transform.position - MassBody.transform.position;
    //     // float distanceSquared = distance.sqrMagnitude;
    //     // Vector3 forceDirection = distance.normalized;

    //     // MassBody.GetComponent<Rigidbody>().AddForce(((forceDirection * 6.674e-11f * 3f * 5.97e24f) / distanceSquared) / 3f, ForceMode.VelocityChange);
    // }
}
