using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Scripts.Storage
{
    public class ObjectRefences : MonoBehaviour
    {
        public static ObjectRefences Instance;
        public Dictionary<string, Material> MaterialReferenceList;
        public List<AudioClip> SoundBites;
        public List<Material> Materials;

        private void Awake()
        {
            if (Instance == null && Instance != this)
            {
                Destroy(gameObject.GetComponent<ObjectRefences>());
                Instance = this;
            }
           
            LoadMaterials();
        }

        private void LoadMaterials()
        {
            MaterialReferenceList = new Dictionary<string, Material>();
            foreach (Material material in Materials)
            {
                MaterialReferenceList.Add(material.name, material);
            }
        }
    }
}