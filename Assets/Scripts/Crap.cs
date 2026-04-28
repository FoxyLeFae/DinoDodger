using UnityEngine;

public class Crap : MonoBehaviour

{
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}