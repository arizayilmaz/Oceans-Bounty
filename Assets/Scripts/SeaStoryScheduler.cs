// SeaStoryScheduler.cs (Şakalar ve hikayeler 2 ayrı dizide sırayla gösterilir, döngülüdür)
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SeaStoryScheduler : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] storySegments = new string[]
    {
        "The sea always told me it loved me, but that love turned into a strange kind of curse. I can no longer set foot on any island; the moment I touch land, my body writhes in unbearable pain.",
        "It’s as if the sea has claimed me for itself forever. I used to believe I was free, but now I understand—the sea has chained me, and it will never let me go.",
        "I am alone on my ship, with nothing but my gold, a few pieces of meat, and strange artifacts collected from mysterious sea creatures.",
        "Everything I have revolves around trade on this ocean. Still, I miss people—their warmth, their laughter, even their petty arguments.",
        "When I look toward the horizon from the deck, the islands I see fill me with both hope and sorrow. Even if I can't set foot there, I know I can still touch the lives of those who live on them.",
        "With the fame I’ve earned through trade and raids, they see me as a hero. Perhaps I am, or maybe I'm just deceiving myself to ease my own loneliness.",
        "Today, I approached the market zone once again. The sea is calm, but the waves within me are not. My gold is dwindling, and so is my meat supply.",
        "Every key I press feels like a desperate exchange to keep surviving.",
        "Another island rises in the distance. I don’t know its strength yet, but I’m prepared. Villager, Archer, Swordsman... these names are more than just numbers to me now; in my mind, they carry the faces of friends.",
        "When I send them off, it feels like I’m going myself, like I’m fighting. Winning this battle will make the islanders look at me with gratitude. Maybe the path to lifting my curse lies in their prayers.",
        "Every raid is a test—not just of my physical strength, but of my patience and belief. When the countdown begins, my heart races. Sixty seconds sometimes feel like an eternity. I am ready, waiting, clinging to hope.",
        "And with every reward—whether it’s a starfish or a jellyfish—my hope is renewed. These little things may seem meaningless to others, but to me, they are life itself.",
        "Sometimes, I have to retreat. The decision isn't easy. It’s not about reclaiming my meat, it's about the blow to my pride. These retreats weigh heavily on me, but I’ve learned that to survive, sometimes you must take a step back.",
        "People think I'm a hero. I wish they knew the real battle within me. I’m fighting the sea, fighting my loneliness, my longing. ",
        "One day, maybe my curse will be lifted, and I’ll step onto land and truly speak to people again. Until then, I’ll continue this strange journey of heroism.",
        "The sea says it loves me—perhaps it truly does—but its love suffocates me. Still, I will press on. Because as I stand on the deck of my ship, gazing into the endless sea, I know one thing for sure: I will not give up.",
        "I remember why I set sail in the first place. To save my family—my little girl and my wife. Our village was plagued by famine after a great storm, and I was seeking a way to save them from starvation.",
        "But the sea was cruel; the storm threw me from my ship. That day, the sea saved my life in exchange for taking it forever. When my ship washed ashore, I heard they had survived—but I never saw them again. The sea, while saving me, imprisoned me in eternal solitude. Perhaps this is a punishment for failing to protect them.",
        "Now, I am bound to this sea forever—alone, but hopeful. Because I know as long as they still breathe, I have a reason to keep fighting. Maybe one day I’ll be forgiven, and I’ll see them again. Until then, the sea will be my fate.",
        "And now, another island appears on the horizon, full of new hopes and challenges. Perhaps this time will be different. Or perhaps I am trapped in the same cycle once more. But one thing I am certain of—I will never stop fighting, and I will never let go of hope.",
        "The howl of the wind was different that night. The roof of our house shook, and the storm seemed to be screaming. My wife held our little daughter in her arms, praying by the window. Meanwhile, I was in the middle of the chaos that had erupted in town.",
        "A group of starving people had broken into the grain storehouse. As I tried to help, a man shoved me—I fell and hit my head on a stone step. When I woke up, it was too late. Our house was in flames, and half the village was in ruins.",
        "My daughter’s blanket had washed ashore. I couldn’t find my wife or daughter. No grave, no trace... Only the cursed silence the sea had brought with it. ",
        "That night, I set sail alone, carrying the weight of my losses. I had no destination. The storm returned. Waves devoured my ship, and as I struggled in the water, a dark silhouette approached.",
        "She was a mermaid. Her eyes held both sorrow and mercy.You’ve lost them,” she said, her voice flowing like water.",
        "But you are still alive. This world has nothing left for you. But there is another way. Bind your soul to the sea. Become immortal. Sail the open waters forever. If you forget your past and focus on your purpose, perhaps one day, you will see them again.",
        "I didn’t hesitate. I was already as good as dead.\r\n“I accept,” I said. And then everything changed.",
        "I was no longer drowning. When I opened my eyes, I had a ship of my own. The deck shimmered. Gold, provisions, and traces of sea creatures filled the inventory. But no shore, no island, would accept me anymore.",
        "Since that day, I’ve been on an endless journey. Trading, raiding, pretending to be a hero for others. But inside, I feel nothing. My only motivation is the faint hope that, as the mermaid promised, I might one day catch a glimpse of my past and see them once more.",
        "Maybe the sea still loves me. But I am no longer the man who was once loved. I am now just a captain—immortal, drifting in the shadow of his losses."
    };

    [TextArea(3, 10)]
    public string[] sailorJokes = new string[]
    {
        "I talk to the ship now… It's a bit rusty, a bit broken—just like me.",
        "I thought I’d sink to the bottom of the sea, turns out I’m stuck sailing on top of it for years. I’m not cargo, damn it!",
        "I used to tell bedtime stories to my kids… Now I tell them to seaweed. Same silence.",
        "Every time I see a port, my heart stirs… but my body’s still cursed, and my feelings are trapped aboard this ship.",
        "I once said I wanted to be alone—but this is too much. Even the sea tells me, ‘You’re too alone.’",
        "They say listening to the wind soothes the soul… But I’ve never met a wind this gossipy.",
        "Every island taken without a fight reminds me of my old home… Quiet, warm, and now far away.",
        "They said a ship should never be without a captain… Well, I’ve got no soul, yet here I am.",
        "Saw a lighthouse last night… Thought it winked at me. Turns out it was lit for a passing seagull.",
        "I greeted some seagulls—They turned and said, ‘Bro, what year is this again?’ No one kept count. They did.",
        "I ponder philosophy on the open seas. Like: if no one’s around, is the captain’s joke still a joke?",
        "It’s not just the magnetic field messing up my compass—it’s the mess inside me.",
        "The mermaid said, ‘Be immortal’… I wish I’d asked if the soul ages too.",
        "Dropped the sails today. Like the ship, I’m motionless.",
        "Sometimes I count my gold. The more I count, the more I realize what I’ve lost.",
        "Life is like a storm… But mine blows somewhere off the map now.",
        "I want to joke about waves… but they already roll right over me.",
        "I try to pep myself up by looking in the mirror. Problem is, at sea there are no mirrors—just reflections. And mine’s shattered.",
        "I never named the ship… because every day it feels like a different memory.",
        "The sea gave me eternity—but who wants to spend eternity alone?","Another stormy day… Not that it can mess up my hair—my soul’s waves are messier anyway.",
        "My ship and I have both lost our way. At least I’m not alone in that.",
        "I talk to starfish now… They don’t reply either, but at least they don’t swim away.",
        "What does a pirate do when he loses his lover? He slams his hand on the compass… I did, but still can’t find my direction.",
        "When the storm of loneliness hits, I wrap myself like a rope—at least something’s holding me together.",
         "One day I made a deal with a mermaid. Still paying for it. The contract had eternity written in fine print.",
        "Sometimes I ask, 'Is anyone on this ship?' No answer. Guess I’m still the captain.",        "Even the sharks don’t fear me anymore… I think they’re tired of the drama too.",
        "If the ship takes on water, I won’t sink. I already drowned inside years ago.",
        "I draw routes, but my heart doesn’t follow the compass. Turns out a broken heart distorts magnetic fields too.",
        "Can’t step on land, remember? So my love life’s docked at port too.",
        "If I had a crew, at least they wouldn’t laugh at my jokes. At least someone would stay silent.",
        "The mermaid gave me immortality but said no Netflix. I accepted. At least my drama’s uninterrupted.",
        "Gold, meat, a ship… But happiness? That’s not marked on any map.",
        "Sometimes I curse the storm… Then I remember, the curse came with me.",
        "I try talking to seashells. At least they mean it when they say, 'listen to the past.'",
        "I still carry my daughter’s blanket. It’s seen more of the world than I have.",
        "I make sailor jokes and laugh at them myself. No one else around to do it anyway.",
        "I gave my soul, got a ship in return. Thought I was buying life on credit—turns out it was a mortgage.",
        "You know what I miss the most? Not the waves… but the way someone’s voice used to hit me.",
    };


    public TMP_Text storyText;
    public TMP_Text jokeText;
    public TMP_Text storyPanelText; // Panel içindeki büyük hikaye gösterimi
    public GameObject storyPanelUI; // Açılıp kapanabilir panel

    public GameObject marketPanel; // Market paneli referansı
    public GameObject battlePanel; // Baskın paneli referansı

    private float storyDelay = 1800f; // 30 dakika
    private float jokeDelay = 1200f;  // 20 dakika
    private float displayDuration = 15f;

    private int storyIndex = 0;
    private int jokeIndex = 0;

    private List<string> collectedStorySegments = new List<string>();
    private int currentPage = 0;

    private void Start()
    {
        if (storySegments == null || storySegments.Length == 0 ||
            sailorJokes == null || sailorJokes.Length == 0)
        {
            Debug.LogWarning("Hikaye veya şaka dizisi boş!");
            return;
        }

        if (storyPanelUI != null)
            storyPanelUI.SetActive(false);

        StartCoroutine(JokeRoutine());
        StartCoroutine(DelayedStoryRoutine());
    }

    private void Update()
    {
        bool otherPanelsOpen = (marketPanel != null && marketPanel.activeSelf) ||
                               (battlePanel != null && battlePanel.activeSelf);

        if (Input.GetKeyDown(KeyCode.Alpha2) && !otherPanelsOpen)
        {
            if (storyPanelUI != null)
                storyPanelUI.SetActive(!storyPanelUI.activeSelf);

            if (storyPanelUI.activeSelf)
                ShowCurrentPage();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && storyPanelUI != null && storyPanelUI.activeSelf)
        {
            storyPanelUI.SetActive(false);
        }

        if (storyPanelUI != null && storyPanelUI.activeSelf)
        {
            int maxPage = Mathf.Max((collectedStorySegments.Count - 1) / 2, 0);

            // ← ok veya '1' ile önceki sayfa
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentPage = Mathf.Max(currentPage - 1, 0);
                ShowCurrentPage();
            }
            // → ok veya '3' ile sonraki sayfa
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentPage = Mathf.Min(currentPage + 1, maxPage);
                ShowCurrentPage();
            }
        }
    }

    private IEnumerator DelayedStoryRoutine()
    {
        yield return new WaitForSeconds(300f); // 5 dakika gecikme
        StartCoroutine(StoryRoutine());
    }

    private IEnumerator StoryRoutine()
    {
        while (true)
        {
            string segment = storySegments[storyIndex];
            storyText.text = segment;
            collectedStorySegments.Add(segment);

            storyIndex = (storyIndex + 1) % storySegments.Length;

            yield return new WaitForSeconds(displayDuration);
            storyText.text = "";

            yield return new WaitForSeconds(storyDelay - displayDuration);
        }
    }

    private IEnumerator JokeRoutine()
    {
        while (true)
        {
            jokeText.text = sailorJokes[jokeIndex];
            jokeIndex = (jokeIndex + 1) % sailorJokes.Length;

            yield return new WaitForSeconds(displayDuration);
            jokeText.text = "";

            yield return new WaitForSeconds(jokeDelay - displayDuration);
        }
    }

    private void ShowCurrentPage()
    {
        int firstIndex = currentPage * 2;
        int secondIndex = firstIndex + 1;
        string pageText = "";

        if (firstIndex < collectedStorySegments.Count)
            pageText += collectedStorySegments[firstIndex] + "\n\n";

        if (secondIndex < collectedStorySegments.Count)
            pageText += collectedStorySegments[secondIndex];

        storyPanelText.text = pageText;
    }
}

