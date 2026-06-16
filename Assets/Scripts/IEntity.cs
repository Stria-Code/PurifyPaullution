using UnityEngine;

public interface IEntity
{
    public void MoveEntity();
    public void OnCollisionEnter2D(Collision2D collision);
}
