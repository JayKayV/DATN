namespace SharedLibrary.BaseGameObject.Interfaces
{
    internal interface ICollidable
    {
        public void OnCollisionEnter(GameObject other);
        public void OnCollisionExit(GameObject other);
    }
}
