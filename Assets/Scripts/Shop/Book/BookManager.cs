using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BookManager : MonoBehaviour
{
    [Header("Book Setup")]
    public List<ChapterButtons> chapterButtons;
    public List<bool> isSelected = new List<bool>();
    public GameObject pagePrefab;

    public GameObject pageUI;
    public TMPro.TextMeshProUGUI pageTitle;
    public TMPro.TextMeshProUGUI pageText;
    private GameObject pageCurentModel;
    public GameObject pageModel;

    public Camera modelCamera;

    public GameObject navigationUI;
    public GameObject ChapterUI;

    [HideInInspector]
    public List<GameObject> spawnedPages = new List<GameObject>();

    private void Awake()
    {
        // Reset selections
        isSelected = new List<bool>(new bool[chapterButtons.Count]);

        // Link each button to this manager and assign index
        for (int i = 0; i < chapterButtons.Count; i++)
        {
            chapterButtons[i].Initialize(this, i);
        }

        pageUI.SetActive(false);
    }

    public void SelectChapter(int chapterIndex, ChapterData chapterData)
    {
        pageUI.SetActive(false);
        if (chapterIndex < 0 || chapterIndex >= isSelected.Count)
        {
            Debug.LogWarning("Invalid chapter index.");
            return;
        }

        if (isSelected[chapterIndex]) return;

        // Reset all selections
        for (int i = 0; i < isSelected.Count; i++)
        {
            isSelected[i] = false;
            chapterButtons[i].ResetAppearance();
        }

        // Clear previous pages
        foreach (var obj in spawnedPages)
        {
            if (obj != null) Destroy(obj);
        }
        spawnedPages.Clear();

        // Mark selected
        isSelected[chapterIndex] = true;
        chapterButtons[chapterIndex].Highlight();

        // Page data
        int totalPages = chapterData.pages.Count;
        int pageCountToDisplay = Mathf.Min(totalPages, 10);

        float spacingZ = 2f;
        float spacingY = 2.5f;

        Vector3 basePos = chapterButtons[chapterIndex].transform.position;
        float rowY1 = basePos.y - 3f;
        float rowY2 = rowY1 - spacingY;
        float x = basePos.x;

        // Split into two rows
        int firstRowCount = Mathf.Min(pageCountToDisplay, 5);
        int secondRowCount = pageCountToDisplay - firstRowCount;

        for (int i = 0; i < pageCountToDisplay; i++)
        {
            int row = i / 5;
            int col = i % 5;

            int itemsInRow = row == 0 ? firstRowCount : secondRowCount;
            float totalWidth = (itemsInRow - 1) * spacingZ;
            float startZ = basePos.z - totalWidth / 2f;

            float zPos = startZ + col * spacingZ;
            float yPos = row == 0 ? rowY1 : rowY2;

            Vector3 spawnPos = new Vector3(x, yPos, zPos);
            GameObject pageObj = Instantiate(pagePrefab, spawnPos, Quaternion.identity);

            if (pageObj.TryGetComponent<PageDataObject>(out var dataObj))
                dataObj.pageData = chapterData.pages[i];

            spawnedPages.Add(pageObj);
        }
    }

    public void SelectPage(PageData pageData)
    {
        // Clear previous pages
        foreach (var obj in spawnedPages)
        {
            if (obj != null) Destroy(obj);
        }
        spawnedPages.Clear();

        foreach (var button in chapterButtons)
        {
            button.ResetAppearance();
            isSelected[chapterButtons.IndexOf(button)] = false;
        }
        navigationUI.SetActive(false);
        pageTitle.text = pageData.title;
        pageText.text = pageData.storyText;
        //pageModel.transform.position = new Vector3 (0, -2.5f, 4.2f);
        //pageModel.transform.position = new Vector3(0, -2.5f, 4.2f);
        pageCurentModel = Instantiate(pageData.model3D, pageModel.transform);
        pageCurentModel.transform.localScale = new Vector3(1, 1, 1);
        ChapterUI.SetActive(false);
        pageUI.SetActive(true);

        switch (pageData.id)
        {
            case 1:
                modelCamera.orthographicSize = 2.98f;
                return;
            case 2:
                modelCamera.orthographicSize = 2.73f;
                return;
            case 3:
                modelCamera.orthographicSize = 2.5f;
                return;
        }

    }

    public void ClosePage()
    {
        pageUI.SetActive(false);
        if (pageCurentModel != null)
        {
            Destroy(pageCurentModel);
            pageCurentModel = null;
        }
        ChapterUI.SetActive(true);
        navigationUI.SetActive(true);
    }

}
