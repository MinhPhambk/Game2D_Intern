using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    public float destroyTime = 1f;
    public Vector3 randomIntensity = new(0.25f, 0f, 0f);

    void Start()
    {
        Destroy(gameObject, destroyTime);

        transform.localPosition += new Vector3(Random.Range(-randomIntensity.x, randomIntensity.x),
            Random.Range(-randomIntensity.y, randomIntensity.y),
            Random.Range(-randomIntensity.z, randomIntensity.z));
    }
}
