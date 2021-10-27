using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference:
// https://www.youtube.com/watch?v=Ie5L8Nz1Ns0

public class Satellite : MonoBehaviour
{
    // const float G = 6.674e-11f; // m^3 / kg * s^2
    // const float PlanetMass = 5.972e24f; // kg

    public MassBody ReferenceBody;

    // Given Values
    float SemiMajorAxis = 6576000f; // m, 6371 km (Earth radius) + 205 km (half-height of orbit)
    float Eccentricity = 0f;
    float Inclination = 0.9f; // RADIANS!
    float LongitudeOfAscendingNode = 0f;
    float ArgumentOfPerigee = 0f;
    float MeanLongitude = 0f;

    // Calculated Values
    float MeanAnomaly;
    float MeanAngularMotion;
    float StandardGravitationalParameter;
    float EccentricAnomaly;
    float TrueAnomaly;
    float TrueAnomalyConstant;
    float DistanceFromCenter;
    float Period;

    const float SPIN_RATE = 18f; // Degrees per second (18 deg/s == 3 RPM)

    // Start is called before the first frame update
    void Start()
    {
        StandardGravitationalParameter = Constants.GRAVITATIONAL_CONSTANT * ReferenceBody.Mass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // POSITIONING
        MeanAngularMotion = Mathf.Sqrt(StandardGravitationalParameter / Mathf.Pow(SemiMajorAxis, 3));
        MeanAnomaly = MeanAngularMotion * ((Time.time * Constants.TIME_MULTIPLIER) - MeanLongitude); // <-- 0.000001f is a time-slow factor. sim goes TOO FAST!
        
        // Find the eccentric anomaly iteratively to within some error.
        float E_this = MeanAnomaly; // Initial guess for EccentricAnomaly.
        float E_last = E_this;
        float Error = 0f;
        float DesiredAccuracy = 0.1f;
        for (int i = 0; Error > DesiredAccuracy && i < 50; i++)
        {
            E_last = E_this;
            E_this = E_last - ((MeanAnomaly - E_last + Eccentricity * Mathf.Sin(E_last)) / ((-1f) + Eccentricity * Mathf.Cos(E_last)));
            Error = Mathf.Abs(E_this - E_last);
        }
        EccentricAnomaly = E_this;

        TrueAnomalyConstant = Mathf.Sqrt((1 + Eccentricity) / (1 - Eccentricity));
        TrueAnomaly = 2 * Mathf.Atan(TrueAnomalyConstant * Mathf.Tan(EccentricAnomaly / 2));

        DistanceFromCenter = SemiMajorAxis * (1 - Eccentricity * Mathf.Cos(EccentricAnomaly));

        // Now, calculate cartesian coordinates.
        float x = DistanceFromCenter * ((Mathf.Cos(LongitudeOfAscendingNode) * Mathf.Cos(ArgumentOfPerigee + TrueAnomaly)) - (Mathf.Sin(LongitudeOfAscendingNode) * Mathf.Sin(ArgumentOfPerigee + TrueAnomaly) * Mathf.Cos(Inclination)));
        float y = DistanceFromCenter * ((Mathf.Sin(LongitudeOfAscendingNode) * Mathf.Cos(ArgumentOfPerigee + TrueAnomaly)) + (Mathf.Cos(LongitudeOfAscendingNode) * Mathf.Sin(ArgumentOfPerigee + TrueAnomaly) * Mathf.Cos(Inclination)));
        float z = DistanceFromCenter * (Mathf.Sin(Inclination) * Mathf.Sin(ArgumentOfPerigee + TrueAnomaly));

        Period = Mathf.Sqrt((4 * Mathf.Pow(Mathf.PI, 2) * Mathf.Pow(DistanceFromCenter * 1000, 3))/(StandardGravitationalParameter));

        transform.position = new Vector3(x / 1000f, y / 1000f, z / 1000f) + ReferenceBody.transform.position;
    
        // ROTATION
        // transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + (18f * Time.deltaTime), 0f);
        // transform.eulerAngles = new Vector3(transform.eulerAngles.x + (18f * Time.deltaTime), 0f, 90f);
        // Debug.Log(transform.eulerAngles.x);
    }
}
