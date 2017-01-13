public interface IItemUsable
{
    bool CanUseOnSelf(int itemId);
    void UseOnSelf(int itemId);
}