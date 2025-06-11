using System.Collections.Generic;

public class RankingList
{
    public RankingNode Head;
    public RankingNode Tail;

    public RankingList()
    {
        Head = null;
        Tail = null;
    }

    public void AddScore(int score, string name)
    {
        RankingNode newNode = new RankingNode(score, name);

        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
            return;
        }

        RankingNode current = Head;

        while (current != null && current.PlayerData.score > score)
            current = current.Next;

        if (current == Head)        // Inserção no início
        {
            newNode.Next = Head;
            Head.Previous = newNode;
            Head = newNode;
        }
        else if (current == null)   // Inserção no final
        {
            Tail.Next = newNode;
            newNode.Previous = Tail;
            Tail = newNode;
        }
        else    // Inserção no meio
        {
            newNode.Previous = current.Previous;
            newNode.Next = current;
            current.Previous.Next = newNode;
            current.Previous = newNode;
        }
    }

    public List<PlayerData> GetTopPlayers(int n)
    {
        List<PlayerData> topList = new List<PlayerData>();
        var current = Head;
        int count = 0;

        while (current != null && count < n)
        {
            topList.Add(current.PlayerData);
            current = current.Next;
            count++;
        }
        return topList;
    }
}
