using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private void Update()
    {
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime); // how much we want to rotate the saw in x, y and z
    }

    // now, all that's left to do is to select our saw and in the inspector menu we just give it the tag "Trap" that we already have made for spikes
}
