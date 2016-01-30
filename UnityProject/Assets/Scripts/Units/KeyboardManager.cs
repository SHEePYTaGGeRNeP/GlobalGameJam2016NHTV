namespace Assets.Scripts.Units
{
    using UnityEngine;

    internal class KeyboardManager
    {

        public static float GetPlayerHorizontal(PlayerMovement.PlayerNumber player)
        {
            if (player == PlayerMovement.PlayerNumber.P1)
            {
                if (Input.GetKey(KeyCode.A))
                    return -1f;
                if (Input.GetKey(KeyCode.D))
                    return 1f;
            }
            else if (player == PlayerMovement.PlayerNumber.P2)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                    return -1f;
                if (Input.GetKey(KeyCode.RightArrow))
                    return 1f;
            }
            return 0f;
        }
        public static float GetPlayerVertical(PlayerMovement.PlayerNumber player)
        {
            if (player == PlayerMovement.PlayerNumber.P1)
            {
                if (Input.GetKey(KeyCode.W))
                    return -1f;
                if (Input.GetKey(KeyCode.S))
                    return 1f;
            }
            else if (player == PlayerMovement.PlayerNumber.P2)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                    return -1f;
                if (Input.GetKey(KeyCode.DownArrow))
                    return 1f;
            }
            return 0f;
        }


    }
}
