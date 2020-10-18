using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    #region Public Fields

    public GameObject enemyPrefab;
    public GameObject[] waypoints;

    #endregion Public Fields

    #region Private Methods

    // Start is called before the first frame update
    private void Start()
    {
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
        newEnemy.transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    #endregion Private Methods
}