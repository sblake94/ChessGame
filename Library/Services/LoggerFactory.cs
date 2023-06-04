using Library.Attributes.ServiceAttributes;
using Library.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    [SingletonService]
    public class LoggerFactory : ILoggerFactory
    {
        private readonly Dictionary<string, ILogger> _categories = new Dictionary<string, ILogger>();

        public void AddProvider(ILoggerProvider provider)
        {
            throw new NotImplementedException();
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (!_categories.TryGetValue(categoryName, out var _))
            {
                _categories.Add(categoryName, new DefaultLogger());
            }

            return _categories[categoryName];
        }

        public void Dispose()
        {
            // Dispose any disposable providers
            foreach (var provider in _categories.Values.OfType<IDisposable>())
            {
                provider.Dispose();
            }

            _categories.Clear();
        }
    }
}
