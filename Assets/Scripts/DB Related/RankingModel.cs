using System.Collections;
using System.Collections.Generic;

public class RankingModel
{
    public int id {get; private set;}
    public string nick{get; set;}
    public string password{private get; set;}
    public double bestRun{get; set;}
    public int highestScore{get; set;}

    public RankingModel(){}

    public RankingModel(string nick, string password, double bestRun, int highestScore)
    {
        this.nick = nick;
        this.password = password;
        this.bestRun = bestRun;
        this.highestScore = highestScore;
    }
    public RankingModel(int id, string nick, int highestScore)
    {
        this.id = id;
        this.nick = nick;
        this.highestScore = highestScore;
    }

    public RankingModel(int id, string nick, double bestRun)
    {
        this.id = id;
        this.nick = nick;
        this.bestRun = bestRun;
    }
}
