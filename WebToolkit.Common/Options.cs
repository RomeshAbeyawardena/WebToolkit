using System;

namespace WebToolkit.Common
{
    public static class Options
    {
        public static Options<TOptions> Create<TOptions>(Action<TOptions> optionsAction)
        {
            return Options<TOptions>.Create(optionsAction);
        }
    }

    public sealed class Options<TOptions>
    {
        public static Options<TOptions> Create(Action<TOptions> optionsAction)
        {
            return new Options<TOptions>(optionsAction);
        }

        public void SetOptions(TOptions options)
        {
            _optionsAction(options);
        }

        private Options(Action<TOptions> optionsAction)
        {
            _optionsAction = optionsAction;
        }

        private readonly Action<TOptions> _optionsAction;
    }
}