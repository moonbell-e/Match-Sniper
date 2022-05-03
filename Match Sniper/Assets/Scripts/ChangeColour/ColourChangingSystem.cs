using System.Collections.Generic;
using UnityEngine;

public class ColourChangingSystem : MonoBehaviour
{
    [SerializeField] private List<Unit> _soldiers;
    [SerializeField] private List<Unit> _juggernauts;
    [SerializeField] private List<Unit> _rambos;
    [SerializeField] private List<Unit> _shields;

    [SerializeField] private Color _colour;

    private void Awake()
    {
        //for(int i = 0; i < _juggernauts.Count; i++)
           // ChangeColour(_juggernauts[i].Body, _juggernauts[i].Helmet, 2, 1);
    }
    private void ChangeColour(SkinnedMeshRenderer body, SkinnedMeshRenderer helmet, int i, int j)
    {
        Material[] mats = body.materials;
        Material[] mats1 = helmet.materials;
        ChangeColourIndex(i, j, mats, mats1);
        body.materials = mats;
        helmet.materials = mats1;
    }

    private void ChangeColourIndex(int i, int j, Material[] mats, Material[] mats1)
    {
        mats[i].color = _colour;
        mats1[j].color = _colour;
    }
}
