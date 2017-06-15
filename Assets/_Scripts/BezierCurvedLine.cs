using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class BezierCurvedLine : MonoBehaviour
{
    public LineRenderer line;
    public int resolution;
    public List<Point> contorPoints;
    public Vector3 start;

    /// <summary>
    ///  Describes a point on the bezier curve
    /// </summary>
    [System.Serializable]
    public class Point
    {
        public Vector3 point;
        public Vector3 dir;
    }

    // Use this for initialization

    void Start ()
    {
        line = GetComponent<LineRenderer>();
    }
	

    void DrawBezierCurve()
    {
        line.positionCount = contorPoints.Count * resolution;
        for (int p = 0; p < contorPoints.Count; p++)
        {
            // How much actual points on bezier line
            for (int i = 0; i < resolution; i++)
            {
                // Calculate per point and calculater linear interpolation
                var t = (float)i / resolution;
                var tt = t * t;
                var u = (1 - t);
                var uu = u * u;
                var p0 = Vector3.zero;

                if (p == 0)
                {
                    p0 = start;
                }
                else
                    p0 = contorPoints[p - 1].point;
                var p1 = contorPoints[p].dir;
                var p2 = contorPoints[p].point;
                contorPoints[p].dir = p1;

                // Calculate the bezier curve
                var x = uu * p0.x + 2 * u * t * p1.x + tt * p2.x;
                var y = uu * p0.y + 2 * u * t * p1.y + tt * p2.y;
                line.SetPosition(i + (p * resolution), new Vector3(x, y));
            }
        }
    }
    //
	// Update is called once per frame
	public void Update ()
    {
        DrawBezierCurve();
    }
}
