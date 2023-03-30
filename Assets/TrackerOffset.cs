using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerOffset : MonoBehaviour
{

    [SerializeField] private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition += offset;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
