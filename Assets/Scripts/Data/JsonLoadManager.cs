using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chapter.Data
{
    public class JsonLoader
    {
        public static Dictionary<string, T> LoadAsDictionary<T>(string fileName, Func<T, string> keySelector)
        {
            TextAsset json = Resources.Load<TextAsset>($"JsonFiles/{fileName}");
            if (json == null)
            {
                Debug.LogError($"JSON 파일이 없습니다: {fileName}");
                return new Dictionary<string, T>();
            }

            var wrapper = JsonUtility.FromJson<DataListWrapper<T>>(json.text);
            return wrapper.items.ToDictionary(keySelector);

        }
    }

    public class DataListWrapper<T>
    {
        public T[] items;
    }
}
