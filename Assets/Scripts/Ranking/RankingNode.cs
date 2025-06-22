public class RankingNode
{
    public PlayerData PlayerData;
    public RankingNode Previous;
    public RankingNode Next;

    public RankingNode(PlayerData playerData)
    {
        PlayerData = playerData;
        Previous = null;
        Next = null;
    }
}
