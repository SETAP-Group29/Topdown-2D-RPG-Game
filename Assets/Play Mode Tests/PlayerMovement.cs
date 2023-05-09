using System.Collections;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Play_Mode_Tests
{
    [TestFixture]
    public class PlayerMovement 
    {

        [OneTimeSetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("MainScene");
        }

        public void Press(Keyboard keyBoard, Key key)
        {
            using (StateEvent.From(keyBoard, out var eventPtr))
            {
                unsafe
                {
                    keyBoard.wKey.WriteValueIntoState(1f, eventPtr);
                    InputSystem.QueueEvent(eventPtr);
                    InputSystem.Update();
                }
            }
        }

        private void SetMovement(PlayerController controller, float newX, float newY)
        {
            FieldInfo info = typeof(PlayerController)
                .GetField("_movement", BindingFlags.Instance | BindingFlags.NonPublic);
            info.SetValue(controller, new Vector2(newX, newY));
        }
        

        [UnityTest]
        public IEnumerator _Player_Moves()
        {
            PlayerController controller = GameObject.FindObjectOfType<PlayerController>();
            
            //Assert.IsNotNull(controller);
            
            Debug.Log(controller.transform.position.y);
            Debug.Log(controller.transform.position.x);
            SetMovement(controller, 0f, 10f);
            yield return new WaitForSeconds(1f);
            Debug.Log(controller.transform.position.y);
            Debug.Log(controller.transform.position.x);
            yield return null;
        }
    }
    
}