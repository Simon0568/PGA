using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints; // we give it square brackets because we need multiple gameObjects, in this case waypoints 1 and 2
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    private void Update() // if the current waypoint and the platform have a distance smaller than .1f we switch directions
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f) //transform.position alone that we used in the second arument is just the position of the game object this script lives in
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length) // this way we know we're at the last waypoint
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed); // moving the platform frame by frame, first argument is the current position, second is the target position and the third is how far we want to move in THIS frame (don't forget that update runs every frame)
    }
}
