using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBeh : MonoBehaviour
{

    public GameObject debugObj;

    public float strength = 0.1f;
    public float range = 0.3f;

    MeshCollider col;

    MeshFilter mf;
    Mesh mesh;

    void Start() {

        mf = GetComponent<MeshFilter>();
        GetComponent<MeshCollider>();
        mesh = mf.mesh;

        col = GetComponent<MeshCollider>();

    }

    void Update() {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(r,out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    //Vector3[] verts = mesh.vertices;
                    mesh.vertices = DeformVertices(hit.point);
                    UpdateCollider();

                    //Vector3 normal = mesh.normals[i];
                    //Vector3 bump = normal * -1 * strength;

                    //verts[i] = verts[i] + bump;

                    //mesh.vertices = verts;

                    //debugObj.transform.position = ConvertToWorld(v);
                }
            }
        }

    }

    Vector3 [] DeformVertices(Vector3 worldPoint) {

        Vector3 objectPoint = ConvertToLocal(worldPoint);

        Vector3[] verts = mesh.vertices;

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Vector3 v = mesh.vertices[i];
            float distance = Vector3.Distance(objectPoint, v);

            float effect = Mathf.Clamp(map(distance,0f,range,1f,0f),0f,1f);

            verts[i] -= mesh.normals[i] * strength * effect;
        }

        return verts;

    }

    public float map(float value, float from1, float to1, float from2, float to2) {
        return value - from1 / (to1 - from1) * (to2 - from2) + from2;
    }

    Vector3 ConvertToLocal(Vector3 point) {

        return point - gameObject.transform.position;

    }

    Vector3 ConvertToWorld(Vector3 point) {

        return point + gameObject.transform.position;

    }

    void UpdateCollider() {
        DestroyImmediate(col);
        col = gameObject.AddComponent<MeshCollider>();
        col.convex = true;
        col.sharedMesh = mesh;
    }

}