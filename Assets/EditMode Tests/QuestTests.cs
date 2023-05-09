using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Quests;
using UnityEngine;
using UnityEngine.TestTools;

public class QuestTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void _Quest_Retains_Progress()
    {
        Quest quest = new Quest("Example", new Dictionary<string, bool>
        {
            {"Step 1", false},
            {"Step 2", false}
        });
        Assert.IsTrue(quest.GetPendingSteps().Contains("Step 1"));
        quest.SetQuestStep("Step 1");
        Assert.IsFalse(quest.GetPendingSteps().Contains("Step 1"));
    }

    [Test]
    public void _Quest_Has_Correct_Pending_Steps()
    {
        Quest quest = new Quest("Example", new Dictionary<string, bool>
        {
            {"Step 1", false},
            {"Step 2", false}
        });
        List<string> steps = quest.GetPendingSteps();
        
        Assert.IsTrue(steps.Contains("Step 1"));
        Assert.IsTrue(steps.Contains("Step 2"));
        Assert.IsTrue(steps.Count == 2);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
