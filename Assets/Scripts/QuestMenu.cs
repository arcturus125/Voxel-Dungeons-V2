using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMenu : MonoBehaviour
{

    public static QuestMenu singleton;
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private QuestListing _questListing;

    [SerializeField]
    private float nextListingOffset = 25;
    [SerializeField]
    private float heightOfListing = 25.7f;

    public List<QuestListing> prefabList;

    public enum ESelectedQuestMenu
    {
        InProgress,
        Completed
    }
    public static ESelectedQuestMenu selectedQuestMenu;

    private void Awake()
    {
        singleton = this;
    }
    private void Update()
    {
        UpdateQuestUI(); // TODO: chaneg this to not run every frame
    }

    public void UpdateQuestUI()
    {
        foreach(QuestListing ql in prefabList)
        {
            DestroyImmediate(ql.gameObject);
        }
        prefabList = new List<QuestListing>();
        int index = 0;
        if (selectedQuestMenu == ESelectedQuestMenu.InProgress)
        {
            foreach (Quest q in Quest.activeQuests)
            {
                QuestListing ql = Instantiate(_questListing, _content);
                ql.SetQuestListingInfo(q, index);
                //ql.transform.position = _content.transform.position;
                ql.transform.localPosition = new Vector2(91.5f, -heightOfListing);

                RectTransform rt = GetComponent<RectTransform>();

                //ql.transform.Translate(new Vector2(rt.rect.width/2  , -rt.rect.height/2));
                ql.transform.Translate(new Vector2(0, (-nextListingOffset * index)));
                prefabList.Add(ql);
                index++;
            }
        }

        RectTransform r = _content.GetComponent<RectTransform>();
        r.sizeDelta = new Vector2(
            r.sizeDelta.x,
            heightOfListing * index);
    }
}
