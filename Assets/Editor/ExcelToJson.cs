using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Excel;
using System.Data;
using Newtonsoft.Json;

public class ExcelToJson {

    private static string excelPath;

    [MenuItem("Tools/ExcelToJson")]
    public static void LoadExcel() {
        Object selection = Selection.activeObject;
        if (selection == null) {
            return;
        }
        excelPath = AssetDatabase.GetAssetPath(selection);
        ConvertJson();
    }

    public static void ConvertJson() {
        FileStream stream = File.Open(excelPath, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet dataSet = excelDataReader.AsDataSet();
        if (dataSet.Tables.Count == 0) {
            return;
        }
        DataTable sheet = dataSet.Tables[0];
        if (sheet.Rows.Count == 0) {
            return;
        }
        int rowCount = sheet.Rows.Count;
        int colCount = sheet.Columns.Count;
        List<Dictionary<string, object>> dataTable = new List<Dictionary<string, object>>();
        for (int i = 2; i < rowCount; i++) {
            Dictionary<string, object> row = new Dictionary<string, object>();
            for (int j = 0; j < colCount; j++) {
                string field = sheet.Rows[1][j].ToString();
                if (string.Equals(sheet.Rows[0][j].ToString(), "Array")) {
                    row[field] = "[" + sheet.Rows[i][j] + "]";
                } else {
                    row[field] = sheet.Rows[i][j];
                }
            }
            dataTable.Add(row);
        }
        string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
        json = json.Replace("\"[", "[").Replace("]\"", "]");
        string filePath = Application.dataPath + "/Resources/Config/Hero.json";
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write)) {
            using (TextWriter textWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8)) {
                textWriter.Write(json);
            }
        }
        AssetDatabase.Refresh();
    }
}
