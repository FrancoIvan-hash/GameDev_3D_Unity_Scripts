using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField][Range(0f, 5f)] private float speed = 1f;

    private List<Node> path = new List<Node>();

    private Enemy enemy;
    private GridManager gridManager;
    private PathFinder pathFinder;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
        // InvokeRepeating(nameof(PrintWaypointName), 0, 1f); // not doing what we want
    }

    // finds a path for the enemy to traverse
    private void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathFinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        // this will stop the enemy from following the path while it finds a new one
        StopAllCoroutines();
        path.Clear(); // make sure we only have one path
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    // moves the enemy object to the beginning of the path
    private void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    // moves the enemy object through the path 
    private IEnumerator FollowPath()
    {
        // prints the waypoints the enemy will follow
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position; // our current waypoint
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            // we're always facing the waypoint we're moving towards
            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
            // yield return new WaitForSeconds(waitTime);
        }

        FinishPath();
    }

    private void FinishPath()
    {
        // don't destroy gameObject, but hide it
        enemy.StealGold();
        gameObject.SetActive(false);
    }
}
