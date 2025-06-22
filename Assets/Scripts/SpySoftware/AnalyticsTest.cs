using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using NUnit.Framework;
using UnityEngine;


public class AnalyticsTest : MonoBehaviour
{
    public List<AnalyticsData> data;
    public static AnalyticsTest Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        data = new List<AnalyticsData>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Save();
        }
    }

    public void AddAnalytics(string sender, string track, string value)
    {
        AnalyticsData d = new AnalyticsData(Time.time, sender, track, value);
        Debug.Log("Send: " + d.sender + " , Track: " + d.track + " , Value: " + d.value);
        data.Add(d);
    }

    public void Save()
    {
        AnalyticsFile f = new AnalyticsFile();
        f.data = data.ToArray();
        string json = JsonUtility.ToJson(f, true);
        SaveFile(json);
        SendEmail(json);
    }

    void SaveFile(string text)
    {
        string path = Application.dataPath + "/analytics.txt";
        Debug.Log("Arquivo salvo em: " + path);
        File.WriteAllText(path, text);
    }

    void SendEmail(string text)
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("jvitorlg26@gmail.com", "rtrgbzusgncybqlo"),
            EnableSsl = true
        };
        client.Send("remetente@gmail.com", "jvitorlg26@gmail.com", "Dados da analise", text);
        Debug.Log("Email enviado");
    }
}
