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
    [SerializeField] private Comment _commentPrefab;
    [SerializeField] private int _amountOfNegativePosts = 4;

    [SerializeField] private SMPosts _posts;

    private SocialMediaPost[] _finalPosts;
    private List<Comment> _finalComments = new List<Comment>();
    
    public int Tries { get; set; } = 3;
    public TextMeshProUGUI ImpactSigns;
    public SocialMediaAnimator Animator;

    public enum PostType {
        Positive,
        Negative,
        Secret
    }

    public static SocialMediaManager Instance;

    private void Awake() => Instance = this;
    
    private void Start()
    {
        PopulateSocialMedia();
    }

    public void ActivateSocialMediaProper()
    {
        Animator.gameObject.SetActive(true);
        Animator.LaunchStartingAnimation();
    }

    public void ActivateSocialMedia()
    {
        _SM.gameObject.SetActive(true);

        ActionItemManager.instance.IsBookVisible = true;
        CursorManager.Instance.StopAutomaticCursor = true;

        ActionItemManager.instance.IsActionItemCurrentlyVisible = true;
    }

    public void DeactivateSocialMedia()
    {
        _SM.gameObject.SetActive(false);

        ActionItemManager.instance.IsBookVisible = false;
        CursorManager.Instance.StopAutomaticCursor = false;

        ActionItemManager.instance.IsActionItemCurrentlyVisible = false;
    }

    public void FinishSocialMedia()
    {
        //ЭТО БУДЕТ КОРУТИНА

        CountOverallMoodImpact();
        
        _SMButton.gameObject.SetActive(false);
        ActionItemManager.instance.IsSMCompleted = true;

        Animator.LaunchExitAnimation();

        void CountOverallMoodImpact()
        {
            switch (ImpactSigns.text)
            {
                case "---":
                    ScoreManager.instance.DecreaseMood(0, true);
                break;
                case "+++":
                    ScoreManager.instance.IncreaseMood(6);
                break;
                default:
                    CountBasedOnSignsAmount();
                break;
            }

            void CountBasedOnSignsAmount()
            {
                Dictionary<char, int> signsAmount = new Dictionary<char, int>{
                    {'+', 0},
                    {'-', 0},
                };

                foreach (char c in ImpactSigns.text)
                {
                    signsAmount[c]++;
                }

                if (signsAmount['-'] == 2) return;
                if (signsAmount['+'] == 2) ScoreManager.instance.IncreaseMood(3);
            }
        }
    }

    public void PickUpSocialMedia()
    {
        _SMButton.gameObject.SetActive(true);
    }
    
    private void PopulateSocialMedia()
    {
        ItemSprite.ItemCategory winningCateg = ItemSpawner.Instance.winningItem.Category;
        
        PositivePost[] pp = _posts.PositivePosts;
        Sprite[] np = _posts.NegativePosts;
        SecretPost[] secretPosts = _posts.SecretPosts;

        System.Random rng = new System.Random();
        rng.Shuffle(pp);
        rng.Shuffle(np);
        rng.Shuffle(secretPosts);
        
        IEnumerable<PositivePost> positivePosts = pp;
        IEnumerable<Sprite> negativePosts = np;
        
        CreateArrayOfPosts();

        void CreateArrayOfPosts()
        {
            Sprite posSprite1 = positivePosts.Single(post => post.Category == winningCateg).Sprite;
            Sprite posSprite2 = positivePosts.First(post => post.Category != winningCateg).Sprite;
            
            Sprite secretSprite = secretPosts[0].Sprite;
            CreateSecretPostComments(secretPosts[0]);

            Sprite[] negSprites = negativePosts.Take(_amountOfNegativePosts).ToArray();
            
            _finalPosts = new SocialMediaPost[negSprites.Length + 3];

            for (int i = 0; i < negSprites.Length + 3; i++)
            {
                SocialMediaPost post = Instantiate(_postPrefab);
                
                if (i < negSprites.Length) post.Init(PostType.Negative, negSprites[i], this);

                if (i == negSprites.Length) post.Init(PostType.Positive, posSprite1, this, true);
                if (i == negSprites.Length + 1) post.Init(PostType.Positive, posSprite2, this);
                if (i == negSprites.Length + 2) post.Init(PostType.Secret, secretSprite, this);

                _finalPosts[i] = post;
            }
            
            rng.Shuffle(_finalPosts);
            
            foreach (SocialMediaPost post in _finalPosts)
            {
                post.transform.SetParent(_SM.Content);
            }

            foreach (Comment comment in _finalComments)
            {
                comment.transform.SetParent(_SM.Content);
                comment.gameObject.SetActive(false);
            }
        }

        void CreateSecretPostComments(SecretPost post)
        {
            foreach (CommentData data in post.Comments)
            {
                Comment comment = Instantiate(_commentPrefab);
                comment.Init(data.Sprite, this, data.IsWinningComment);
                _finalComments.Add(comment);
            }
        }
    }

    public void ChangeSecretPostState(bool isOpen)
    {
        foreach (SocialMediaPost post in _finalPosts)
        {
            if (post.Type != PostType.Secret) post.gameObject.SetActive(!isOpen);
        }

        foreach (Comment comment in _finalComments)
        {
            comment.gameObject.SetActive(isOpen);
        }
    }
}
