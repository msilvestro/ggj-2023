using UnityEngine;

namespace GGJ23
{
    [System.Serializable]
    public class FeatureCreepData
    {
        public string title;
        public string[] descriptions;

        public string GetRandomDescription()
        {
            return descriptions[Random.Range(0, descriptions.Length)];
        }
    }

    [CreateAssetMenu(
        fileName = "Feature Creep Collection",
        menuName = "GGJ23/Feature Creep Collection",
        order = 0
    )]
    public class FeatureCreepCollection : ScriptableObject
    {
        public FeatureCreepData[] featureCreepData;

        public FeatureCreepData GetRandomFeatureCreepData()
        {
            return featureCreepData[Random.Range(0, featureCreepData.Length)];
        }

        public FeatureCreepData GetFromTitle(string title)
        {
            foreach (FeatureCreepData featureCreepDatum in featureCreepData)
            {
                if (featureCreepDatum.title == title)
                {
                    return featureCreepDatum;
                }
            }
            return null;
        }
    }
}
