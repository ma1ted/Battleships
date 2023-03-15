using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField, Range(1f, 25f)] public float noiseScale;
    [SerializeField, Range(0f, 360f)] public float noiseAngle;
    [SerializeField, Range(0f, 2f)] public float noiseSpeed;
    [SerializeField, Range(0f, 5f)] public float noiseAmplitude;

    private float lastWaveY = 0f;

    public float CalculateWaveY(Vector3 pos)
    {
        float angle = noiseAngle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * -noiseSpeed;

        return (Mathf.PerlinNoise(
                pos.x / noiseScale + Time.time * direction.x,
               pos.z / noiseScale + Time.time * direction.y
            ) - 0.5f) * noiseAmplitude;
    }

    private void Update()
    {
        float newWaveY = CalculateWaveY(transform.position);

        float deltaWaveY = newWaveY - lastWaveY;

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + deltaWaveY,
            transform.position.z
        );

        lastWaveY = newWaveY;
    }
}
