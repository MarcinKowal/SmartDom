using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartDom.Service.Interface;

namespace SmartDom.Service.MediaAdapters
{
    public abstract class MediaAdapterFactory<T>
    {
        public abstract MediaAdapter<T> Create();
    }

    public class SerialAdapterFactory : MediaAdapterFactory<SerialPort>
    {
        public IConfigurationRepository ConfigurationRepository { get; set; }

        public SerialAdapterFactory(IConfigurationRepository configurationRepository)
        {
            this.ConfigurationRepository = configurationRepository;
        }

        public override MediaAdapter<SerialPort> Create()
        {
            return new MediaAdapter(ConfigurationRepository);
        }
    }
}
