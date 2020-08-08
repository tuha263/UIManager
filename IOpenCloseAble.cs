using UnityEngine.Events;

namespace Modules.UIManager
{
    public interface IOpenCloseAble
    {
        void open();
        void close(UnityAction callback);
    }
}