using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(BezierCurvedLine))]
public class BezierCurvedLineEditor : Editor
{
    HashSet<KeyValuePair<int, Vector3>> controlIIDs = new HashSet<KeyValuePair<int, Vector3>>();

    void BezierDotCap(int iid, Vector3 position, Quaternion rotation, float size)
    {
        controlIIDs.Add(new KeyValuePair<int, Vector3>(iid, position));
        Handles.DotCap(iid, position, rotation, size);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BezierCurvedLine line = target as BezierCurvedLine;
    }

    void OnSceneGUI()
    {
        BezierCurvedLine line = target as BezierCurvedLine;

        if (!line.line)
            line.line = line.GetComponent<LineRenderer>();

        Handles.color = Color.black;
        EditorGUI.BeginChangeCheck();

        Vector2 startPoint = Handles.PositionHandle(line.start, Quaternion.identity);

        if (line.contorPoints.Count <= 0)
        {
            BezierCurvedLine.Point p = new BezierCurvedLine.Point();
            p.point = startPoint;
            p.dir = startPoint;
            line.contorPoints.Add(p);
        }

        Vector3[] points = new Vector3[line.contorPoints.Count];
        Vector3[] dirs = new Vector3[line.contorPoints.Count];

        for (int i = 0; i < line.contorPoints.Count; i++)
        {
            points[i] = Handles.FreeMoveHandle(
                line.contorPoints[i].point, 
                Quaternion.identity,
                HandleUtility.GetHandleSize(line.contorPoints[i].point) * 0.05f, 
                Vector3.zero,
                BezierDotCap);
            
            dirs[i] = Handles.FreeMoveHandle(
                line.contorPoints[i].dir, 
                Quaternion.identity, 
                HandleUtility.GetHandleSize(line.contorPoints[i].dir) * 0.05f, 
                Vector3.zero,
                BezierDotCap);

            Handles.DrawLine(points[i], dirs[i]);
        }

        Event e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 0 && e.shift)
        {
            Undo.RecordObject(target, target.ToString());
            Vector2 mouse = e.mousePosition;
            mouse.y = Camera.current.pixelHeight - mouse.y;
            BezierCurvedLine.Point p = new BezierCurvedLine.Point();

            p.point = Camera.current.ScreenToWorldPoint(mouse);
            p.point.z = 0;
            p.dir = (line.contorPoints[line.contorPoints.Count - 1].point + p.point) / 2;
            p.dir.z = 0;
            line.contorPoints.Add(p);
        }

        if (e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.Delete)
        {
            int index = -1;
            foreach (var iid in controlIIDs)
            {
                if (GUIUtility.keyboardControl == iid.Key)
                {
                    for (int i = 0; i < line.contorPoints.Count; i++)
                    {
                        if (line.contorPoints[i].dir == iid.Value ||
                            line.contorPoints[i].point == iid.Value)
                            index = i;
                    }
                }
            }

            if (index != -1)
            {
                Undo.RecordObject(target, target.ToString());
                line.contorPoints.RemoveAt(index);
            }
        }

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, target.ToString());

            for (int i = 0; i < line.contorPoints.Count; i++)
            {
                line.contorPoints[i].point = points[i];
                line.contorPoints[i].dir = dirs[i];
            }

            line.start = startPoint;
            line.Update();
        }
    }
}
