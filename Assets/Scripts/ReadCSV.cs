using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using System.Text.RegularExpressions;

public class ReadCSV : MonoBehaviour
{
    [HideInInspector]
    public JsonData booksJson = new JsonData();
 
    public List<BookInformation> bookInfos = new List<BookInformation>();
 
    void Start()
    {
        StartCoroutine(Check());
    }

    

    private IEnumerator Check()
    {
        UnityWebRequest www = new UnityWebRequest("https://docs.google.com/spreadsheets/d/1V7CPJncqt4K5tcSGj9WS8FQV4QA6Lf4hs8Xf8gF_kl4/export?format=csv&gid=0");
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            // Debug.LogWarning(www.downloadHandler.text);
            // Or retrieve results as binary data
            var dataLines = www.downloadHandler.text.Split('\n');
           
            foreach (var lineItem in dataLines.Skip(1))
            {
                //var dataItem = lineItem.ToString().Split(','); ,(?!\s)
                var dataItem = Regex.Split(lineItem, @",(?!\s)");
                Debug.Log(dataItem[0]);
                BookInformation BI = new BookInformation(dataItem[0], dataItem[1], dataItem[2], double.Parse(dataItem[3]), dataItem[4].Replace("\"",""), dataItem[5], dataItem[6], dataItem[7], dataItem[8], dataItem[9], dataItem[10].Replace("\r", ""));
                bookInfos.Add(BI);
               

            }
               
            booksJson = JsonMapper.ToJson(bookInfos);
            Debug.Log(booksJson);
            File.WriteAllText(Application.persistentDataPath + "/BookName.json", booksJson.ToString());
            string jsonFilePath = File.ReadAllText(Application.persistentDataPath + "/BookName.json");
          
            booksJson = JsonMapper.ToObject(jsonFilePath);
           

            //byte[] results = www.downloadHandler.data;
        }

    }
    public JsonData GetBookInfos( )
    {       
        return booksJson;
    }


}

public class BookInformation
{
    public string bookName;
    public string author;
    public string genre;
    public double rating;
    public string description;
    public string recommendation1;
    public string recommendation2;
    public string recommendation3;
    public string recommImageUrlId1;
    public string recommImageUrlId2;
    public string recommImageUrlId3;
    public string imageUrl;

    public BookInformation(string bookName, string author, string genre, double rating, string description, string recommendation1, string recommendation2, string recommendation3,  string recommImageUrlId1, string recommImageUrlId2, string recommImageUrlId3)
    {
        this.bookName = bookName;
        this.author = author;
        this.genre = genre;
        this.rating = rating;
        this.description = description;
        this.recommendation1 = recommendation1;
        this.recommendation2 = recommendation2;
        this.recommendation3 = recommendation3;

        this.recommImageUrlId1 = recommImageUrlId1;
        this.recommImageUrlId2 = recommImageUrlId2;
        this.recommImageUrlId3 = recommImageUrlId3;

        this.imageUrl = imageUrl;
    }
}


