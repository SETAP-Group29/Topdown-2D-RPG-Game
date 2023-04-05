using System;
using System.Collections.Generic;
using System.Linq;
using State;
using Buffer = State.Buffer;

namespace Quests
{
    public class QuestSaveHandler : SaveHandler<Quest>
    {
        public QuestSaveHandler(string type) : base(type)
        {
        }

        public override Quest LoadFromSave(Buffer saveData)
        {
            Dictionary<string, bool> questSteps = new Dictionary<string, bool>();
             
            int amountOfSteps = saveData.ReadInt();
            for (int i = 0; i < amountOfSteps; i++)
            {
                string questStep = saveData.ReadString();
                bool questStepComplete = saveData.ReadBoolean();
                questSteps.Add(questStep, questStepComplete);
            }

            return new Quest(questSteps);
        }

        public override Buffer Serialize(Quest instance)
        {
            Buffer buffer = new Buffer();
            var steps = instance.GetQuestStepState().ToList();
            buffer.WriteInt(steps.Count);
            for (int i = 0; i < steps.Count; i++)
            {
                var kvp = steps[i];
                buffer.WriteString(kvp.Key);
                buffer.WriteBoolean(kvp.Value);
            }

            return buffer;
        }
    }
}