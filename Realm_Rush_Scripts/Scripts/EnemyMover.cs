using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Waypoint> path = new List<Waypoint>();

    [SerializeField][Range(0f, 5f)] private float speed = 1f;

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
        // InvokeRepeating(nameof(PrintWaypointName), 0, 1f); // not doing what we want
    }

    // finds a path for the enemy to traverse
    private void FindPath()
    {
        path.Clear(); // make sure we only have one path

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if (waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }

    // moves the enemy object to the beginning of the path
    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    // moves the enemy object through the path 
    private IEnumerator FollowPath()
    {
        // prints the waypoints the enemy will follow
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position; // our current waypoint
            Vector3 endPosition = waypoint.transform.position; // the waypoint we're going to
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
