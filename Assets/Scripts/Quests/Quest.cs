using System;
using System.Collections.Generic;
using System.Linq;
using State;

namespace Quests
{
    public class Quest
    {

        private string _name;
        private Dictionary<string, bool> _questSteps;
        private bool _needsSave = false;
        private static SaveHandler<Quest> _handler = new QuestSaveHandler("Quest");
        public Quest(string name, Dictionary<string, bool> steps)
        {
            _questSteps = steps;
            _name = name;
        }

        public Dictionary<string, bool> GetQuestStepState()
        {
            return _questSteps;
        }

        public void SetQuestStep(String questStep) 
        {
            if (_questSteps.ContainsKey(questStep))
            {
                throw new NullReferenceException("Quest step not found");
            }

            _questSteps[questStep] = true;
            _needsSave = true;
        }

        public List<String> GetPendingSteps()
        {
            return _questSteps.Where((kvp) => !kvp.Value).Select((pair => pair.Key)).ToList();
        }

        public void Save()
        {
            if (!_needsSave)
            {
                // Maybe throw an exception here?
                return;
            }
            _handler.Save(this, _name);
        }

    }
}