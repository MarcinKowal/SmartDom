using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartDom.Service.Interface;
using SmartDom.Service.MediaAdapters;

namespace SmartDom.Service
{
    public abstract class ModbusMasterFactory<T>
    {
        public abstract IModbusMasterAdapter Create(MediaAdapter<T> media);
    }

    public class RtuModbusMasterFactory : ModbusMasterFactory<SerialPort>
    {
        public override IModbusMasterAdapter Create(MediaAdapter<SerialPort> media)
        {
            media.Initialize();
            return new RtuModbusMaster(media);
        }
    }
}
