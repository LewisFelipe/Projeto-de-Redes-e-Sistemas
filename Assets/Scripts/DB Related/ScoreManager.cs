using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.SqliteClient;

public class ScoreManager : MonoBehaviour
{
    const string DB_NAME = "URI=file:SQLiteDB.db";

    public static int score = 0;
    public GameObject entriePrefab;
    public Transform entrieParent;
    public int maximumEntriesShown;

    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;
    private int highestScore = 0, playerToDelete;
    private int ids, highestScores;
    private string nicks;
    private List<RankingModel> models = new List<RankingModel>();
    GameObject[] temporaryObject;

    private void ConnectionDB()
    {
        connection = new SqliteConnection(DB_NAME);
        command = connection.CreateCommand();
    }

    public void ChangeHighestScore()
    {
        connection.Open();
        command = connection.CreateCommand();

        command.CommandText = "SELECT highestScore FROM ranking WHERE nick ='" + Login.nick + "';";
        reader = command.ExecuteReader();
        while(reader.Read())
        {
            highestScore = reader.GetInt32(0);
        }

        if(score > highestScore)
        {
            command.CommandText = "UPDATE ranking SET highestScore = " + score + " WHERE nick = '" + Login.nick + "';";
            command.ExecuteNonQuery();
        }
        connection.Close();
    }

    private void GetHighestScores()
    {
        models.Clear();
        connection.Open();
        command = connection.CreateCommand();

        command.CommandText = "SELECT id, nick, highestScore FROM ranking;";
        reader = command.ExecuteReader();
        while(reader.Read())
        {
            models.Add(new RankingModel(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
        }
        connection.Close();
        models.Sort();
    }

    private void DeleteFromDB()
    {
        connection.Open();
        command = connection.CreateCommand();

        command.CommandText = "DELETE FROM ranking WHERE id = " + playerToDelete + ";";
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void ShowHighScores()
    {
        GetHighestScores();
        for(int i = 0; i < maximumEntriesShown; i++)
        {
            if(i < models.Count)
            {
                temporaryObject[i] = Instantiate(entriePrefab);

                RankingModel temporaryModel = models[i];

                temporaryObject[i].GetComponent<ModelScript>().SetEntries(("#" + ((i + 1).ToString())) ,temporaryModel.nick, temporaryModel.highestScore.ToString());

                temporaryObject[i].transform.SetParent(entrieParent);
                temporaryObject[i].transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void CleanRanking()
    {
        for (int i = 0; i < temporaryObject.Length - 1; i++)
        {
            Destroy(temporaryObject[i]);
        }
    }

    public void RefreshRanking()
    {
        CleanRanking();
        ShowHighScores();
    }

    private void OnEnable()
    {
        ConnectionDB();
        temporaryObject = new GameObject[maximumEntriesShown];
    }

    private void OnDisable()
    {
        connection.Dispose();
    }
}
