using System.Collections.Generic;
using System.Collections;

//classe que guarda uma lista com todas as espécies de peixes
[System.Serializable]
public class FishesData : IEnumerable<Fish>
{
    public List<Fish> fishes = new List<Fish>();

    /* public FishesData(List<Fish> fishList){
        fishes = fishList;
    } */

    public IEnumerator<Fish> GetEnumerator()
    {
        //return fishes.GetEnumerator();
        for(int i = 0; i < fishes.Count; i++)
            yield return fishes[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
