using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshBakery
{
    [System.Serializable]
    public class Batch
    {
        [SerializeField]
        private List<MeshFilter> m_MeshFilters;
        [SerializeField]
        private Material m_Material;
        private List<CombineInstance> m_Combine;
        private int i;

        /// <summary>
        /// The amount of vertices gathered in the current baking cycle.
        /// </summary>
        private int m_CurrentVertexCount;

        /// <summary>
        /// 65535, the max unsigned short. This const should be left alone.
        /// </summary>
        private const ushort MAX_VERTICES = System.UInt16.MaxValue;

        /// <summary>
        /// Private, do not invoke.
        /// </summary>
        private Batch() { }

        public Batch(List<MeshFilter> meshFilters, Material material)
        {
            i = 0;
            m_MeshFilters = meshFilters;
            m_Material = material;
            m_Combine = new List<CombineInstance>();
        }

        /// <summary>
        /// Bakes the batch into a new Mesh
        /// </summary>
        public void Bake()
        {
            while (i < m_MeshFilters.Count)
            {
                if (!CombineMesh(ref i))
                    break;
            }
            SeparateMesh();
            m_MeshFilters.Clear();
        }

        /// <summary>
        /// Breaks out the List of meshes currently within m_Combine into one final mesh, m_Combine will reset itself and continue the baking 
        /// process if there are more meshes to be combined.
        /// </summary>
        private void SeparateMesh()
        {
            if (m_Combine.Count < 1)
                Debug.LogWarning("Batch did not contain anything to bake. As a result, the bake process ended.");
            else
            {
                GameObject newBatchedMesh = new GameObject("Batched Mesh", typeof(MeshFilter), typeof(MeshRenderer));
                newBatchedMesh.GetComponent<MeshFilter>().mesh.CombineMeshes(m_Combine.ToArray());
                newBatchedMesh.GetComponent<MeshRenderer>().material = m_Material;
                m_Combine.Clear();
                m_CurrentVertexCount = 0;
                if (i < m_MeshFilters.Count)
                    Bake();
            }
        }

        /// <summary>
        /// Attempts to combine a mesh to m_Combine under the assumption that the vertices count will be within the bounds of MAX_VERTICES
        /// </summary>
        /// <param name="meshIndex"></param>
        /// <returns></returns>
        private bool CombineMesh(ref int meshIndex)
        {
            CombineInstance t = new CombineInstance();
            if (m_CurrentVertexCount + (ushort)m_MeshFilters[meshIndex].mesh.vertexCount < MAX_VERTICES)
            {
                t.mesh = m_MeshFilters[meshIndex].sharedMesh;
                t.transform = m_MeshFilters[meshIndex].transform.localToWorldMatrix;
                m_MeshFilters[meshIndex].gameObject.GetComponent<MeshRenderer>().enabled = false;
                m_Combine.Add(t);

                m_CurrentVertexCount += (ushort)m_MeshFilters[meshIndex].mesh.vertexCount;
                meshIndex++;
                return true;
            }
            return false;
        }
    }
}