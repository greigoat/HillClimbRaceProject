using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LineToEdgeCollider : MonoBehaviour {

    public EdgeCollider2D edgeCollider2D;
    public LineRenderer lineRenderer;
    //public int pointsSize;

	// Use this for initialization
	void Start () {
		
	}

    void Calculate()
    {
        if (lineRenderer && edgeCollider2D)
        {
            Vector3[] points = new Vector3[lineRenderer.positionCount];
            Vector2[] p2ep = new Vector2[lineRenderer.positionCount];
            lineRenderer.GetPositions(points);

            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                p2ep[i] = points[i];
            }

            edgeCollider2D.points = p2ep;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
