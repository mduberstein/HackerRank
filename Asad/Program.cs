Input: {2,4,6,4,-1,10}
Ouput: (2,4) (2,6) ... (4,2) (4,6)
Problem: Find all distinct pairs(i, j) where i and j belong to numbers.
  
(2,4) 
(2,4) x
(4,2) x

  class Pair
{

    Pair(int f, int s)

  override bool Equals(Pair other)
    {

    }
  
  override int GetHashCode()
    {
    }
    static override operator == (int f, int s){
        return f == s || s == f;
    }
    static override operator != (in..)
}
public static void FindAllPairs(int[] n)
{
    var h = new Hashset<Pair>();
    for (int i = 0; i < n.Length; i++)
    {
        for (int j = 0; j < n.Length; i++)
        {
            if (i != j)
            {
                var p = new Pair(n[i], n[j]);
                if (!h.Contains(p))
                {
                    h.Add(p);
                    Console.Write($"({n[i]},{n[j]}");
                }
            }
        }

    }