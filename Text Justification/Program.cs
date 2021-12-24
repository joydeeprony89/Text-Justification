using System;
using System.Text;

namespace Text_Justification
{
  class TextJustification
  {
    static void Main(string[] args)
    {
      string[] words1 = new string[]{ "Tushar", "roy", "likes", "to", "code"}; // "Tushar", "likes", "to", "write", "code", "at", "free", "time"
      TextJustification awl = new TextJustification();
      Console.WriteLine(awl.Justify(words1, 10)); // 12
    }

    public string Justify(string[] words, int width)
    {
      int[,] cost = new int[words.Length,words.Length];

      //next 2 for loop is used to calculate cost of putting words from
      //i to j in one line. If words don't fit in one line then we put
      //Integer.MAX_VALUE there.
      for(int i = 0; i < words.Length; i++)
      {
        cost[i,i] = width - words[i].Length;
        for(int j = i + 1; j < words.Length; j++)
        {
          cost[i,j] = cost[i,j - 1] - words[j].Length - 1;
        }
      }

      for (int i = 0; i < words.Length; i++)
      {
        for (int j = i; j < words.Length; j++)
        {
          if (cost[i,j] < 0)
          {
            cost[i,j] = int.MaxValue;
          }
          else
          {
            cost[i,j] = (int)Math.Pow(cost[i,j], 2);
          }
        }
      }

      //minCost from i to len is found by trying
      //j between i to len and checking which
      //one has min value
      int[] minCost = new int[words.Length];
      int[] result = new int[words.Length];
      for (int i = words.Length - 1; i >= 0; i--)
      {
        minCost[i] = cost[i,words.Length - 1];
        result[i] = words.Length;
        for (int j = words.Length - 1; j > i; j--)
        {
          if (cost[i,j - 1] == int.MaxValue)
          {
            continue;
          }
          if (minCost[i] > minCost[j] + cost[i,j - 1])
          {
            minCost[i] = minCost[j] + cost[i,j - 1];
            result[i] = j;
          }
        }
      }
      int ii = 0;
      int jj;

      Console.WriteLine("Minimum cost is " + minCost[0]);
      Console.WriteLine("\n");
      //finally put all words with new line added in 
      //string buffer and print it.
      StringBuilder builder = new StringBuilder();
      do
      {
        jj = result[ii];
        for (int k = ii; k < jj; k++)
        {
          builder.Append(words[k] + " ");
        }
        builder.Append("\n");
        ii = jj;
      } while (jj < words.Length);

      return builder.ToString();
    }
  }
}
