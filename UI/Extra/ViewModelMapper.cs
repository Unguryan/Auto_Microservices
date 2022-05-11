using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using UI.Interfaces;

namespace UI.Extra
{
    public class ViewModelMapper : IViewModelMapper
    {
        private readonly IServices _services;

        private IDictionary<Type, Type> _mappedTypes;

        public ViewModelMapper(IServices services)
        {
            _mappedTypes = new Dictionary<Type, Type>();
            _services = services;
        }

        public void RegisterViewModel(Type viewModel, Type view)
        {
            _mappedTypes.Add(viewModel, view);
        }

        public UserControl GetViewByViewModelType(Type viewModelType)
        {
            if (!_mappedTypes.Keys.Contains(viewModelType))
            {
                return null;
            }

            var viewType = _mappedTypes[viewModelType];

            return (UserControl)Activator.CreateInstance(viewType);
        }

        public IViewModel GetViewModelByType(Type viewModelType)
        {
            if (!_mappedTypes.Keys.Contains(viewModelType))
            {
                return null;
            }

            return (IViewModel)Activator.CreateInstance(viewModelType, _services);
        }
    }
}
