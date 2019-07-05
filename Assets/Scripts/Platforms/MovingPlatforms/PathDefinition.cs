using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Thanks to 3DBuzz: https://www.youtube.com/watch?v=ZkGPoZOQE5Q&t=2257s

public class PathDefinition : MonoBehaviour
{
    public Transform[] points;

    public IEnumerator<Transform> GetPathEnumerator()
    {
        if (points == null || points.Length < 2)
        {
            yield break;
        }

        int direction = 1;
        int i = 0;
        while (true)
        {
            yield return points[i];

            if(points.Length == 1)
            {
                continue;
            }

            if(i <= 0)
            {
                direction = 1;
            }
            else if (i >= points.Length - 1)
            {
                direction = -1;
            }
            i += direction;
        }
    }



    public void OnDrawGizmos()
    {
        if(points == null || points.Length < 2)
        {
            return;
        }

        for (int i = 1; i < points.Length; i++)
        {
            Gizmos.DrawLine(points[i-1].position, points[i].position);
        }
    }
}
