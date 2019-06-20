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
    public bool startWhenPlayerJumpsOn = false;
    public bool fallDownAtEnd = false;

    private IEnumerator<Transform> currentPoint;
    private GameObject target = null;
    private Vector3 offset;
    private bool playerJumpedOn = false;

    void Start()
    {
        if (path == null)
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
        if(currentPoint == null || currentPoint.Current == null || (startWhenPlayerJumpsOn && !playerJumpedOn))
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
            if (fallDownAtEnd && currentPoint.Current == path.points[path.points.Length - 1])
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                return;
            }
            currentPoint.MoveNext();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            target = col.gameObject;
            offset = target.transform.position - transform.position;
            playerJumpedOn = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            target = null;
        }
    }

    void LateUpdate()
    {
        if(target != null)
        {
            target.transform.position = transform.position + offset;
        }
    }
}
