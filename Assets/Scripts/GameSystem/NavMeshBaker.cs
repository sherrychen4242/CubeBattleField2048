using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    public NavMeshSurface[] navMeshSurfaces;
   
    public void BuildNavMesh()
    {
        navMeshSurfaces = FindObjectsOfType<NavMeshSurface>();

        for (int i = 0; i < navMeshSurfaces.Length; i++)
        {
            if (navMeshSurfaces[i].gameObject.CompareTag("Ground"))
            {
                navMeshSurfaces[i].BuildNavMesh();
            }
            
        }
    }

    public void RemoveNavMesh()
    {
        navMeshSurfaces = FindObjectsOfType<NavMeshSurface>();

        for (int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].enabled = false;
        }

        NavMesh.RemoveAllNavMeshData();
    }
}
