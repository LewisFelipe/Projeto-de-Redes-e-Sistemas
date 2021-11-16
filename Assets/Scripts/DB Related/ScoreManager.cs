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

    public static float runTime = -1;
    public static int score = 0;
    public GameObject entriePrefab;

    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;
    private float bestRun = -1;
    private int highestScore = 0, playerToDelete;
    private int ids, highestScores, bestRuns;
    private string nicks;
    private List<RankingModel> models = new List<RankingModel>();

    private void ConnectionDB()
    {
        connection = new SqliteConnection(DB_NAME);
        command = connection.CreateCommand();
    }

    public void changeHighestScore()
    {
        connection.Open();
        command = connection.CreateCommand();

        command.CommandText = "SELECT highestScore FROM ranking WHERE nick = '" + Login.nick + "';";
        reader = command.ExecuteReader();
        while(reader.Read())
        {
            highestScore = reader.GetInt32(0);
        }

        if(score > highestScore)
        {
            command.CommandText = "UPDATE highestScore INTO ranking WHERE nick = '" + Login.nick + "' VALUE(" + highestScore + ");";
            command.ExecuteNonQuery();
        }
        connection.Close();
    }

    public void runEnded()
    {
        connection.Open();
        command = connection.CreateCommand();

        command.CommandText = "SELECT bestRun FROM ranking WHERE nick = '" + Login.nick + "';";
        reader = command.ExecuteReader();
        while(reader.Read())
        {
            bestRun = reader.GetFloat(0);
        }

        if((runTime > 0) || (runTime < bestRun))
        {
            command.CommandText = "UPDATE bestRun INTO ranking WHERE nick = '" + Login.nick + "' VALUE(" + bestRun + ");";
            command.ExecuteNonQuery();
        }
        connection.Close();
    }

    private void GetHighestScores()
    {
        models.Clear();
        connection.Open();
        command = connection.CreateCommand();

        command.CommandText = "SELECT id, nick, highestScores FROM ranking;";
        reader = command.ExecuteReader();
        while(reader.Read())
        {
            models.Add(new RankingModel(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
        }
        connection.Close();
    }

    private void GetBestRuns()
    {
        models.Clear();
        connection.Open();
        command = connection.CreateCommand();

        command.CommandText = "SELECT id, nick, bestRuns FROM ranking;";
        reader = command.ExecuteReader();
        while(reader.Read())
        {
            models.Add(new RankingModel(reader.GetInt32(0), reader.GetString(1), reader.GetFloat(2)));
        }
        connection.Close();
    }

    private void DeleteFromDB()
    {
        connection.Open();
        command = connection.CreateCommand();

        command.CommandText = "DELETE FROM ranking WHERE id = " + playerToDelete + ";";
        command.ExecuteNonQuery();
        connection.Close();
    }

    private void ShowHighScores()
    {
        for(int i = 0; i < models.Count; i++)
        {
            GameObject temporaryObject = Instantiate(entriePrefab);

            RankingModel temporaryModel = models[i];

            //temporaryObject.GetComponent<ModelScript>();
        }
    }

    private void OnEnable()
    {
        ConnectionDB();
    }

    private void OnDisbale()
    {
        connection.Dispose();
    }

    private void Start()
    {
        runTime = 0;
    }

    private void Update()
    {
        if(Login.isLogged)
        {
            runTime += Time.deltaTime;
        }
    }
}
