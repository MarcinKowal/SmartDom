using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDom.Service.Interface
{
    public interface IMediaAdapter<T> : IDisposable
    {
        T Media { get; }
        void Initialize();
    }
}
