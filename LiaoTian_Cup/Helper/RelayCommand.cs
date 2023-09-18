using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LiaoTian_Cup.Helper
{
    //命令绑定类
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// 命令是否能够执行
        /// </summary>
        readonly Func<bool> _canExecute;

        /// <summary>
        /// 命令需要执行的方法
        /// </summary>
        readonly Action<object> _execute;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="action"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action<object> action, Func<bool> canExecute)
        {
            _execute = action;
            _canExecute = canExecute;
        }
       

        public bool CanExecute(object parameter)
        {
            if(_canExecute == null)
            {
                return true;
            }
            return _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if(_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }
    }
}
