using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour, ResettableGameobject
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
    private bool fellDown = false;
    private Rigidbody2D rb;

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

        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if(rb != null)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }

        fellDown = false;
        playerJumpedOn = false;
        
        transform.position = currentPoint.Current.position;
    }

    void Update()
    {
        if(fellDown || currentPoint == null || currentPoint.Current == null || (startWhenPlayerJumpsOn && !playerJumpedOn))
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
                rb.bodyType = RigidbodyType2D.Dynamic;
                fellDown = true;
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

    public void Reset()
    {
        if (startWhenPlayerJumpsOn)
        {
            Start();
        }
    }
}
