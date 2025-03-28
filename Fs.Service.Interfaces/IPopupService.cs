namespace Fs.Service.Interfaces
{
    public interface IPopupService
    {
        /// <summary>
        /// 获取页面实例
        /// </summary>
        /// <returns></returns>
        object GetInstance();
        /// <summary>
        /// 重新绑定弹窗对象的viewModel
        /// </summary>
        /// <param name="viewModel"></param>
        void SetDataContext(object viewModel);
        /// <summary>
        /// 调用页面的弹出方法（如果有的话）
        /// </summary>
        void Show();
        /// <summary>
        /// 调用页面的关闭方法（如果有的话）
        /// </summary>
        void Close();
        /// <summary>
        /// 调用页面的隐藏方法（如果有的话）
        /// </summary>
        void Hide();
    }
}
