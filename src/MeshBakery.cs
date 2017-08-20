using Overtop.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overtop.Utility
{
    public class MeshBakery : MonoBehaviour
    {
        public bool m_LaunchFromEditor = false;
        [Tooltip("Batch Ingredients consist of a single Mesh and Material.")]
        public List<BatchIngredient> m_BatchIngredients;
        [SerializeField]
        [Tooltip("Batches created")]
        private List<Batch> m_Batches;

        /// <summary>
        /// Compiled List of all the MeshFilters within the children of this MeshBakery
        /// </summary>
        public List<MeshFilter> m_MeshFilters;

        private float m_Now;
        [SerializeField]
        [Tooltip("Time to complete in Milliseconds")]
        private float m_TTC;

        private System.Diagnostics.Stopwatch m_StopWatch;

        private void Awake()
        {
            m_Batches = new List<Batch>();
            m_StopWatch = new System.Diagnostics.Stopwatch();
        }

        private void Start()
        {
            if(m_BatchIngredients != null && m_LaunchFromEditor)
                Init();
        }

        /// <summary>
        /// Searches the m_MeshFilter List for all MeshFilters that have the specified name and material assigned.
        /// </summary>
        /// <param name="meshName">The name of the Mesh to search for</param>
        /// <param name="meshMaterial">The Material to search for</param>
        /// <returns>List of MeshFilters</returns>
        private List<MeshFilter> SearchForArbitraryMeshToBatch(string meshName, Material meshMaterial)
        {
            return m_MeshFilters.FindAll((meshFilter) => {
                /**
                 * NOTE: If anything you may need to do a String.Replace(" (Instance)", "") on either 
                 * meshFilter.name or meshFilter.sharedMesh.name because reasons...the meshName parameter
                 * will always be correct though.
                 */
                return (meshFilter.name == meshName || meshFilter.sharedMesh.name == meshName)
                    && meshFilter.gameObject.GetComponent<MeshRenderer>().sharedMaterial.Equals(meshMaterial);
            });
        }

        /// <summary>
        /// Manually kick off the baking process when instantiating meshes or prefabs within code instead of the editor.
        /// </summary>
        public void Init()
        {
            m_StopWatch.Start();
            m_MeshFilters = new List<MeshFilter>(gameObject.GetComponentsInChildren<MeshFilter>());
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
            //m_MeshFilters.Clear();
            m_StopWatch.Stop();
            m_TTC = m_StopWatch.Elapsed.Milliseconds;
        }
    }
}