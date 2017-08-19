using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshBakery
{
    /// <summary>
    /// A BatchIngredient is an object to be defined within the Unity Editor, MeshCombiner will take
    /// a List of the given BatchIngredients and create Batch objects from them.
    /// </summary>
    [System.Serializable]
    public class BatchIngredient
    {
        [Tooltip("The Mesh attached to this BatchIngredient.")]
        public Mesh Mesh;
        [Tooltip("The Material attached to this BatchIngredient.")]
        public Material Material;

        /// <summary>
        /// Private, do not invoke
        /// </summary>
        private BatchIngredient() { }

        public BatchIngredient(Mesh mesh, Material material)
        {
            Mesh = mesh;
            Material = material;
        }
    }
}