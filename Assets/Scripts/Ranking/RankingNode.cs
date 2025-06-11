public class RankingNode
{
    public PlayerData PlayerData;
    public RankingNode Previous;
    public RankingNode Next;

    public RankingNode(int score, string name)
    {
        PlayerData = new PlayerData(name, score);
        Previous = null;
        Next = null;
    }
}
