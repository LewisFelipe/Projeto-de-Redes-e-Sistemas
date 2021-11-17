using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RankingModel : IComparable<RankingModel>
{
    public int id {get; private set;}
    public string nick{get; set;}
    public string password{private get; set;}
    public int highestScore{get; set;}

    public RankingModel(){}

    public RankingModel(string nick, string password, int highestScore)
    {
        this.nick = nick;
        this.password = password;
        this.highestScore = highestScore;
    }
    public RankingModel(int id, string nick, int highestScore)
    {
        this.id = id;
        this.nick = nick;
        this.highestScore = highestScore;
    }

    public int CompareTo(RankingModel other)
    {
        if(other.highestScore < this.highestScore)
        {
            return -1;
        }
        else if(other.highestScore > this.highestScore)
        {
            return 1;
        }
        else if(other.id > this.id)
        {
            return -1;
        }
        else if(other.id < this.id)
        {
            return 1;
        }
        return 0;
    }
}
