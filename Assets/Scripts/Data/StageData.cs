using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Data
{
    [System.Serializable]
    public class StageData
    {
        public int stageNumber;
    }

    [System.Serializable]
    public class StageDataList
    {
        public List<StageData> stages = new List<StageData>();
    }

}