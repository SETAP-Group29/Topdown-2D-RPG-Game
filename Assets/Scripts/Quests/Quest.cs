using System;
using System.Collections.Generic;
using System.Linq;
using State;

namespace Quests
{
    /// <summary>
    /// Used to store the state of a quest
    /// </summary>
    public class Quest
    {

        private string _name;
        private bool _needsSave;
        
        private Dictionary<string, bool> _questSteps;
        private static SaveHandler<Quest> _handler = new QuestSaveHandler("Quest");
        
        /// <summary>
        /// Create a new instance of a quest
        /// </summary>
        /// <param name="name">the name of the quest</param>
        /// <param name="steps">the steps in the quest, and if they've been achieved.</param>
        public Quest(string name, Dictionary<string, bool> steps)
        {
            _questSteps = steps;
            _name = name;
        }

        
        /// <summary>
        /// Get current quest progress
        /// </summary>
        /// <returns>A dictionary of quests progresses and whether they've been complete.</returns>
        public Dictionary<string, bool> GetQuestStepState()
        {
            return _questSteps;
        }

        /// <summary>
        /// Marks a certain step as complete.
        /// </summary>
        /// <param name="questStep">the step name</param>
        /// <exception cref="NullReferenceException"> if a step doesn't exist.</exception>
        public void SetQuestStep(String questStep) 
        {
            if (!_questSteps.ContainsKey(questStep))
            {
                throw new NullReferenceException("Quest step not found");
            }

            _questSteps[questStep] = true;
            _needsSave = true;
        }

        /// <summary>
        /// Get all incomplete steps of a quest.
        /// </summary>
        /// <returns>a list of incomplete steps.</returns>
        public List<String> GetPendingSteps()
        {
            return _questSteps.Where((kvp) => !kvp.Value).Select((pair => pair.Key)).ToList();
        }

        /// <summary>
        /// Save the current quest state.
        /// </summary>
        public void Save()
        {
            if (!_needsSave)
            {
                // Maybe throw an exception here?
                return;
            }
            _handler.Save(this, _name);
        }

        public string GetName()
        {
            return _name;
        }

    }
}