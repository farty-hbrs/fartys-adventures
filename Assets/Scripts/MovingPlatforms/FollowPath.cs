using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public enum FollowType
    {
        MoveTowards,
        Lerp
    }

    public FollowType type = FollowType.MoveTowards;
    public PathDefinition path;
    public float speed = 5;
    public float maxDistanceToGoal = .1f;
    private IEnumerator<Transform> currentPoint;

    void Start()
    {
        if(path == null)
        {
            return;
        }

        currentPoint = path.GetPathEnumerator();
        currentPoint.MoveNext();

        if(currentPoint.Current == null)
        {
            return;
        }

        transform.position = currentPoint.Current.position;
    }

    void Update()
    {
        if(currentPoint == null || currentPoint.Current == null)
        {
            return;
        }

        if(type == FollowType.MoveTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.Current.position, Time.deltaTime * speed);
        }
        else if (type == FollowType.Lerp)
        {
            transform.position = Vector3.Lerp(transform.position, currentPoint.Current.position, Time.deltaTime * speed);
        }

        float distanceSquared = (transform.position - currentPoint.Current.position).sqrMagnitude;
        if(distanceSquared < maxDistanceToGoal * maxDistanceToGoal)
        {
            currentPoint.MoveNext();
        }
    }
}
