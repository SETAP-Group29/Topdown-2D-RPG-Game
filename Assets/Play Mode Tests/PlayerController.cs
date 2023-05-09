using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Play_Mode_Tests
{
    [TestFixture]
    public class PlayerMovement : InputTestFixture
    {

        private Keyboard _keyboard;
        
        [SetUp]
        public void  SetUp()
        {
            SceneManager.LoadScene("MainScene");
            _keyboard = InputSystem.AddDevice<Keyboard>();
        }

        /*public void Press(Keyboard keyBoard, Key key)
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
        }*/

        private void SetMovement(PlayerController controller, float newX, float newY)
        {
            // FieldInfo info = typeof(PlayerController)
            //     .GetField("_movement", BindingFlags.Instance | BindingFlags.NonPublic);
            // Vectoer
            // info.SetValue(controller, new Vector2(newX, newY));
            controller._movement.x = newX;
            controller._movement.y = newY;
        }
        

        [UnityTest]
        public IEnumerator _Player_Moves_Up()
        {
            PlayerController controller = GameObject.FindObjectOfType<PlayerController>();
            Vector2 lastPosition = controller.transform.position;
            controller.testingDontMove = true;
            SetMovement(controller, 0, 1f);
            // Press(_keyboard.wKey);
            yield return new WaitForSeconds(0.1f);
            // Release(_keyboard.wKey);
            SetMovement(controller, 0, 0);
            
            float diffX = controller.transform.position.x - lastPosition.x;
            float diffY = controller.transform.position.y - lastPosition.y;
            Debug.Log(diffX);
            Debug.Log(diffY);
            Assert.IsTrue(diffY > 0);
            Assert.IsFalse(diffX != 0);
            

            yield return null;
        }
        
        [UnityTest]
        public IEnumerator _Player_Moves_Down()
        {
            
            PlayerController controller = GameObject.FindObjectOfType<PlayerController>();
            Vector2 lastPosition = controller.transform.position;
            SetMovement(controller, 0, -1f);
            yield return new WaitForSeconds(0.1f);
            SetMovement(controller, 0, 0);
            
            float diffX = controller.transform.position.x - lastPosition.x;
            float diffY = controller.transform.position.y - lastPosition.y;
            Debug.Log(diffX);
            Debug.Log(diffY);
            Assert.IsTrue(diffY < 0);
            Assert.IsFalse(diffX != 0);
            
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator _Player_Moves_Right()
        {
            PlayerController controller = GameObject.FindObjectOfType<PlayerController>();
            Vector2 lastPosition = controller.transform.position;
            SetMovement(controller, 1f, 0);
            yield return new WaitForSeconds(0.1f);
            SetMovement(controller, 0, 0);
            
            float diffX = controller.transform.position.x - lastPosition.x;
            float diffY = controller.transform.position.y - lastPosition.y;
            Debug.Log(diffX);
            Debug.Log(diffY);
            Assert.IsTrue(diffX > 0);
            Assert.IsFalse(diffY != 0);
            
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator _Player_Moves_Left()
        {
            
            PlayerController controller = GameObject.FindObjectOfType<PlayerController>();
            Vector2 lastPosition = controller.transform.position;
            SetMovement(controller, -1f, 0);
            yield return new WaitForSeconds(0.1f);
            SetMovement(controller, 0, 0);
            
            float diffX = controller.transform.position.x - lastPosition.x;
            float diffY = controller.transform.position.y - lastPosition.y;
            Debug.Log(diffX);
            Debug.Log(diffY);
            Assert.IsTrue(diffX < 0);
            Assert.IsFalse(diffY != 0);
            
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator _Player_Attacks()
        {
            PlayerController controller = GameObject.FindObjectOfType<PlayerController>();
            controller.Attack();
            yield return new WaitForSeconds(0.1f);
            Assert.IsTrue(controller.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
            
            yield return null;
        }
    }
    
}