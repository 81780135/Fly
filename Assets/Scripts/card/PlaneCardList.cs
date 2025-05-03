public class PlaneCardList : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform contentParent;
    
    private PlaneDataManager dataManager;

    void Start()
    {
        dataManager = FindObjectOfType<PlaneDataManager>();
        GenerateCards();
    }

    void GenerateCards()
    {
        List<int> planeIDs = dataManager.GetAllPlaneIDs();
        
        foreach (int id in planeIDs)
        {
            GameObject newCard = Instantiate(cardPrefab, contentParent);
            PlaneCard cardComponent = newCard.GetComponent<PlaneCard>();
            cardComponent.Initialize(id);
        }
    }
}