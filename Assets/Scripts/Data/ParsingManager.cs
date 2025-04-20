using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Data
{
#if UNITY_EDITOR
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using Unity.EditorCoroutines.Editor;
    using Unity.Mathematics;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.Rendering;


    namespace Chapter.Data
    {
        public partial class SheetParsing : EditorWindow
        {
            string sheetAPIurl = "https://script.google.com/macros/s/AKfycbzofCyJ8eZxHUulGf0s1TSCCGjgJA3IYu0MTELheaFTRJm7cfbvYI1APAANui0qGwWXrQ/exec";
            string sheeturl = "https://docs.google.com/spreadsheets/d/17Gff8xN4jhsmfIsua5yDj9HfptYavuPgQovEG4aS16U/edit?gid=0#gid=0";

            private List<SheetData> sheets = new List<SheetData>();
            private int selectedSheetIndex = 0;
            private bool isFetching = false;

            [MenuItem("Tools/Google Sheet Parsing Tool")]
            public static void ShowWindow()
            {
                EditorWindow window = GetWindow(typeof(SheetParsing));
                window.titleContent = new GUIContent("Google Sheet Parser");
                window.maxSize = new Vector2(600, 400);
                window.minSize = new Vector2(600, 400);
            }

            private void OnGUI()
            {
                GUILayout.Space(10);
                if (isFetching)
                {
                    EditorGUILayout.LabelField("Fetching data...");
                }
                else
                {
                    if (sheets.Count > 0)
                    {
                        string[] sheetNames = sheets.Select(s => s.sheetName).ToArray();
                        selectedSheetIndex = EditorGUILayout.Popup("Select Sheet", selectedSheetIndex, sheetNames);
                    }
                    else
                    {
                        EditorGUILayout.LabelField("No sheets found.");
                    }

                    GUILayout.Space(20);
                    if (GUILayout.Button("Fetch Sheets Data", GUILayout.Height(40)))
                    {
                        EditorCoroutineUtility.StartCoroutine(FetchSheetsData(), this);
                    }
                }

                GUILayout.Space(30);
                if (GUILayout.Button("Parse Selected Sheet and Create Class", GUILayout.Height(40)))
                {
                    if (sheets.Count > 0)
                    {
                        ParseSelectedSheet();
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Error", "Please fetch sheet names and select a sheet.", "OK");
                    }
                }
            }

            private IEnumerator FetchSheetsData()
            {
                isFetching = true;
                UnityWebRequest request = UnityWebRequest.Get(sheetAPIurl);
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    ProcessSheetsData(request.downloadHandler.text);
                }
                else
                {
                    Debug.LogError("Error fetching data: " + request.error);
                }

                isFetching = false;
                Repaint();
            }

            private void ProcessSheetsData(string json)
            {
                var sheetsData = JsonUtility.FromJson<SheetDataList>(json);
                sheets.Clear();
                sheets.AddRange(sheetsData.sheetData);

                if (sheets.Count > 0)
                {
                    selectedSheetIndex = 0;
                }
            }

            private void ParseSelectedSheet()
            {
                var selectedSheet = sheets[selectedSheetIndex];
                string jsonFileName = RemoveSpecialCharacters(selectedSheet.sheetName);
                Debug.Log($"Selected Sheet: {selectedSheet.sheetName}, Sheet ID: {selectedSheet.sheetId}");

                EditorCoroutineUtility.StartCoroutine(ParseGoogleSheet(jsonFileName, selectedSheet.sheetId.ToString()), this);
            }

            private string RemoveSpecialCharacters(string sheetName)
            {
                return Regex.Replace(sheetName, @"[^a-zA-Z0-9\s]", "").Replace(" ", "_");
            }

            private IEnumerator ParseGoogleSheet(string jsonFileName, string gid, bool notice = true)
            {
                string sheetUrl = $"https://docs.google.com/spreadsheets/d/17Gff8xN4jhsmfIsua5yDj9HfptYavuPgQovEG4aS16U/export?format=tsv&gid={gid}";

                UnityWebRequest request = UnityWebRequest.Get(sheetUrl);
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    EditorUtility.DisplayDialog("Fail", "GoogleConnect Fail!", "OK");
                    yield break;
                }

                string data = request.downloadHandler.text;
                List<string> rows = ParseTSVData(data);

                if (rows == null || rows.Count < 4)
                {
                    Debug.LogError("Not enough data rows to parse.");
                    yield break;
                }

                HashSet<int> dbIgnoreColumns = GetDBIgnoreColumns(rows[0]);
                if (rows.Count < 2)
                {
                    Debug.LogError("Error: Not enough data rows. Check the sheet format.");
                    yield return null;
                }
                // ù ��° �ٿ��� ���(�÷���) ����
                var firstRow = rows[0].Trim();

                // �� ��° �ٿ��� Ÿ�� ��ȯ�� �� ��������
                var secondRow = rows[1].Trim();

                // �����͸� ��(`\t`) �Ǵ� ��ǥ(`,`)�� ������ (CSV/TSV �ڵ� ����)
                char delimiter = firstRow.Contains("\t") ? '\t' : ','; // ���� ������ TSV, ������ CSV
                char delimiter2 = secondRow.Contains("\t") ? '\t' : ','; // ���� ������ TSV, ������ CSV

                // ��� �����͸� �ùٸ��� �и�
                var keys = firstRow.Split(delimiter).Select(k => k.Trim().Replace("\"", "")).ToList();
                var typeIngredients = secondRow.Split(delimiter2).Select(k => k.Trim().Replace("\"", "")).ToList();

                var types = Enumerable.Repeat("string", keys.Count).ToList(); // �⺻ Ÿ���� string���� ����

                for (int i = 0; i < types.Count; i++)
                {
                    types[i] = ConvertValue(typeIngredients[i]).ToString();
                }

                Debug.Log("Parsed Headers: " + string.Join(", ", keys)); // Ȯ�ο� �α�

                JArray jArray = new JArray();

                for (int i = 0; i < rows.Count; i++)
                {
                    Debug.Log($"Row {i}: {rows[i]}");
                }

                for (int i = 1; i < rows.Count; i++) // ������ �� ����
                {
                    var rowData = rows[i].Split(delimiter).Select(d => d.Trim().Replace("\"", "")).ToList();

                    //�÷� ������ �´��� üũ
                    if (rowData.Count != keys.Count)
                    {
                        Debug.LogError($"Row {i} column count mismatch! Keys: {keys.Count}, Data: {rowData.Count}");
                        continue;
                    }

                    JObject rowObject = new JObject();
                    for (int j = 0; j < keys.Count; j++)
                    {
                        rowObject[keys[j]] = rowData[j];
                    }

                    jArray.Add(rowObject);
                }

                SaveJsonToFile(jsonFileName, jArray);
                string className = CreateDataClass(jsonFileName, keys, types, dbIgnoreColumns);  // C# Ŭ���� ����

                if (notice)
                {
                    EditorUtility.DisplayDialog("Success", "Sheet parsed and saved as JSON successfully!", "OK");
                    AssetDatabase.Refresh();
                }
            }

            // TSV ������ �Ľ�
            private List<string> ParseTSVData(string data)
            {
                return data.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            // DB_IGNORE �� ���͸�
            private HashSet<int> GetDBIgnoreColumns(string headerRow)
            {
                var dbIgnoreColumns = new HashSet<int>();
                var firstRow = headerRow.Split('\t').ToList();

                for (int i = 0; i < firstRow.Count; i++)
                {
                    if (firstRow[i].Equals("DB_IGNORE", StringComparison.OrdinalIgnoreCase))
                    {
                        dbIgnoreColumns.Add(i);
                        Debug.Log($"Column {i + 1} ignored due to DB_IGNORE");
                    }
                }

                return dbIgnoreColumns;
            }

            // ���� �� �Ľ�
            private JObject ParseRow(List<string> keys, List<string> types, List<string> rowData, HashSet<int> dbIgnoreColumns)
            {
                var rowObject = new JObject();

                for (int j = 0; j < keys.Count && j < rowData.Count; j++)
                {
                    if (dbIgnoreColumns.Contains(j)) continue;

                    string key = keys[j];
                    string type = types[j];
                    string value = rowData[j].Trim();

                    if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) continue;

                    rowObject[key] = ConvertValue(value);
                }

                return rowObject.HasValues ? rowObject : null;
            }

            private string TypeConvert(string value, string type)
            {
                value = value.Trim();
                // �迭 ���� ���� (��ǥ�� ���е� �� ����Ʈ)
                if (value.StartsWith("[") && value.EndsWith("]"))
                {
                    string content = value.Substring(1, value.Length - 2).Trim();
                    if (content.Contains(","))
                    {
                        string[] elements = content.Split(',');
                        if (elements.All(e => int.TryParse(e.Trim(), out _))) return "int[]";
                        if (elements.All(e => float.TryParse(e.Trim(), out _))) return "float[]";
                        return "string[]"; // �⺻������ ���ڿ� �迭�� ó��
                    }
                }
                // �⺻ Ÿ�� ����
                if (int.TryParse(value, out _)) return "int";
                if (long.TryParse(value, out _)) return "long";
                if (float.TryParse(value, out _)) return "float";
                if (double.TryParse(value, out _)) return "double";
                if (bool.TryParse(value, out _)) return "bool";
                if (byte.TryParse(value, out _)) return "byte";
                if (Guid.TryParse(value, out _)) return "Guid";
                if (DateTime.TryParse(value, out _)) return "DateTime";
                if (TimeSpan.TryParse(value, out _)) return "TimeSpan";

                return "string"; // �⺻������ ���ڿ��� ó��
            }



            private string TypeConvert(string value)
            {
                value = value.Trim();
                if (int.TryParse(value, out _)) return "int";
                if (long.TryParse(value, out _)) return "long";
                if (float.TryParse(value, out _)) return "float";
                if (double.TryParse(value, out _)) return "double";
                if (bool.TryParse(value, out _)) return "bool";
                if (DateTime.TryParse(value, out _)) return "DateTime";
                if (TimeSpan.TryParse(value, out _)) return "TimeSpan";
                return "string";
            }

            private JToken ConvertValue(string value)
            {
                string type = TypeConvert(value);
                switch (type)
                {
                    case "int": return int.TryParse(value, out int intValue) ? intValue : 0;
                    case "long": return long.TryParse(value, out long longValue) ? longValue : 0L;
                    case "float": return float.TryParse(value, out float floatValue) ? floatValue : 0.0f;
                    case "double": return double.TryParse(value, out double doubleValue) ? doubleValue : 0.0d;
                    case "bool": return bool.TryParse(value, out bool boolValue) ? boolValue : false;
                    case "DateTime": return DateTime.TryParse(value, out DateTime dateTimeValue) ? dateTimeValue : DateTime.MinValue;
                    case "TimeSpan": return TimeSpan.TryParse(value, out TimeSpan timeSpanValue) ? timeSpanValue : TimeSpan.Zero;
                    default: return value;
                }
            }

            // JSON ���� ���� �޼���
            private void SaveJsonToFile(string jsonFileName, JArray jArray)
            {
                string directoryPath = Path.Combine(Application.dataPath, "Resources", "JsonFiles");

                // ������ �������� ������ ����
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string jsonFilePath = Path.Combine(directoryPath, $"{jsonFileName}.json");

                File.WriteAllText(jsonFilePath, jArray.ToString());
                Debug.Log($"Saved JSON to: {jsonFilePath}");
            }

            // C# Ŭ���� ���� �޼���
            private string CreateDataClass(string fileName, List<string> keys, List<string> types, HashSet<int> dbIgnoreColumns)
            {
                string className = fileName; // ���� �̸��� Ŭ���� �̸����� ���
                string directoryPath = Path.Combine(Application.dataPath, "Resources/DataClass");

                // ������ �������� ������ ����
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string dataClassPath = Path.Combine(directoryPath, $"{className}.cs");

                using (StreamWriter writer = new StreamWriter(dataClassPath))
                {
                    writer.WriteLine("using System.Collections.Generic;");
                    writer.WriteLine("");
                    writer.WriteLine("namespace Chapter.Data");
                    writer.WriteLine("{");
                    writer.WriteLine("\t[System.Serializable]");
                    writer.WriteLine($"\tpublic class {className}");
                    writer.WriteLine("\t{");

                    // Ŭ���� �ʵ� ����
                    for (int i = 0; i < keys.Count; i++)
                    {
                        if (dbIgnoreColumns.Contains(i)) continue; // DB_IGNORE�� ������ �÷� �ǳʶ�

                        string fieldType = ConvertTypeToCSharp(types[i]);
                        string fieldName = SanitizeFieldName(keys[i]); // �ʵ�� ����

                        // �ʵ���� �ùٸ��� ������ ����
                        if (string.IsNullOrEmpty(fieldName) || !Regex.IsMatch(fieldName, @"^[a-zA-Z_][a-zA-Z0-9_]*$"))
                        {
                            Debug.LogError($"Invalid field name detected: {fieldName}");
                            continue;
                        }

                        writer.WriteLine($"\t\tpublic {fieldType} {fieldName};");
                    }

                    writer.WriteLine();

                    writer.WriteLine("\t}");
                    writer.WriteLine("}");
                }

                Debug.Log($"Saved C# class to: {dataClassPath}");
                AssetDatabase.Refresh(); // ���� ������ Ŭ������ �����Ϳ��� �ν��ϵ��� ��������

                return className; // ������ Ŭ���� �̸��� ��ȯ
            }

            private string SanitizeFieldName(string fieldName)
            {
                // ���ڷ� �����ϸ� _ �߰�, Ư������ ����, ������ _�� ����
                fieldName = Regex.Replace(fieldName, @"[^a-zA-Z0-9_]", "_");
                if (char.IsDigit(fieldName[0])) fieldName = "_" + fieldName;

                return fieldName;
            }

            private string ConvertTypeToCSharp(string type)
            {
                type = type.Trim(); // ���ʿ��� ���� ����

                // ���� Ÿ���� ���ڿ��� ó��
                if (int.TryParse(type, out _)) return "int";
                if (long.TryParse(type, out _)) return "long";
                if (float.TryParse(type, out _)) return "float";
                if (double.TryParse(type, out _)) return "double";
                if (bool.TryParse(type, out _)) return "bool";
                if (byte.TryParse(type, out _)) return "byte";

                switch (type)
                {
                    case "int": return "int";
                    case "long": return "long";
                    case "float": return "float";
                    case "double": return "double";
                    case "bool": return "bool";
                    case "byte": return "byte";
                    case "int[]": return "int[]";
                    case "float[]": return "float[]";
                    case "string[]": return "string[]";
                    case "DateTime": return "System.DateTime"; // DateTime�� ���� �ùٸ� ��ȯ��
                    case "TimeSpan": return "System.TimeSpan";
                    case "Guid": return "System.Guid";
                    default: return "string"; // �⺻������ string���� ó��
                }
            }
        }

        // SheetData Ŭ����
        [System.Serializable]
        public class SheetData
        {
            public string sheetName;
            public int sheetId;
        }

        // SheetDataList Ŭ����
        [System.Serializable]
        public class SheetDataList
        {
            public SheetData[] sheetData;
        }
#endif
    }
}
