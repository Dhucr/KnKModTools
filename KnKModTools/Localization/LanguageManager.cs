using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows;

namespace KnKModTools.Localization
{
    public static class LanguageManager
    {
        private static ResourceDictionary _currentLanguageDictionary;
        private static string key = "";
        private static readonly Dictionary<string, ResourceDictionary> _languageDictionaries = new Dictionary<string, ResourceDictionary>();

        public static event EventHandler LanguageChanged;

        public static void Initialize(string cultureCode)
        {
            // 注册默认语言
            RegisterLanguage("zh-CN", new Uri("/Resources/Strings.zh.xaml", UriKind.Relative));
            RegisterLanguage("ja-JP", new Uri("/Resources/Strings.ja.xaml", UriKind.Relative));
            RegisterLanguage("ko-KR", new Uri("/Resources/Strings.ko.xaml", UriKind.Relative));
            RegisterLanguage("en-US", new Uri("/Resources/Strings.en.xaml", UriKind.Relative));

            // 设置默认语言
            SetLanguage(cultureCode);
        }

        public static void RegisterLanguage(string cultureCode, Uri resourceUri)
        {
            if (_languageDictionaries.ContainsKey(cultureCode))
                return;

            var dict = new ResourceDictionary { Source = resourceUri };
            _languageDictionaries.Add(cultureCode, dict);
        }

        public static void SetLanguage(string cultureCode)
        {
            if (key == cultureCode) return;
            if (!_languageDictionaries.TryGetValue(cultureCode, out var newDictionary))
                return;

            key = cultureCode;

            if (_currentLanguageDictionary != null)
            {
                // 从应用程序资源中移除当前语言
                Application.Current.Resources.MergedDictionaries.Remove(_currentLanguageDictionary);
            }

            // 添加新语言资源
            Application.Current.Resources.MergedDictionaries.Add(newDictionary);
            _currentLanguageDictionary = newDictionary;

            // 更新框架语言设置
            /*FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));*/

            // 触发语言更改事件
            LanguageChanged?.Invoke(null, EventArgs.Empty);
        }

        public static string GetString(string key)
        {
            if (_currentLanguageDictionary != null && _currentLanguageDictionary.Contains(key))
            {
                return _currentLanguageDictionary[key] as string;
            }
            return key; // 如果找不到资源，返回键名
        }
    }
}
