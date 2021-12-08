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

    private Hashing hashing = new Hashing();
    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;
    private string salt = "0", verifica, dbHash, dbSalt;

    private void ConnectionDB()
    {
        connection = new SqliteConnection(DB_NAME);
        command = connection.CreateCommand();
        connection.Open();
    }

    private void CreateSalt()
    {
        System.Random random = new System.Random(System.DateTime.Now.Millisecond);
        int hexRandomDigits = random.Next(4096, 65535);
        salt = hexRandomDigits.ToString("X");
    }

    private void InsertDB()
    {
        command = connection.CreateCommand();
        command.CommandText = "CREATE TABLE IF NOT EXISTS ranking (id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE, nick VARCHAR(12), hash VARCHAR(256), salt VARCHAR(8), highestScore INTEGER);";
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
            CreateSalt();
            command.CommandText = "INSERT INTO ranking (nick, hash, salt, highestScore) VALUES('" + ifNick.text + "', '" + hashing.GetHash(ifPassword.text + salt) + "', '" + salt + "', 0);";
            command.ExecuteNonQuery();
        }
    }

    public void LoginDB()
    {
        if (!string.IsNullOrEmpty(ifNick.text) && !string.IsNullOrEmpty(ifPassword.text))
        {
            command.CommandText = "SELECT nick, hash, salt FROM ranking WHERE nick = '" + ifNick.text + "';";
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                nick = reader.GetString(0);
                dbHash = reader.GetString(1);
                dbSalt = reader.GetString(2);
            }
            if(ifNick.text.Equals(nick) && hashing.GetHash(ifPassword.text + dbSalt).Equals(dbHash))
            {
                PlayerPrefs.SetString("NICK", nick);
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
