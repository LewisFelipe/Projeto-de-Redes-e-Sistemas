using Realms;
using Realms.Sync;

public class RankingModel : RealmObject
{
    [PrimaryKey]
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
}
