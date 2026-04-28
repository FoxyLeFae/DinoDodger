using UnityEngine;

public class IceCream : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}