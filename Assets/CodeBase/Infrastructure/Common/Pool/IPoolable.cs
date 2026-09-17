namespace CodeBase.Infrastructure.Common.Pool
{
    public interface IPoolable
    {
        public bool IsActiveInHierarchy { get;  }
        public void SetActive(bool value);
    }
}