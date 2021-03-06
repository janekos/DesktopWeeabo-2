﻿using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace DesktopWeeabo2.CustomControls {

	public class InfoBlock : Control {

		static InfoBlock() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(InfoBlock), new FrameworkPropertyMetadata(typeof(InfoBlock)));
		}

		public object DynamicItem {
			get { return GetValue(DynamicItemProperty); }
			set { SetValue(DynamicItemProperty, value); }
		}

		public static readonly DependencyProperty DynamicItemProperty = DependencyProperty.Register("DynamicItem", typeof(object), typeof(InfoBlock), new PropertyMetadata(null, OnTextChanged));

		private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var pattern = (string) d.GetValue(PatternProperty);
			if (pattern != null && e.NewValue != null && !new Regex(pattern).IsMatch(e.NewValue.ToString()))
				d.SetValue(DynamicItemProperty, e.OldValue);
		}

		public string Pattern {
			get { return (string) GetValue(PatternProperty); }
			set { SetValue(PatternProperty, value); }
		}

		public static readonly DependencyProperty PatternProperty = DependencyProperty.Register("Pattern", typeof(string), typeof(InfoBlock), new PropertyMetadata(null));

		public string StaticText {
			get { return (string) GetValue(StaticTextProperty); }
			set { SetValue(StaticTextProperty, value); }
		}

		public static readonly DependencyProperty StaticTextProperty = DependencyProperty.Register("StaticText", typeof(string), typeof(InfoBlock), new PropertyMetadata(null));

		public string DynamicTextEnding {
			get { return (string) GetValue(DynamicTextEndingProperty); }
			set { SetValue(DynamicTextEndingProperty, value); }
		}

		public static readonly DependencyProperty DynamicTextEndingProperty = DependencyProperty.Register("DynamicTextEnding", typeof(string), typeof(InfoBlock), new PropertyMetadata(null));

		public string PlaceHolder {
			get { return (string) GetValue(PlaceHolderProperty); }
			set { SetValue(PlaceHolderProperty, value); }
		}

		public static readonly DependencyProperty PlaceHolderProperty = DependencyProperty.Register("PlaceHolder", typeof(string), typeof(InfoBlock), new PropertyMetadata(null));

		public TextAlignment TextAlignment {
			get { return (TextAlignment) GetValue(TextAlignmentProperty); }
			set { SetValue(TextAlignmentProperty, value); }
		}

		public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(InfoBlock), new PropertyMetadata(TextAlignment.Left));
	}
}