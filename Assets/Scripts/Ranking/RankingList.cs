using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class RankingList
{
    public List<PlayerData> playerDataList = new List<PlayerData>();

    public void AddScore(PlayerData newPlayer)
    {
        playerDataList.Add(newPlayer);
        SortRanking();
    }

    public List<PlayerData> GetTopPlayers(int count)
    {
        return playerDataList.Take(count).ToList();
    }

    private void SortRanking()
    {
        playerDataList = playerDataList
            .OrderByDescending(p => p.score)
            .ThenBy(p => p.name)
            .ToList();
    }
}
