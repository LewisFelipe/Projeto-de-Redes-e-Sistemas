using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.SqliteClient;

public class Login : MonoBehaviour
{
    const string DB_NAME = "URI=file:SQLiteDB.db";

    public static bool isLogged = false;
    public static string nick;
    public InputField ifNick;
    public InputField ifPassword;

    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;
    private string verifica, password;

    private void ConnectionDB()
    {
        connection = new SqliteConnection(DB_NAME);
        command = connection.CreateCommand();
        connection.Open();
    }

    private void InsertDB()
    {
        command = connection.CreateCommand();
        command.CommandText = "CREATE TABLE IF NOT EXISTS ranking (id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE, nick VARCHAR(12), password VARCHAR(16), bestRun FLOAT, highestScore INTEGER);";
        command.ExecuteNonQuery();
    }

    public void RegisterDB()
    {
        InsertDB();

        command.CommandText = "SELECT nick FROM ranking WHERE nick = '" + ifNick.text + "';";
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            verifica = reader.GetString(0);
        }

        if (verifica == ifNick.text)
        {
            Debug.LogError("Usuário já existe");
        }
        else
        {
            command.CommandText = "INSERT INTO ranking (nick, password) VALUES('" + ifNick.text + "', '" + ifPassword.text + "');";
            command.ExecuteNonQuery();
        }
    }

    public void LoginDB()
    {
        if (!string.IsNullOrEmpty(ifNick.text) && !string.IsNullOrEmpty(ifPassword.text))
        {
            command.CommandText = "SELECT nick, password FROM ranking WHERE nick = '" + ifNick.text + "' AND password = '" + ifPassword.text + "';";
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                nick = reader.GetString(0);
                password = reader.GetString(1);
            }
            if(ifNick.text.Equals(nick) && ifPassword.text.Equals(password))
            {
                isLogged = true;
                SceneManager.LoadScene(1);
            }
        }
    }

    private void OnEnable()
    {
        ConnectionDB();
    }

    private void OnDisable()
    {
        connection.Close();
    }
}
