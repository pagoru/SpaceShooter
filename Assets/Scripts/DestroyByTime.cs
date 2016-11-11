using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(30);
        Destroy(gameObject);
    }
}
