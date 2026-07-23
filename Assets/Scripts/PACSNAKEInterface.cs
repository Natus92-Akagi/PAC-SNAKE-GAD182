using UnityEngine;

    public interface IPacSnakeable
    {
        void Grow();
        void Move(Vector2 direction, GameObject tail);

    }
    public interface ICollisionable
    {
        void OnCollisionEnter2D(Collision2D collision);
    }
    public interface IInputable
    {
        Vector2 GetInput();
    }
    public interface IScoreable
    {
        int GetScore();
        void AddScore(int score);
    }
    public interface IModeable
    {
        void ChangeMode();
        string GetMode();
    }
    public interface IGameStateable
    {
        void StartGame();
        void PauseGame();
        void ResumeGame();
        void EndGame();
    }
    public interface IGhostInteractable
    {
        void OnGhostCollision();
        void Flee();
 
    }
    public interface IPelletInteractable
    {
        void OnPelletCollision();
        void ConsumePellet();
    }
    public interface IWallInteractable
    {
        void OnWallCollision();       
    }
    public interface IFruitInteractable
    {
        void OnFruitCollision();
        void ConsumeFruit();
        
    }
    public interface IDeathable
    {
        void Die();
        void Respawn();
    }





