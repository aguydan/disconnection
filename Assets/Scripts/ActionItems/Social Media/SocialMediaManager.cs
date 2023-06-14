using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class SocialMediaManager : MonoBehaviour
{
    [SerializeField] private SocialMediaItem _SM;
    [SerializeField] private Button _SMButton;
    [SerializeField] private SocialMediaPost _postPrefab;
    [SerializeField] private int _amountOfNegativePosts = 4;
    
    [SerializeField] private SMPosts _posts;
    [SerializeField] private GameObject _background;

    public enum PostType {
        Positive,
        Negative,
        Secret
    }

    private Sprite _mainSprite;
    private Sprite _secondarySprite;
    private List<Sprite> _negativeSprites = new List<Sprite>();

    public static SocialMediaManager Instance;

    private void Awake() => Instance = this;
    
    private void Start()
    {
        PopulateSocialMedia();
    }

    public void ActivateSocialMedia()
    {
        _background.SetActive(true);
        _SM.gameObject.SetActive(true);
        ActionItemManager.instance.IsActionItemCurrentlyVisible = true;
    }

    public void DeactivateSocialMedia()
    {
        _background.SetActive(false);
        _SM.gameObject.SetActive(false);
        ActionItemManager.instance.IsActionItemCurrentlyVisible = false;
    }

    public void DeactivateSocialMediaFromPost()
    {
        //ЭТО БУДЕТ КОРУТИНА

        //ПРОВЕРИТЬ ИМПАКТ ТЕКСТ И НА ЕГО ОСНОВАНИИ ДАТЬ ОЧКИ
        
        ActionItemManager.instance.IsSMCompleted = true;
        CursorManager.Instance.StopAutomaticCursor = false;

        ActionItemManager.instance.SMMDeactivateSocialMedia();
    }

    public void PickUpSocialMedia()
    {
        _SMButton.gameObject.SetActive(true);
    }
    
    private void PopulateSocialMedia()
    {
        ItemSprite.ItemCategory winningCateg = ItemSpawner.Instance.winningItem.Category;
        IEnumerable<NonNegativePost> positivePosts = _posts.PositivePosts;
        
        //shuffle positive posts
        
        //наверно надо объединить в одну функцию, которая будет создавать массив
        //для дальнейшего создания постов
        _mainSprite = positivePosts.Single(post => post.Category == winningCateg).Sprite;
        _secondarySprite = positivePosts.Single(post => post.Category != winningCateg).Sprite;

        // ChooseRandomNegativeSprites(_amountOfNegativePosts);

        SocialMediaPost post = Instantiate(_postPrefab);
        post.Image.sprite = _mainSprite;
        post.Type = PostType.Positive;
        post.transform.SetParent(_SM.Content);

        SocialMediaPost post1 = Instantiate(_postPrefab);
        post1.Image.sprite = _secondarySprite;
        post1.Type = PostType.Positive;
        post1.transform.SetParent(_SM.Content);

        foreach (Sprite sprite in _negativeSprites)
        {
            SocialMediaPost post2 = Instantiate(_postPrefab);
            post2.Image.sprite = sprite;
            post2.Type = PostType.Negative;
            post2.transform.SetParent(_SM.Content);
        }
    }

//     private void ChooseRandomNegativeSprites(int amount)
//     {
//         List<int> uniqueIndexes = new List<int>();
        
//         for (int i = 0; i < amount; i++)
//         {
//             int guard = 0;
        
//             while (guard < 100)
//             {
//                 int rand = Random.Range(0, 4);

//                 if (!uniqueIndexes.Contains(rand))
//                 {
//                     uniqueIndexes.Add(rand);
//                     break;
//                 }
                
//                 guard++;
//             }
//         }

//         foreach (int index in uniqueIndexes)
//         {
//             _negativeSprites.Add(_posts.NegativePosts[index].Sprite);
//         }
//     }
}
