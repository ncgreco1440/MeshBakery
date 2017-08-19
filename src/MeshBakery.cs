using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshBakery
{
    public class MeshBakery : MonoBehaviour
    {
        [Tooltip("Batch Ingredients consist of a single Mesh and Material.")]
        public List<BatchIngredient> m_BatchIngredients;
        [SerializeField]
        [Tooltip("Batches created")]
        private List<Batch> m_Batches;

        /// <summary>
        /// Compiled List of all the MeshFilters within the children of this MeshBakery
        /// </summary>
        private List<MeshFilter> m_MeshFilters;

        private float m_Now;
        [SerializeField]
        [Tooltip("Time to complete in Milliseconds")]
        private float m_TTC;

        private System.Diagnostics.Stopwatch m_StopWatch;

        private void Awake()
        {
            m_Batches = new List<Batch>();
            m_MeshFilters = new List<MeshFilter>(gameObject.GetComponentsInChildren<MeshFilter>());
            m_StopWatch = new System.Diagnostics.Stopwatch();
        }

        private void Start()
        {
            m_StopWatch.Start();
            if (m_BatchIngredients.Count == 0)
                Debug.LogWarning("You have a MeshCombiner script attached to " + gameObject.ToString() + " but you haven't specified any Batch Ingredients. Because of this, no batches have been made.");
            else
            {
                for (int y = 0; y < m_BatchIngredients.Count; y++)
                {
                    m_Batches.Add(new Batch(SearchForArbitraryMeshToBatch(m_BatchIngredients[y].Mesh.name, m_BatchIngredients[y].Material), m_BatchIngredients[y].Material));
                    m_Batches[y].Bake();
                }
            }
            m_MeshFilters.Clear();
            m_StopWatch.Stop();
            m_TTC = m_StopWatch.Elapsed.Milliseconds;
        }

        /// <summary>
        /// Searches the m_MeshFilter List for all MeshFilters that have the specified name and material assigned.
        /// </summary>
        /// <param name="meshName"></param>
        /// <param name="meshMaterial"></param>
        /// <returns></returns>
        private List<MeshFilter> SearchForArbitraryMeshToBatch(string meshName, Material meshMaterial)
        {
            return m_MeshFilters.FindAll((meshFilter) => {
                return meshFilter.name == meshName && meshFilter.gameObject.GetComponent<MeshRenderer>().sharedMaterial.Equals(meshMaterial);
            });
        }
    }
}