using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    #region Public Fields

    public float speed = 1.0f;

    [HideInInspector]
    public GameObject[] waypoints;

    #endregion Public Fields

    #region Private Fields

    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;

    #endregion Private Fields

    #region Private Methods

    private void FaceForward(Vector3 startPos, Vector3 endPos)
    {
        Vector3 dir = endPos - startPos;

        float x = dir.x;
        float y = dir.y;

        float tarAngle = (Mathf.Atan2(y, x) * 180 / Mathf.PI)-90;
        float curAngle = gameObject.transform.rotation.eulerAngles.z;
        float difAngle = tarAngle - curAngle;
        gameObject.transform.rotation = Quaternion.AngleAxis(curAngle + difAngle, Vector3.forward);
    }

    // Start is called before the first frame update
    private void Start()
    {
        lastWaypointSwitchTime = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 startPos = waypoints[currentWaypoint].transform.position;
        Vector3 endPos = waypoints[currentWaypoint + 1].transform.position;
        float pathlength = Vector3.Distance(startPos, endPos);
        float totalTimeForPath = pathlength / speed;
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        gameObject.transform.position = Vector2.Lerp(startPos, endPos, currentTimeOnPath / totalTimeForPath);

        if (gameObject.transform.position.Equals(endPos))
        {
            if (currentWaypoint < waypoints.Length - 2)
            {
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
                FaceForward(waypoints[currentWaypoint].transform.position, waypoints[currentWaypoint + 1].transform.position);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    #endregion Private Methods
}