using System;
using System.Collections.Generic;
using System.Linq;
using State;
using Buffer = State.Buffer;

namespace Quests
{
    /// <summary>
    /// Handles all of the save serialization and deserialization.
    /// </summary>
    public class QuestSaveHandler : SaveHandler<Quest>
    {
        public QuestSaveHandler(string type) : base(type)
        {
        }

        /// <summary>
        /// Create a quest instance from a serialized quest buffer.
        /// </summary>
        /// <param name="saveData"></param>
        /// <returns></returns>
        public override Quest LoadFromSave(Buffer saveData)
        {
            string questName = saveData.ReadString();
            Dictionary<string, bool> questSteps = new Dictionary<string, bool>();
            int amountOfSteps = saveData.ReadInt();
            for (int i = 0; i < amountOfSteps; i++)
            {
                string questStep = saveData.ReadString();
                bool questStepComplete = saveData.ReadBoolean();
                questSteps.Add(questStep, questStepComplete);
            }

            return new Quest(questName, questSteps);
        }

        /// <summary>
        /// Quest save implementation.
        /// </summary>
        /// <param name="instance"> the quest instance</param>
        /// <returns> a quest serialized as a buffer. </returns>
        public override Buffer Serialize(Quest instance)
        {
            Buffer buffer = new Buffer();
            buffer.WriteString(instance.GetName());
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