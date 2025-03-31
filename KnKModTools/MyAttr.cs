using KnKModTools.Localization;
using System.Windows;

namespace KnKModTools
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class FieldIndexAttr : Attribute
    {
        public int Index { get; set; }

        public FieldIndexAttr(int index)
        {
            Index = index;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class BinStreamAttr : Attribute
    {
        public int Length { get; set; }       // 固定长度
        public string LengthRef { get; set; } // 引用其他属性的名称

        public string SubHeaderName { get; set; }
        public bool UseSubHeader { get; set; }

        public BinStreamAttr(int length = 0, string lengthRef = null)
        {
            Length = length;
            LengthRef = lengthRef;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class UIDisplayAttribute : Attribute
    {
        private string _resourceKey;

        public string DisplayName => LanguageManager.GetString(_resourceKey);

        public bool UseLink { get; set; }

        public string Link { get; set; }

        public UIDisplayAttribute(string name = null, bool useLink = false, string link = null)
        {
            _resourceKey = name;
            UseLink = useLink;
            Link = link;
        }

        public string GetDisplayName()
        {
            if (Application.Current.Resources.Contains(_resourceKey))
            {
                return Application.Current.Resources[_resourceKey] as string;
            }
            return _resourceKey; // 如果找不到资源，返回键本身作为默认值
        }
    }
}